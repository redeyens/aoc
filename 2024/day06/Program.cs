using System.Text.RegularExpressions;

var obstacleMatcher = new Regex("#");
var row = 0;
var obstacles = new HashSet<(int x, int y)>();
(int x, int y) guardPos = default;
int maxX = 0;
int maxY = 0;
(int dx, int dy) move = (0, -1);

foreach (string line in PuzzleInput())
{
    maxX = line.Length;
    var guardCol = line.IndexOf('^');
    if (guardCol > -1)
    {
        guardPos = (x: guardCol, y: row);
    }
    foreach (var obstacle in obstacleMatcher.Matches(line).Select(m => (x: m.Index, y: row)))
    {
        obstacles.Add(obstacle);
    }
    row++;
}
maxY = row;

var visitedLocations = new HashSet<(int x, int y)>();
while (guardPos.x >= 0 && guardPos.x < maxX && guardPos.y >= 0 && guardPos.y < maxY)
{
    visitedLocations.Add(guardPos);
    var newPos = (x: guardPos.x + move.dx, y: guardPos.y + move.dy);
    if (obstacles.Contains(newPos))
    {
        move = TurnRight(move);
    }
    else
    {
        guardPos = newPos;
    }
}

System.Console.WriteLine(visitedLocations.Count);

Console.WriteLine("day06 completed.");

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
