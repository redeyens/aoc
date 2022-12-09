var model = new RopeModel();

foreach (string line in PuzzleInput())
{
    var cmd = line.Split(" ");
    model.MoveHead(cmd[0][0], int.Parse(cmd[1]));
}

Console.WriteLine(model.Score);

Console.WriteLine("day09 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day09.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day09.Input.PuzzleInput.txt");

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
