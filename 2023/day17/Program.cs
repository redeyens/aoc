var heatLoss = TestInput().Select(x => x.Select(y => y - '0').ToList()).ToList();
var goal = (heatLoss.Count - 1, heatLoss[heatLoss.Count - 1].Count - 1);
var horizon = new PriorityQueue<((int row, int col) prev, (int row, int col) pos, int run, int bestHeatLoss), int>();
var visited = new HashSet<(int row, int col)>();
var totalHeatLoss = 0;
horizon.Enqueue(((0, 0), (0, 1), 1, heatLoss[0][1]), heatLoss[0][1]);
horizon.Enqueue(((0, 0), (1, 0), 1, heatLoss[1][0]), heatLoss[1][0]);

while (horizon.Count > 0)
{
    var current = horizon.Dequeue();
    while (visited.Contains(current.pos))
    {
        current = horizon.Dequeue();
    }
    if (current.pos == goal)
    {
        totalHeatLoss = current.bestHeatLoss;
        break;
    }

    horizon.EnqueueRange(GetNextSteps(current).Where(x => IsInBounds(heatLoss, x) && !visited.Contains(x.pos)).Select(x => (x.prev, x.pos, x.run, bestHeatLoss: x.bestHeatLoss + heatLoss[x.pos.row][x.pos.col])).Select(element => (element, element.bestHeatLoss)));

    visited.Add(current.pos);
    Console.WriteLine($"{current.pos} - {current.bestHeatLoss}");
}

Console.WriteLine(totalHeatLoss);
Console.WriteLine("day17 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day17.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day17.Input.PuzzleInput.txt");

static bool IsInBounds(List<List<int>> heatLoss, ((int row, int col) prev, (int row, int col) pos, int run, int bestHeatLoss) x)
    => x.pos.row >= 0 && x.pos.row < heatLoss.Count && x.pos.col >= 0 && x.pos.col < heatLoss[0].Count;

static IEnumerable<((int row, int col) prev, (int row, int col) pos, int run, int bestHeatLoss)> GetNextSteps(((int row, int col) prev, (int row, int col) pos, int run, int bestHeatLoss) current)
{
    var direction = (dr: current.pos.row - current.prev.row, dc: current.pos.col - current.prev.col);
    if (current.run < 3)
    {
        yield return (current.pos, (current.pos.row + direction.dr, current.pos.col + direction.dc), current.run + 1, current.bestHeatLoss);
    }
    direction = (direction.dc, direction.dr);
    yield return (current.pos, (current.pos.row + direction.dr, current.pos.col + direction.dc), 1, current.bestHeatLoss);
    direction = (-direction.dr, -direction.dc);
    yield return (current.pos, (current.pos.row + direction.dr, current.pos.col + direction.dc), 1, current.bestHeatLoss);

}

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
