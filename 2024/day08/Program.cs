using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;

var am = new Regex("([a-zA-Z0-9])");
var row = 0;
var colCount = 0;
var antennas = new Dictionary<char, List<(int row, int col)>>();
foreach (string line in PuzzleInput())
{
    colCount = line.Length;
    foreach (var (f, loc) in am.Matches(line).Select(m => (f: m.Value, loc: (row: row, col: m.Index))))
    {
        if (!antennas.TryGetValue(f[0], out var bucket))
        {
            antennas[f[0]] = bucket = new List<(int row, int col)>();
        }
        bucket.Add(loc);
    }
    row++;
}
var rowCount = row;

var res = antennas.Values.SelectMany(bucket => bucket.SelectMany(b1 => bucket.Where(x => x != b1).Select(b2 => (first: b1, second: b2))))
    .SelectMany(Project)
    .Distinct()
    .ToList();

Console.WriteLine(res.Count);
Console.WriteLine("day08 completed.");

IEnumerable<(int row, int col)> Project(((int row, int col) first, (int row, int col) second) pair)
{
    for (int i = 0; ; i++)
    {
        var pRow = pair.second.row + i * (pair.second.row - pair.first.row);
        var pCol = pair.second.col + i * (pair.second.col - pair.first.col);
        if (pRow >= 0 && pCol >= 0 && pRow < rowCount && pCol < colCount)
        {
            yield return (pRow, pCol);
        }
        else
        {
            break;
        }
    }
}

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
