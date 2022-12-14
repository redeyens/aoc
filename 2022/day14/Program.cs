var rocks = new HashSet<(int x, int y)>();

foreach (string line in PuzzleInput())
{
    var rockCorner = line.Split("->", StringSplitOptions.TrimEntries);
    var coords = rockCorner.Select(GetCoords).ToList();
    TraceLines(rocks, coords);
}

var existingRocks = rocks.Count;
var bottom = rocks.Max(r => r.y);

var floor = bottom + 2;

while (true)
{
    var next = (x: 500, y: 0);

    while (true)
    {
        if (!rocks.Contains((next.x, next.y + 1)) && next.y + 1 < floor)
        {
            next.y += 1;
        }
        else if (!rocks.Contains((next.x - 1, next.y + 1)) && next.y + 1 < floor)
        {
            next = (next.x - 1, next.y + 1);
        }
        else if (!rocks.Contains((next.x + 1, next.y + 1)) && next.y + 1 < floor)
        {
            next = (next.x + 1, next.y + 1);
        }
        else
        {
            rocks.Add(next);
            break;
        }
    }

    if (rocks.Contains((x: 500, y: 0)))
    {
        break;
    }
}

Console.WriteLine(rocks.Count - existingRocks);

Console.WriteLine("day14 completed.");

void TraceLines(HashSet<(int x, int y)> rocks, List<(int x, int y)> coords)
{
    if (coords.Count == 0) return;

    if (coords.Count == 1)
    {
        rocks.Add(coords[0]);
        return;
    }

    for (int r = 1; r < coords.Count; r++)
    {
        var current = coords[r - 1];
        var end = coords[r];

        while (current != end)
        {
            rocks.Add(current);

            var dx = Math.Sign(end.x - current.x);
            var dy = Math.Sign(end.y - current.y);

            current = (current.x + dx, current.y + dy);
        }
        rocks.Add(end);
    }
}

(int x, int y) GetCoords(string rock)
{
    var xy = rock.Split(',');
    return (int.Parse(xy[0]), int.Parse(xy[1]));
}

static IEnumerable<string> TestInput() => GetLinesFromResource("day14.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day14.Input.PuzzleInput.txt");

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
