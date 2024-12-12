using System.Data;

var map = PuzzleInput().Select(line => line.ToCharArray()).ToArray();

var perimeter = new Queue<(int row, int col, int dir)>();
var outerPerimeter = new Queue<(int row, int col)>();
var visited = new HashSet<(int row, int col)>();
var regions = new List<(char color, int area, List<(int row, int col, int dir)> perimeter)>();

outerPerimeter.Enqueue((0, 0));

while (outerPerimeter.Count > 0)
{
    var startingPos = outerPerimeter.Dequeue();
    var currentRegion = (color: map[startingPos.row][startingPos.col], area: 0, perimeter: new List<(int row, int col, int dir)>());
    perimeter.Enqueue((startingPos.row, startingPos.col, 0));

    while (perimeter.Count > 0)
    {
        var current = perimeter.Dequeue();

        if (currentRegion.color == map[current.row][current.col])
        {
            if (visited.Contains((current.row, current.col)))
            {
                continue;
            }

            currentRegion.area += 1;

            if (current.row == 0)
            {
                currentRegion.perimeter.Add((current.row - 1, current.col, 0));
            }
            else
            {
                perimeter.Enqueue((current.row - 1, current.col, 0));
            }

            if (current.col == 0)
            {
                currentRegion.perimeter.Add((current.row, current.col - 1, 1));
            }
            else
            {
                perimeter.Enqueue((current.row, current.col - 1, 1));
            }

            if (current.row == map.Length - 1)
            {
                currentRegion.perimeter.Add((current.row + 1, current.col, 2));
            }
            else
            {
                perimeter.Enqueue((current.row + 1, current.col, 2));
            }

            if (current.col == map[current.row].Length - 1)
            {
                currentRegion.perimeter.Add((current.row, current.col + 1, 3));
            }
            else
            {
                perimeter.Enqueue((current.row, current.col + 1, 3));
            }

            visited.Add((current.row, current.col));
        }
        else
        {
            outerPerimeter.Enqueue((current.row, current.col));
            currentRegion.perimeter.Add(current);
        }
    }

    if (currentRegion.area > 0)
    {
        regions.Add(currentRegion);
    }
}

Console.WriteLine(regions.Sum(r => r.area * CountSides(r)));

Console.WriteLine("day12 completed.");

int CountSides((char color, int area, List<(int row, int col, int dir)> perimeter) area)
{
    var a = area.perimeter.GroupBy(x => (x.row, x.dir)).ToDictionary(x => x.Key, y => y.ToList());
    var b = a.Values.Select(x => x.Where(a => a.dir == 0 || a.dir == 2))
        .Where(x => x.Any()).Select(x => x.ToList()).ToList();
    var horizontal = b.Select(x => x.OrderBy(a => a.col).Zip(x.OrderBy(b => b.col).Skip(1)).Count(x => (x.Second.col - x.First.col) > 1) + 1).Sum();
    var d = area.perimeter.GroupBy(x => (x.col, x.dir)).ToDictionary(x => x.Key, y => y.ToList());
    var c = d.Values
        .Select(g => g.Where(a => a.dir == 1 || a.dir == 3).ToList())
        .Where(x => x.Any()).ToList();
    var vertical = c
        .Select(x => x.OrderBy(a => a.row).Zip(x.OrderBy(b => b.row).Skip(1)).Count(x => (x.Second.row - x.First.row) > 1) + 1).Sum();

    return horizontal + vertical;
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
