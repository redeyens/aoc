var map = PuzzleInput().Select(line => line.ToCharArray()).ToArray();

var trailHeads = map.SelectMany((line, row) => line.Select((c, col) => (c, row, col)))
    .Where(pos => pos.c == '0')
    .Select(pos => (pos.row, pos.col));

var trails = trailHeads.Select(trailHead => (trailHead, ends: GetAllTrails(map, trailHead).Distinct().ToList())).ToList();
var totalScore = trails.Sum(trailHead => trailHead.ends.Count());

Console.WriteLine(totalScore);

Console.WriteLine("day10 completed.");

IEnumerable<(int row, int col)> GetAllTrails(char[][] map, (int row, int col) trailHead)
{
    var boundary = new Queue<(int row, int col)>();
    boundary.Enqueue(trailHead);

    while (boundary.Count > 0)
    {
        var currentPos = boundary.Dequeue();
        if (map[currentPos.row][currentPos.col] == '9')
        {
            yield return currentPos;
        }
        else
        {
            foreach (var next in GetAllStepsFrom(map, currentPos).Where(next => map[next.row][next.col] - map[currentPos.row][currentPos.col] == 1))
            {
                boundary.Enqueue(next);
            }
        }
    }
}

IEnumerable<(int row, int col)> GetAllStepsFrom(char[][] map, (int row, int col) pos)
{
    if (pos.row > 0)
    {
        yield return (pos.row - 1, pos.col);
    }
    if (pos.col > 0)
    {
        yield return (pos.row, pos.col - 1);
    }
    if (pos.row < map.Length - 1)
    {
        yield return (pos.row + 1, pos.col);
    }
    if (pos.col < map[pos.row].Length - 1)
    {
        yield return (pos.row, pos.col + 1);
    }
}

static IEnumerable<string> TestInput() => GetLinesFromResource("day10.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day10.Input.PuzzleInput.txt");

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
