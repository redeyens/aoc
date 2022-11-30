foreach (string line in TestInput())
{
    Console.WriteLine(line);
}

foreach (string line in PuzzleInput())
{
    Console.WriteLine(line);
}

Console.WriteLine("aoc.template completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("aoc.template.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("aoc.template.Input.PuzzleInput.txt");

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
