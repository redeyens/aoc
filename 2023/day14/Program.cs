var rowSupport = Array.Empty<int>();
var input = PuzzleInput().ToList();
var stones = input.SelectMany((l, r) => l.Select((x, c) => (m: x, r, c)).Where(x => x.m != '.'));
stones = TiltNorth(stones);

var totalLoad = stones.Where(x => x.m == 'O').Sum(x => input.Count - x.r);

Console.WriteLine(totalLoad);
Console.WriteLine("day14 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day14.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day14.Input.PuzzleInput.txt");

IEnumerable<(char m, int r, int c)> TiltNorth(IEnumerable<(char m, int r, int c)> stones)
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
