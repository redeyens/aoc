var segmentMap = new Dictionary<(int row, int col), IEnumerable<(int dr, int dc)>>();
var start = (row: -1, col: -1);
var row = 0;

foreach (string line in PuzzleInput())
{
    var startIndex = line.IndexOf('S');
    if (startIndex >= 0)
    {
        start = (row: row, col: startIndex);
    }
    var pipeSegments = line.Select((c, col) => (pos: (row, col), steps: GetSteps(c))).Where(x => x.steps.Any());
    foreach (var segment in pipeSegments)
    {
        segmentMap[segment.pos] = segment.steps;
    }
    row++;
}

var possibleStart = new (int row, int col)[] { (start.row - 1, start.col), (start.row + 1, start.col), (start.row, start.col - 1), (start.row, start.col + 1) };

var horizon = new Queue<(int row, int col)>(possibleStart.Where(x => segmentMap.ContainsKey(x) && segmentMap[x].Select(d => (x.row + d.dr, x.col + d.dc)).Any(x => start == x)));
var visited = new HashSet<(int row, int col)>
{
    start
};

while (horizon.Count > 0)
{
    var node = horizon.Dequeue();
    foreach (var item in segmentMap[node].Select(d => (node.row + d.dr, node.col + d.dc)).Except(visited))
    {
        horizon.Enqueue(item);
    }
    visited.Add(node);
}

Console.WriteLine(visited.Count / 2);
Console.WriteLine("day10 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day10.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day10.Input.PuzzleInput.txt");

static IEnumerable<(int, int)> GetSteps(char c) => c switch
{
    '|' => new (int, int)[] { (-1, 0), (1, 0) },
    '-' => new (int, int)[] { (0, -1), (0, 1) },
    'L' => new (int, int)[] { (-1, 0), (0, 1) },
    'J' => new (int, int)[] { (-1, 0), (0, -1) },
    '7' => new (int, int)[] { (0, -1), (1, 0) },
    'F' => new (int, int)[] { (1, 0), (0, 1) },
    _ => Enumerable.Empty<(int, int)>()
};

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

