using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;

var am = new Regex("([a-zA-Z0-9])");
var row = 0;
var colCount = 0;
var antennas = new Dictionary<char, List<(int row, int col)>>();
foreach (string line in PuzzleInput())
{
    colCount = line.Length;
    foreach (var antenna in am.Matches(line).Select(m => (f: m.Value, loc: (row: row, col: m.Index))))
    {
        if (!antennas.TryGetValue(antenna.f[0], out var bucket))
        {
            antennas[antenna.f[0]] = bucket = new List<(int row, int col)>();
        }
        bucket.Add(antenna.loc);
    }
    row++;
}
var rowCount = row;

var res = antennas.Values.SelectMany(bucket => bucket.SelectMany(b1 => bucket.Where(x => x != b1).Select(b2 => (first: b1, second: b2))))
    .Select(Project)
    .Where(x => x.row >= 0 && x.col >= 0 && x.row < rowCount && x.col < colCount)
    .Distinct()
    .ToList();

Console.WriteLine(res.Count);
Console.WriteLine("day08 completed.");

(int row, int col) Project(((int row, int col) first, (int row, int col) second) pair)
{
    var pRow = 2 * pair.second.row - pair.first.row;
    var pCol = 2 * pair.second.col - pair.first.col;

    return (pRow, pCol);
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
