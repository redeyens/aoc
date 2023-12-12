var lines = PuzzleInput().ToList();
var start = (row: -1, col: -1);
var row = 0;

foreach (string line in lines)
{
    var startIndex = line.IndexOf('S');
    if (startIndex >= 0)
    {
        start = (row: row, col: startIndex);
        break;
    }
    row++;
}

var possibleStart = new (int row, int col)[] { (start.row - 1, start.col), (start.row + 1, start.col), (start.row, start.col - 1), (start.row, start.col + 1) }
    .Where(s => s.row >= 0 && s.row < lines.Count && s.col >= 0 && s.col < lines[s.row].Length);

var horizon = new Queue<(int row, int col)>(possibleStart.Where(x => GetSteps(lines[x.row][x.col]).Select(d => (x.row + d.dr, x.col + d.dc)).Any(x => start == x)));

var diff = horizon.Select(x => (x.row - start.row, x.col - start.col)).ToList();
var seg = "|-LJ7F".Where(c => GetSteps(c).Intersect(diff).Count() == 2).First();
lines[start.row] = lines[start.row].Replace('S', seg);

var visited = new HashSet<(int row, int col)>
{
    start
};

while (horizon.Count > 0)
{
    var node = horizon.Dequeue();
    foreach (var item in GetSteps(lines[node.row][node.col]).Select(d => (node.row + d.dr, node.col + d.dc)).Except(visited))
    {
        horizon.Enqueue(item);
    }
    visited.Add(node);
}

var enclosedArea = visited.Where(x => lines[x.row][x.col] != '-'
                               && lines[x.row][x.col] != '.')
    .GroupBy(x => x.row)
    .OrderBy(g => g.Key)
    .Select(g => (row: g.Key, cr: g.OrderBy(x => x.col)))
    .SelectMany(x => x.cr.Zip(x.cr.Skip(1))  // make intervals
        .Select(x => (x.First.row, x.First.col, len: x.Second.col - x.First.col - 1, left: lines[x.First.row][x.First.col], right: lines[x.Second.row][x.Second.col]))
        .Where(x => !((x.left == 'F' && x.right == 'J') || (x.left == 'L' && x.right == '7')))  // exclude angled crossing intervals
        .Select(x => ((x.left == 'F' && x.right == '7') || (x.left == 'L' && x.right == 'J')) ? (x.row, x.col, len: 0, x.left, x.right) : x)   // these would be borders so they don't count
        .Select((x, i) => x.len * ((i + 1) % 2))) // even intervals are outside
    .Sum();

Console.WriteLine(enclosedArea);
Console.WriteLine("day10 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day10.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day10.Input.PuzzleInput.txt");

static IEnumerable<(int dr, int dc)> GetSteps(char c) => c switch
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

