int targetLine = 2000000;
var beacons = new HashSet<(int x, int y)>();
var exclusionZone = new List<(int from, int to)>();

foreach (string line in Input())
{
    var majorParts = line.Split(':', StringSplitOptions.TrimEntries);
    var sensorCoords = majorParts[0].Split(' ', 3)[2].Split(',', StringSplitOptions.TrimEntries).Select(c => int.Parse(c[2..])).ToArray();
    var beaconCoords = majorParts[1].Split(' ', 5)[4].Split(',', StringSplitOptions.TrimEntries).Select(c => int.Parse(c[2..])).ToArray();

    if (beaconCoords[1] == targetLine)
    {
        beacons.Add((x: beaconCoords[0], y: beaconCoords[1]));
    }

    int distance = Math.Abs(sensorCoords[0] - beaconCoords[0]) + Math.Abs(sensorCoords[1] - beaconCoords[1]);
    int yDist = Math.Abs(sensorCoords[1] - targetLine);
    int xFrom = sensorCoords[0] - (yDist - distance);
    int xTo = sensorCoords[0] + (yDist - distance);
    exclusionZone.Add((Math.Min(xFrom, xTo), Math.Max(xFrom, xTo)));
}

var sorted = exclusionZone.OrderBy(r => r.from).ToList();
var indexesMerged = new List<int>(new int[] { 1 });
for (int i = 1; i < sorted.Count; i++)
{
    Console.WriteLine(sorted[i - 1]);
    if (sorted[i].from <= sorted[i - 1].to)
    {
        if (sorted[i].to > sorted[i - 1].to)
        {
            sorted[i - 1] = (sorted[i - 1].from, sorted[i].to);
        }
    }
    else
    {
        indexesMerged.Add(i);
    }
}
Console.WriteLine(sorted[sorted.Count - 1]);

foreach (var index in indexesMerged)
{
    Console.WriteLine($"{sorted[index]} -> {sorted[index].to - sorted[index].from}");
}

// Console.WriteLine(exclusionZone.to - exclusionZone.from + 1 - beacons.Count);

Console.WriteLine("day15 completed.");

static IEnumerable<string> Input() => GetLinesFromResource("day15.Input.PuzzleInput.txt");

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
