
var input = GetInput().Split(string.Join(string.Empty, Environment.NewLine, Environment.NewLine), StringSplitOptions.TrimEntries);

var seeds = input[0].Split(":", StringSplitOptions.TrimEntries)[1].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToList();
var seedRanges = CreateSeedRanges(seeds);
var seedToSoil = input[1].Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(RangeMapFromLine).Aggregate(Mapping.Default, (cur, next) => cur.Prepend(next));
var soilToFert = input[2].Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(RangeMapFromLine).Aggregate(Mapping.Default, (cur, next) => cur.Prepend(next));
var fertToWater = input[3].Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(RangeMapFromLine).Aggregate(Mapping.Default, (cur, next) => cur.Prepend(next));
var waterToLight = input[4].Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(RangeMapFromLine).Aggregate(Mapping.Default, (cur, next) => cur.Prepend(next));
var lightToTemp = input[5].Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(RangeMapFromLine).Aggregate(Mapping.Default, (cur, next) => cur.Prepend(next));
var tempTohum = input[6].Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(RangeMapFromLine).Aggregate(Mapping.Default, (cur, next) => cur.Prepend(next));
var humToLoc = input[7].Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(RangeMapFromLine).Aggregate(Mapping.Default, (cur, next) => cur.Prepend(next));

var locs = seedRanges
    .SelectMany(seedToSoil.Map)
    .SelectMany(soilToFert.Map)
    .SelectMany(fertToWater.Map)
    .SelectMany(waterToLight.Map)
    .SelectMany(lightToTemp.Map)
    .SelectMany(tempTohum.Map)
    .SelectMany(humToLoc.Map);

Console.WriteLine(locs.Min(l => l.Start));
Console.WriteLine("day05 completed.");

RangeMapping RangeMapFromLine(string line)
{
    var param = line.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    var destRangeStart = long.Parse(param[0]);
    var srcRangeStart = long.Parse(param[1]);
    var rangeLen = long.Parse(param[2]);

    return new RangeMapping(destRangeStart, srcRangeStart, rangeLen);
}

string GetInput()
{
    using Stream inStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("day05.Input.PuzzleInput.txt") ?? throw new InvalidOperationException("Could not open input stream!");
    using TextReader inReader = new StreamReader(inStream);

    return inReader.ReadToEnd();
}

IEnumerable<SeedRange> CreateSeedRanges(List<long> seeds)
{
    for (int i = 0; i < seeds.Count / 2; i++)
    {
        yield return new SeedRange(seeds[2 * i], seeds[2 * i + 1]);
    }
}

internal class SeedRange
{
    private long rangeStart;
    private long rangeLen;

    public SeedRange(long rangeStart, long rangeLen)
    {
        this.rangeStart = rangeStart;
        this.rangeLen = rangeLen;
    }

    public long Start => rangeStart;

    public long End => rangeStart + rangeLen - 1;

    internal IEnumerable<SeedRange> Split(long start)
    {
        if (start > rangeStart && start < rangeStart + rangeLen)
        {
            long leftLen = start - rangeStart;

            yield return new SeedRange(rangeStart, leftLen);
            yield return new SeedRange(start, rangeLen - leftLen);
        }
        else
        {
            yield return this;
        }
    }
}

internal class RangeMapping : Mapping
{
    private long destRangeStart;
    private long srcRangeStart;
    private long rangeLen;
    private Mapping alternative = new NoMapping();

    public RangeMapping(long destRangeStart, long srcRangeStart, long rangeLen) : this(destRangeStart, srcRangeStart, rangeLen, new NoMapping()) { }

    public RangeMapping(long destRangeStart, long srcRangeStart, long rangeLen, Mapping alternative)
    {
        this.destRangeStart = destRangeStart;
        this.srcRangeStart = srcRangeStart;
        this.rangeLen = rangeLen;
        this.alternative = alternative;
    }

    protected override Mapping WithAlternative(Mapping mapping)
    {
        return new RangeMapping(destRangeStart, srcRangeStart, rangeLen, mapping);
    }

    internal override IEnumerable<SeedRange> Map(SeedRange range)
    {
        var rangesToMap = range.Split(srcRangeStart).SelectMany(range => range.Split(srcRangeStart + rangeLen)).ToList();

        return rangesToMap
            .Where(r => CanMap(r.Start))
            .Select(r => new SeedRange(Map(r.Start), r.End - r.Start + 1))
            .Concat(rangesToMap.Where(r => !CanMap(r.Start)).SelectMany(alternative.Map));
    }

    private bool CanMap(long num) => num >= srcRangeStart && num < srcRangeStart + rangeLen;

    private long Map(long num) => num - srcRangeStart + destRangeStart;
}

internal abstract class Mapping
{
    public static Mapping Default = new NoMapping();

    public Mapping Prepend(Mapping other) => other.WithAlternative(this);

    protected abstract Mapping WithAlternative(Mapping mapping);

    internal abstract IEnumerable<SeedRange> Map(SeedRange range);
}

internal class NoMapping : Mapping
{
    protected override Mapping WithAlternative(Mapping mapping) => this;

    internal override IEnumerable<SeedRange> Map(SeedRange range)
    {
        yield return range;
    }
}
