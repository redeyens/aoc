using System.Collections.Concurrent;

var initialStones = PuzzleInput().First().Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x));
var stones = new Queue<(long stone, long count)>(initialStones.Select(x => (x, 1L)));
var visited = new ConcurrentDictionary<long, long>();
for (int i = 0; i < 75; i++)
{
    stones = Blink(stones);
}

Console.WriteLine(stones.Sum(x => x.count));

Console.WriteLine("day11 completed.");

Queue<(long stone, long count)> Blink(Queue<(long stone, long count)> stones)
{
    while (stones.Count > 0)
    {
        var current = stones.Dequeue();

        if (current.stone == 0)
        {
            visited.AddOrUpdate(1L, current.count, (s, c) => c + current.count);
        }
        else
        {
            var stoneString = current.stone.ToString();

            if (stoneString.Length % 2 == 0)
            {
                var half = stoneString.Length / 2;
                var left = long.Parse(stoneString[..half]);
                var right = long.Parse(stoneString[half..]);
                visited.AddOrUpdate(left, current.count, (s, c) => c + current.count);
                visited.AddOrUpdate(right, current.count, (s, c) => c + current.count);
            }
            else
            {
                visited.AddOrUpdate(current.stone * 2024, current.count, (s, c) => c + current.count);
            }
        }
    }

    foreach (var stone in visited)
    {
        stones.Enqueue((stone.Key, stone.Value));
    }
    visited.Clear();

    return stones;
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
