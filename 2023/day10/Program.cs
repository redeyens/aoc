var segmentMap = new Dictionary<(int row, int col), HashSet<(int row, int col)>>();
var start = (row: -1, col: -1);
var bigRowCnt = 0;

foreach (string line in PuzzleInput())
{
    var startIndex = line.IndexOf('S');
    if (startIndex >= 0)
    {
        start = (row: bigRowCnt, col: startIndex);
    }
    var pipeSegments = line.SelectMany((c, i) => GetPipeSegment(c, i, bigRowCnt)).ToList();
    foreach (var segment in pipeSegments)
    {
        HashSet<(int row, int col)> set;
        if (!segmentMap.TryGetValue(segment.Item1, out set))
        {
            set = new HashSet<(int row, int col)>();
            segmentMap[segment.Item1] = set;
        }
        set.Add(segment.Item2);

        if (!segmentMap.TryGetValue(segment.Item2, out set))
        {
            set = new HashSet<(int row, int col)>();
            segmentMap[segment.Item2] = set;
        }
        set.Add(segment.Item1);
    }
    bigRowCnt++;
}

var possibleStart = new (int, int)[] { (2 * start.row, 2 * start.col + 1), (2 * start.row + 1, 2 * start.col + 2), (2 * start.row + 2, 2 * start.col + 1), (2 * start.row + 1, 2 * start.col) };

var horizon = new HashSet<(int row, int col)>(possibleStart.Where(x => segmentMap.ContainsKey(x)));
var visited = new HashSet<(int row, int col)>();
var steps = 0;

while (horizon.Count > 1)
{
    foreach (var item in horizon)
    {
        visited.Add(item);
    }
    var next = horizon.SelectMany(x => segmentMap[x].Except(visited)).ToList();
    horizon.Clear();
    foreach (var item in next)
    {
        horizon.Add(item);
    }
    steps++;
}

System.Console.WriteLine(steps);
Console.WriteLine("day10 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day10.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day10.Input.PuzzleInput.txt");

static IEnumerable<((int, int), (int, int))> GetPipeSegment(char c, int i, int bigRowCnt) => c switch
{
    '|' => new ((int, int), (int, int))[] { ((2 * bigRowCnt, 2 * i + 1), (2 * (bigRowCnt + 1), 2 * i + 1)) },
    '-' => new ((int, int), (int, int))[] { ((2 * bigRowCnt + 1, 2 * i), (2 * bigRowCnt + 1, 2 * (i + 1))) },
    'L' => new ((int, int), (int, int))[] { ((2 * bigRowCnt, 2 * i + 1), (2 * bigRowCnt + 1, 2 * (i + 1))) },
    'J' => new ((int, int), (int, int))[] { ((2 * bigRowCnt, 2 * i + 1), (2 * bigRowCnt + 1, 2 * i)) },
    '7' => new ((int, int), (int, int))[] { ((2 * bigRowCnt + 1, 2 * i), (2 * (bigRowCnt + 1), 2 * i + 1)) },
    'F' => new ((int, int), (int, int))[] { ((2 * (bigRowCnt + 1), 2 * i + 1), (2 * bigRowCnt + 1, 2 * (i + 1))) },
    _ => Enumerable.Empty<((int, int), (int, int))>()
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

