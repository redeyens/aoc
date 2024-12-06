using System.Text.RegularExpressions;

var obstacleMatcher = new Regex("#");
var row = 0;
var obstacles = new HashSet<(int x, int y)>();
(int x, int y) startingPos = default;
int maxX = 0;
int maxY = 0;
(int dx, int dy) initialDirection = (0, -1);

foreach (string line in PuzzleInput())
{
    maxX = line.Length;
    var guardCol = line.IndexOf('^');
    if (guardCol > -1)
    {
        startingPos = (x: guardCol, y: row);
    }
    foreach (var obstacle in obstacleMatcher.Matches(line).Select(m => (x: m.Index, y: row)))
    {
        obstacles.Add(obstacle);
    }
    row++;
}
maxY = row;

var possibleObstacles = new HashSet<(int x, int y)>();
var originalPath = GetPath(startingPos, initialDirection);

foreach (var (pos, dir) in originalPath)
{
    obstacles.Add(pos);

    var testPath = GetPath(startingPos, initialDirection);

    if (!testPath.Select(x => (x: x.pos.x + x.dir.dx, y: x.pos.y + x.dir.dy)).Any(p => !IsInside(maxX, maxY, p)))
    {
        possibleObstacles.Add(pos);
    }

    obstacles.Remove(pos);
}

Console.WriteLine(possibleObstacles.Count);

Console.WriteLine("day06 completed.");

HashSet<((int x, int y) pos, (int dx, int dy) dir)> GetPath((int x, int y) startingPos, (int dx, int dy) initialDirection)
{
    var currentPos = startingPos;
    var move = initialDirection;
    var path = new HashSet<((int x, int y) pos, (int dx, int dy) dir)>();
    while (IsInside(maxX, maxY, currentPos))
    {
        if (!path.Add((currentPos, move)))
        {
            break;
        }
        var newPos = (x: currentPos.x + move.dx, y: currentPos.y + move.dy);
        if (obstacles.Contains(newPos))
        {
            move = TurnRight(move);
        }
        else
        {
            currentPos = newPos;
        }
    }
    return path;
}

(int dx, int dy) TurnRight((int dx, int dy) move) => move switch
{
    (-1, 0) => (0, -1),
    (0, 1) => (-1, 0),
    (1, 0) => (0, 1),
    (0, -1) => (1, 0),
    _ => throw new ArgumentException(nameof(move)),
};

static IEnumerable<string> TestInput() => GetLinesFromResource("day06.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day06.Input.PuzzleInput.txt");

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

static bool IsInside(int maxX, int maxY, (int x, int y) currentPos)
{
    return currentPos.x >= 0 && currentPos.x < maxX && currentPos.y >= 0 && currentPos.y < maxY;
}