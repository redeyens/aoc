var map = new List<string>();
var start = (row: 0, col: 0);
var end = (row: 0, col: 0);

int row = 0;
foreach (string line in PuzzleInput())
{
    var correctedLine = line;
    int col = correctedLine.IndexOf('S');
    if (col >= 0)
    {
        start = (row, col);
        correctedLine = correctedLine.Replace('S', 'a');
    }
    col = correctedLine.IndexOf('E');
    if (col >= 0)
    {
        end = (row, col);
        correctedLine = correctedLine.Replace('E', 'z');
    }
    map.Add(correctedLine);
    row++;
}

var nextSteps = new PriorityQueue<(int row, int col), int>();
var visitedLocations = new (int dist, bool done)[map.Count][];

for (int r = 0; r < visitedLocations.Length; r++)
{
    visitedLocations[r] = new (int dist, bool done)[map[r].Length];
    for (int c = 0; c < visitedLocations[r].Length; c++)
    {
        visitedLocations[r][c] = (int.MaxValue, false);
    }
}

visitedLocations[start.row][start.col].dist = 0;
nextSteps.Enqueue(start, 0);

while (nextSteps.Count > 0)
{
    var current = nextSteps.Dequeue();

    if (visitedLocations[current.row][current.col].done)
    {
        continue;
    }

    visitedLocations[current.row][current.col].done = true;

    foreach (var next in GetNextSteps(map, (current, visitedLocations[current.row][current.col].dist)))
    {
        if (!visitedLocations[next.loc.row][next.loc.col].done
        && visitedLocations[next.loc.row][next.loc.col].dist > next.dist)
        {
            visitedLocations[next.loc.row][next.loc.col].dist = next.dist;
            nextSteps.Enqueue(next.loc, next.dist);
        }
    }
}

Console.WriteLine(visitedLocations[end.row][end.col].dist);

Console.WriteLine("day12 completed.");

IEnumerable<((int row, int col) loc, int dist)> GetNextSteps(List<string> map, ((int row, int col) loc, int dist) current)
{
    if (current.loc.row > 0)
    {
        var next = (loc: (row: current.loc.row - 1, current.loc.col), dist: current.dist + 1);
        var elevationDelta = map[next.loc.row][next.loc.col] - map[current.loc.row][current.loc.col];
        if (elevationDelta < 2)
        {
            yield return next;
        }
    }
    if (current.loc.row < (map.Count - 1))
    {
        var next = (loc: (row: current.loc.row + 1, current.loc.col), dist: current.dist + 1);
        var elevationDelta = map[next.loc.row][next.loc.col] - map[current.loc.row][current.loc.col];
        if (elevationDelta < 2)
        {
            yield return next;
        }
    }
    if (current.loc.col > 0)
    {
        var next = (loc: (current.loc.row, col: current.loc.col - 1), dist: current.dist + 1);
        var elevationDelta = map[next.loc.row][next.loc.col] - map[current.loc.row][current.loc.col];
        if (elevationDelta < 2)
        {
            yield return next;
        }
    }
    if (current.loc.col < (map[0].Length - 1))
    {
        var next = (loc: (current.loc.row, col: current.loc.col + 1), dist: current.dist + 1);
        var elevationDelta = map[next.loc.row][next.loc.col] - map[current.loc.row][current.loc.col];
        if (elevationDelta < 2)
        {
            yield return next;
        }
    }
}

static IEnumerable<string> TestInput() => GetLinesFromResource("day12.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day12.Input.PuzzleInput.txt");

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
