foreach (string line in TestInput())
{
    var game = Game.From(line);
}
Console.WriteLine(TestInput().Select(l => Game.From(l)).Where(g => g.IsPossible(12, 13, 14)).Sum(g => g.Id));
Console.WriteLine(PuzzleInput().Select(l => Game.From(l)).Where(g => g.IsPossible(12, 13, 14)).Sum(g => g.Id));

Console.WriteLine(TestInput().Select(l => Game.From(l)).Sum(g => g.Power));
Console.WriteLine(PuzzleInput().Select(l => Game.From(l)).Sum(g => g.Power));

Console.WriteLine("day02 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day02.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day02.Input.PuzzleInput.txt");

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
internal class Game
{
    private readonly int id;
    private Dictionary<string, int> cubes = new Dictionary<string, int>();


    public Game(int id)
    {
        this.id = id;
    }

    public int Id => id;

    public int Power => cubes["red"] * cubes["green"] * cubes["blue"];

    internal static Game From(string line)
    {
        var mainParts = line.Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var id = int.Parse(mainParts[0].Split(' ')[1]);
        var gameSets = mainParts[1].Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var game = new Game(id);

        foreach (var set in gameSets)
        {
            var draw = set.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            foreach (var cubes in draw)
            {
                var cubeParts = cubes.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                game.SetCubes(cubeParts[1], int.Parse(cubeParts[0]));
            }
        }

        return game;
    }

    internal bool IsPossible(int redCnt, int greenCnt, int blueCnt) => cubes["red"] <= redCnt && cubes["green"] <= greenCnt && cubes["blue"] <= blueCnt;


    private void SetCubes(string color, int cnt)
    {
        if (!cubes.TryGetValue(color, out int prevCnt))
        {
            cubes[color] = cnt;
        }
        else
        {
            cubes[color] = Math.Max(prevCnt, cnt);
        }
    }
}

