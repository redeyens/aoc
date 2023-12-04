var totalVal = 0;

foreach (string line in PuzzleInput())
{
    var splitOpt = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
    var numbers = line.Split(":", splitOpt)[1].Split("|", splitOpt);
    var winningNumbers = numbers[0].Split(" ", splitOpt).Select(s => int.Parse(s));
    var myNumbers = numbers[1].Split(" ", splitOpt).Select(s => int.Parse(s));
    var val = 0;
    foreach (var n in myNumbers.Intersect(winningNumbers))
    {
        val = val == 0 ? 1 : val << 1;

    }
    totalVal += val;
}

Console.WriteLine(totalVal);
Console.WriteLine("day04 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day04.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day04.Input.PuzzleInput.txt");

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
