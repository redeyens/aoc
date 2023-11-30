var valves = new Dictionary<string, (int pressureRelease, string[] tunnels)>();

foreach (string line in Input())
{
    var parts = line.Split(new char[] { ' ', ';', '=' }, 11, StringSplitOptions.RemoveEmptyEntries);
    var valve = parts[1];
    var pressureRelease = int.Parse(parts[5]);
    var tunnels = parts[10].Split(',', StringSplitOptions.TrimEntries);

    valves[valve] = (pressureRelease, tunnels);
}

foreach (var valve in valves)
{
    Console.WriteLine(string.Join(' ', valve.Key, valve.Value.pressureRelease, string.Join(',', valve.Value.tunnels)));
}

Console.WriteLine("day16 completed.");

static IEnumerable<string> Input() => GetLinesFromResource("day16.Input.TestInput.txt");

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
