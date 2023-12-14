using System.Text;

var rowSupport = Array.Empty<int>();
var input = PuzzleInput().ToList();
var maxCol = input[0].Length - 1;
var stones = input.SelectMany((l, r) => l.Select((x, c) => (m: x, r, c)).Where(x => x.m != '.'));
var uniqueStates = new Dictionary<string, int>();
uniqueStates[StonesToString(stones, maxCol)] = 0;

var firstCycle = 0;
var repeatedCycle = 0;
for (int i = 1; ; i++)
{
    stones = TiltNorth(stones);
    stones = TiltWest(stones);
    stones = TiltSouth(stones, input.Count - 1);
    stones = TiltEast(stones, maxCol).ToList();
    int prevCycle;
    string key = StonesToString(stones, maxCol);
    if (uniqueStates.TryGetValue(key, out prevCycle))
    {
        firstCycle = prevCycle;
        repeatedCycle = i;
        break;
    }
    uniqueStates[key] = i;
}

long cycles = (1000000000 - firstCycle) % (repeatedCycle - firstCycle);

for (int i = 0; i < cycles; i++)
{
    stones = TiltNorth(stones);
    stones = TiltWest(stones);
    stones = TiltSouth(stones, input.Count - 1);
    stones = TiltEast(stones, maxCol).ToList();
}

var totalLoad = stones.Where(x => x.m == 'O').Sum(x => input.Count - x.r);

Console.WriteLine(totalLoad);
Console.WriteLine("day14 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day14.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day14.Input.PuzzleInput.txt");

static IEnumerable<(char m, int r, int c)> TiltNorth(IEnumerable<(char m, int r, int c)> stones)
{
    foreach (var columnGroup in stones.GroupBy(x => x.c))
    {
        var currentRow = 0;
        foreach (var stone in columnGroup.OrderBy(x => x.r))
        {
            if (stone.m == 'O')
            {
                yield return (stone.m, currentRow++, stone.c);
            }
            else
            {
                currentRow = stone.r + 1;
                yield return stone;
            }
        }
    }
}

static IEnumerable<(char m, int r, int c)> TiltSouth(IEnumerable<(char m, int r, int c)> stones, int maxRow)
{
    foreach (var rowGroup in stones.GroupBy(x => x.c))
    {
        var currentRow = maxRow;
        foreach (var stone in rowGroup.OrderByDescending(x => x.r))
        {
            if (stone.m == 'O')
            {
                yield return (stone.m, currentRow--, stone.c);
            }
            else
            {
                currentRow = stone.r - 1;
                yield return stone;
            }
        }
    }
}

static IEnumerable<(char m, int r, int c)> TiltWest(IEnumerable<(char m, int r, int c)> stones)
{
    foreach (var rowGroup in stones.GroupBy(x => x.r))
    {
        var currentCol = 0;
        foreach (var stone in rowGroup.OrderBy(x => x.c))
        {
            if (stone.m == 'O')
            {
                yield return (stone.m, stone.r, currentCol++);
            }
            else
            {
                currentCol = stone.c + 1;
                yield return stone;
            }
        }
    }
}

static IEnumerable<(char m, int r, int c)> TiltEast(IEnumerable<(char m, int r, int c)> stones, int maxCol)
{
    foreach (var rowGroup in stones.GroupBy(x => x.r))
    {
        var currentCol = maxCol;
        foreach (var stone in rowGroup.OrderByDescending(x => x.c))
        {
            if (stone.m == 'O')
            {
                yield return (stone.m, stone.r, currentCol--);
            }
            else
            {
                currentCol = stone.c - 1;
                yield return stone;
            }
        }
    }
}

static void Print(IEnumerable<(char m, int r, int c)> stones, int maxCol)
{
    Console.WriteLine(StonesToString(stones, maxCol));
    Console.WriteLine();
}

static string StonesToString(IEnumerable<(char m, int r, int c)> stones, int maxCol)
{
    var lineBuilder = new StringBuilder();
    foreach (var line in stones.GroupBy(x => x.r).OrderBy(g => g.Key))
    {
        int currentCol = 0;
        foreach (var cc in line.OrderBy(x => x.c))
        {
            while (currentCol < cc.c)
            {
                lineBuilder.Append('.');
                currentCol++;
            }
            lineBuilder.Append(cc.m);
            currentCol++;
        }
        while (currentCol++ < maxCol + 1)
        {
            lineBuilder.Append('.');
        }
        lineBuilder.AppendLine();
    }
    return lineBuilder.ToString();
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
