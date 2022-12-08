var grid = PuzzleInput().Select(l => l.ToCharArray()).ToArray();
var topHorizon = new char[grid[0].Length];
Array.Copy(grid[0], topHorizon, grid[0].Length);
var internalVisibleTrees = new HashSet<(int i, int j)>();
var bottomCandidates = new HashSet<(int i, int j)>();

for (int i = 1; i < grid.Length - 1; i++)
{
    char leftHorizon = grid[i][0];
    char rightHorizon = grid[i][grid[i].Length - 1];
    var row = grid[i];

    for (int j = 1; j < row.Length - 1; j++)
    {
        int p = row.Length - 1 - j;

        if (topHorizon[j] < row[j])
        {
            internalVisibleTrees.Add((i, j));
            topHorizon[j] = row[j];
        }
        if (leftHorizon < row[j])
        {
            internalVisibleTrees.Add((i, j));
            leftHorizon = row[j];
        }
        if (rightHorizon < row[p])
        {
            internalVisibleTrees.Add((i, p));
            rightHorizon = row[p];
        }
        bottomCandidates.ExceptWith(bottomCandidates.Where(p => p.j == j && grid[p.i][p.j] <= row[j]));
        bottomCandidates.Add((i, j));
    }
}

var lastRow = grid[grid.Length - 1];
for (int j = 1; j < lastRow.Length - 1; j++)
{
    bottomCandidates.ExceptWith(bottomCandidates.Where(p => p.j == j && grid[p.i][p.j] <= lastRow[j]));
}

internalVisibleTrees.UnionWith(bottomCandidates);
int alwaysVisibleCount = grid.Length * 2 + grid[0].Length * 2 - 4;

Console.WriteLine(alwaysVisibleCount + internalVisibleTrees.Count);

Console.WriteLine("day08 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day08.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day08.Input.PuzzleInput.txt");

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
