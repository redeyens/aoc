Console.WriteLine(GetCalorieLists(PuzzleInput()).Select(l => l.Sum()).OrderByDescending(c => c).Take(3).Sum());

Console.WriteLine("day01 completed.");

static IEnumerable<List<int>> GetCalorieLists(IEnumerable<string> source)
{
    List<int> result = new();

    foreach (var line in source)
    {
        if (string.IsNullOrEmpty(line))
        {
            yield return result;
            result = new();
        }
        else
        {
            result.Add(int.Parse(line));
        }
    }
    yield return result;
}

static IEnumerable<string> TestInput() => GetLinesFromResource("day01.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day01.Input.PuzzleInput.txt");

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
