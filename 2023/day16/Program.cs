var input = PuzzleInput().ToList();
var elements = input.SelectMany((line, row) => line.Select((c, col) => (c, row, col)).Where(x => x.c != '.')).ToList();
var ray = (startRow: 0, startCol: int.MinValue, endRow: 0, endCol: int.MaxValue);
var visitedRays = new HashSet<(int startRow, int startCol, int endRow, int endCol)>();
var rays = new Queue<(int startRow, int startCol, int endRow, int endCol)>();
rays.Enqueue(ray);
var energized = new HashSet<(int row, int col)>();

while (rays.Count > 0)
{
    var cR = rays.Dequeue();
    visitedRays.Add(cR);
    var possibleHit = elements.Where(x => x.row >= Math.Min(cR.startRow, cR.endRow) && x.row <= Math.Max(cR.startRow, cR.endRow) && x.col >= Math.Min(cR.startCol, cR.endCol) && x.col <= Math.Max(cR.startCol, cR.endCol));
    possibleHit = (cR.endCol > cR.startCol) ? possibleHit.OrderBy(x => x.col).Where(x => x.c != '-' && x.col != cR.startCol)
        : (cR.startCol > cR.endCol) ? possibleHit.OrderByDescending(x => x.col).Where(x => x.c != '-' && x.col != cR.startCol)
        : (cR.endRow > cR.startRow) ? possibleHit.OrderBy(x => x.row).Where(x => x.c != '|' && x.row != cR.startRow)
        : (cR.startRow > cR.endRow) ? possibleHit.OrderByDescending(x => x.row).Where(x => x.c != '|' && x.row != cR.startRow)
        : throw new InvalidOperationException();
    var hit = possibleHit.FirstOrDefault();

    if (hit.c != default(char))
    {
        cR.endRow = hit.row;
        cR.endCol = hit.col;

        foreach (var r in GetNextRays(cR, hit).Except(visitedRays))
        {
            rays.Enqueue(r);
        }
    }

    for (int i = Math.Max(0, Math.Min(cR.startRow, cR.endRow)); i <= Math.Min(Math.Max(cR.startRow, cR.endRow), input.Count - 1); i++)
    {
        for (int j = Math.Max(0, Math.Min(cR.startCol, cR.endCol)); j <= Math.Min(Math.Max(cR.startCol, cR.endCol), input[0].Length - 1); j++)
        {
            energized.Add((i, j));
        }
    }
}

Console.WriteLine(energized.Count);
Console.WriteLine("day16 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day16.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day16.Input.PuzzleInput.txt");

static IEnumerable<(int startRow, int startCol, int endRow, int endCol)> GetNextRays((int startRow, int startCol, int endRow, int endCol) cR, (char c, int row, int col) hit)
{
    if (hit.c == '|' && (cR.endCol > cR.startCol || cR.endCol < cR.startCol))
    {
        yield return (hit.row, hit.col, int.MinValue, hit.col);
        yield return (hit.row, hit.col, int.MaxValue, hit.col);
    }
    if (hit.c == '-' && (cR.endRow > cR.startRow || cR.endRow < cR.startRow))
    {
        yield return (hit.row, hit.col, hit.row, int.MinValue);
        yield return (hit.row, hit.col, hit.row, int.MaxValue);
    }
    if (hit.c == '/')
    {
        if (cR.endCol > cR.startCol)
        {
            yield return (hit.row, hit.col, int.MinValue, hit.col);
        }
        if (cR.endCol < cR.startCol)
        {
            yield return (hit.row, hit.col, int.MaxValue, hit.col);
        }
        if (cR.endRow > cR.startRow)
        {
            yield return (hit.row, hit.col, hit.row, int.MinValue);
        }
        if (cR.endRow < cR.startRow)
        {
            yield return (hit.row, hit.col, hit.row, int.MaxValue);
        }
    }
    if (hit.c == '\\')
    {
        if (cR.endCol > cR.startCol)
        {
            yield return (hit.row, hit.col, int.MaxValue, hit.col);
        }
        if (cR.endCol < cR.startCol)
        {
            yield return (hit.row, hit.col, int.MinValue, hit.col);
        }
        if (cR.endRow > cR.startRow)
        {
            yield return (hit.row, hit.col, hit.row, int.MaxValue);
        }
        if (cR.endRow < cR.startRow)
        {
            yield return (hit.row, hit.col, hit.row, int.MinValue);
        }
    }
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
