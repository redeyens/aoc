using System.Data;
using System.Security.AccessControl;

var galaxies = PuzzleInput().SelectMany((l, row) => l.Select((c, col) => (c, coord: (row: row, col: col)))).Where(g => g.c == '#').Select(g => g.coord).OrderBy(g => g.row).ToList();

var prevNonEmptyRow = 0;
var currentInflation = 0;
for (int i = 0; i < galaxies.Count; i++)
{
    var localInflation = (galaxies[i].row - prevNonEmptyRow > 1) ? galaxies[i].row - prevNonEmptyRow - 1 : 0;
    currentInflation += localInflation;
    prevNonEmptyRow = galaxies[i].row;
    galaxies[i] = (galaxies[i].row + currentInflation, galaxies[i].col);
}

galaxies = galaxies.OrderBy(g => g.col).ToList();
var prevNonEmptyCol = 0;
currentInflation = 0;
for (int i = 0; i < galaxies.Count; i++)
{
    var localInflation = (galaxies[i].col - prevNonEmptyCol > 1) ? galaxies[i].col - prevNonEmptyCol - 1 : 0;
    currentInflation += localInflation;
    prevNonEmptyCol = galaxies[i].col;
    galaxies[i] = (galaxies[i].row, galaxies[i].col + currentInflation);
}

Console.WriteLine(GetAllPairs(galaxies).Sum(p => Math.Abs(p.Item2.row - p.Item1.row) + Math.Abs(p.Item2.col - p.Item1.col)));

Console.WriteLine("day11 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day11.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day11.Input.PuzzleInput.txt");

IEnumerable<((int row, int col), (int row, int col))> GetAllPairs(List<(int row, int col)> galaxies)
{
    for (int i = 0; i < galaxies.Count - 1; i++)
    {
        for (int j = i + 1; j < galaxies.Count; j++)
        {
            yield return (galaxies[i], galaxies[j]);
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
