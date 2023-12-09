var total = 0;

foreach (string line in PuzzleInput())
{
    var measurements = line.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToList();
    var lastVal = new Stack<int>();
    while (measurements.Any(m => m != 0))
    {
        lastVal.Push(measurements.Last());
        measurements = measurements.Take(measurements.Count - 1).Zip(measurements.Skip(1)).Select(x => x.Second - x.First).ToList();
    }
    while (lastVal.Count > 1)
    {
        lastVal.Push(lastVal.Pop() + lastVal.Pop());
    }

    total += lastVal.Pop();
}

Console.WriteLine(total);
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
