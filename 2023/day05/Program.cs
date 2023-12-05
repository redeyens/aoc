var input = GetInput().Split(string.Join(string.Empty, Environment.NewLine, Environment.NewLine), StringSplitOptions.TrimEntries);

var seeds = input[0].Split(":", StringSplitOptions.TrimEntries)[1].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s));
var seedToSoil = input[1].Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(RangeMapFromLine).Aggregate(Mapping.Default, (cur, next) => cur.Prepend(next));
var soilToFert = input[2].Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(RangeMapFromLine).Aggregate(Mapping.Default, (cur, next) => cur.Prepend(next));
var fertToWater = input[3].Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(RangeMapFromLine).Aggregate(Mapping.Default, (cur, next) => cur.Prepend(next));
var waterToLight = input[4].Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(RangeMapFromLine).Aggregate(Mapping.Default, (cur, next) => cur.Prepend(next));
var lightToTemp = input[5].Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(RangeMapFromLine).Aggregate(Mapping.Default, (cur, next) => cur.Prepend(next));
var tempTohum = input[6].Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(RangeMapFromLine).Aggregate(Mapping.Default, (cur, next) => cur.Prepend(next));
var humToLoc = input[7].Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(RangeMapFromLine).Aggregate(Mapping.Default, (cur, next) => cur.Prepend(next));

var locs = seeds
    .Select(seedToSoil.Map)
    .Select(soilToFert.Map)
    .Select(fertToWater.Map)
    .Select(waterToLight.Map)
    .Select(lightToTemp.Map)
    .Select(tempTohum.Map)
    .Select(humToLoc.Map);

Console.WriteLine(locs.Min());
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

    internal override bool CanMap(long num) => num >= srcRangeStart && num < srcRangeStart + rangeLen;

    internal override long Map(long num) => CanMap(num) ? num - srcRangeStart + destRangeStart : alternative.Map(num);
}

internal abstract class Mapping
{
    public static Mapping Default = new NoMapping();

    public Mapping Prepend(Mapping other) => other.WithAlternative(this);

    protected abstract Mapping WithAlternative(Mapping mapping);

    internal abstract long Map(long num);

    internal abstract bool CanMap(long num);
}

internal class NoMapping : Mapping
{
    protected override Mapping WithAlternative(Mapping mapping) => this;

    internal override bool CanMap(long num) => true;

    internal override long Map(long num) => num;
}
