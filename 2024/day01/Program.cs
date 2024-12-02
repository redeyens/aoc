List<long> left = new List<long>();
List<long> right = new List<long>();
foreach (string line in PuzzleInput())
{
    var locations = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    left.Add(long.Parse(locations[0]));

    right.Add(long.Parse(locations[1]));
}
var second = right.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
var res = left.Sum(x => x * (second.ContainsKey(x) ? second[x] : 0));

System.Console.WriteLine(res);

Console.WriteLine("day01 completed.");

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
