Console.WriteLine(PuzzleInput().Select(ParseRanges).Where(x => OneIsFullyContained(x.first, x.second)).Count());

Console.WriteLine("day03 completed.");

static ((int from, int to) first, (int from, int to) second) ParseRanges(string line)
{
    var pair = line.Split(',');
    var firstRange = pair[0].Split('-');
    var secondRange = pair[1].Split('-');

    return ((int.Parse(firstRange[0]), int.Parse(firstRange[1])), (int.Parse(secondRange[0]), int.Parse(secondRange[1])));
}

static bool OneIsFullyContained((int from, int to) first, (int from, int to) second) => Contains(first, second) || Contains(second, first);

static bool Contains((int from, int to) first, (int from, int to) second) => first.from <= second.from && first.to >= second.to;

static IEnumerable<string> TestInput() => GetLinesFromResource("day03.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day03.Input.PuzzleInput.txt");

static IEnumerable<string> GetLinesFromResource(string name)
{
    using Stream inStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(name) ?? throw new InvalidOperationException("Could not open input stream!");
    using TextReader inReader = new StreamReader(inStream);

    string? line;
    while ((line = inReader.ReadLine()) != null)
    {
        yield return line;
    }
}
