var report = new List<int>();
int res = 0;
foreach (string line in PuzzleInput())
{
    report.Clear();
    report.AddRange(line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => int.Parse(x)));
    var dr = report.Zip(report.Skip(1), (l, r) => r - l);
    if (dr.All(x => Math.Abs(x) > 0 && Math.Abs(x) < 4)
    && dr.GroupBy(x => Math.Sign(x)).Count() == 1)
    {
        res++;
    }
}

System.Console.WriteLine(res);

Console.WriteLine("day02 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day02.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day02.Input.PuzzleInput.txt");

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
