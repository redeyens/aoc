var total = 0;
foreach (string line in PuzzleInput())
{
    total += line.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(x => GetGash(x)).Sum();
}

int GetGash(string x)
{
    int current = 0;

    foreach (var c in x)
    {
        current += c;
        current *= 17;
        current %= 256;
    }

    return current;
}

Console.WriteLine(total);
Console.WriteLine("day15 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day15.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day15.Input.PuzzleInput.txt");

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
