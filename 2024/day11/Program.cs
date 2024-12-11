var stones = PuzzleInput().First().Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x));

for (int i = 0; i < 25; i++)
{
    stones = Blink(stones);
}

Console.WriteLine(stones.Count());

Console.WriteLine("day11 completed.");

IEnumerable<long> Blink(IEnumerable<long> stones)
{
    foreach (var stone in stones)
    {
        var stoneString = stone.ToString();
        if (stone == 0)
        {
            yield return 1;
        }
        else if (stoneString.Length % 2 == 0)
        {
            var half = stoneString.Length / 2;
            yield return long.Parse(stoneString[..half]);
            yield return long.Parse(stoneString[half..]);
        }
        else
        {
            yield return stone * 2024;
        }
    }
}

static IEnumerable<string> TestInput() => GetLinesFromResource("day11.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day11.Input.PuzzleInput.txt");

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
