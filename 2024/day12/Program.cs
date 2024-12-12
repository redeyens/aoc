var map = PuzzleInput().Select(line => line.ToCharArray()).ToArray();

var perimeter = new Queue<(int row, int col)>();
var outerPerimeter = new Queue<(int row, int col)>();
var visited = new HashSet<(int row, int col)>();
var regions = new List<(char color, int area, int perimeter)>();

outerPerimeter.Enqueue((0, 0));

while (outerPerimeter.Count > 0)
{
    var startingPos = outerPerimeter.Dequeue();
    var currentRegion = (color: map[startingPos.row][startingPos.col], area: 0, perimeter: 0);
    perimeter.Enqueue(startingPos);

    while (perimeter.Count > 0)
    {
        var current = perimeter.Dequeue();

        if (currentRegion.color == map[current.row][current.col])
        {
            if (visited.Contains(current))
            {
                continue;
            }

            currentRegion.area += 1;

            if (current.row == 0)
            {
                currentRegion.perimeter += 1;
            }
            else
            {
                perimeter.Enqueue((current.row - 1, current.col));
            }

            if (current.col == 0)
            {
                currentRegion.perimeter += 1;
            }
            else
            {
                perimeter.Enqueue((current.row, current.col - 1));
            }

            if (current.row == map.Length - 1)
            {
                currentRegion.perimeter += 1;
            }
            else
            {
                perimeter.Enqueue((current.row + 1, current.col));
            }

            if (current.col == map[current.row].Length - 1)
            {
                currentRegion.perimeter += 1;
            }
            else
            {
                perimeter.Enqueue((current.row, current.col + 1));
            }

            visited.Add(current);
        }
        else
        {
            outerPerimeter.Enqueue(current);
            currentRegion.perimeter += 1;
        }
    }

    if (currentRegion.area > 0)
    {
        regions.Add(currentRegion);
    }
}

Console.WriteLine(regions.Sum(r => r.area * r.perimeter));

Console.WriteLine("day12 completed.");

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
