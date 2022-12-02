int[][] scores = new int[][] { new int[] { 3, 6, 0 }, new int[] { 0, 3, 6 }, new int[] { 6, 0, 3 } };

int score = 0;

foreach (string line in PuzzleInput())
{
    var play = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    int moveScore = play[1][0] - 'X' + 1;
    int outcomeScore = scores[play[0][0] - 'A'][play[1][0] - 'X'];
    Console.WriteLine($"{moveScore} + {outcomeScore} = {moveScore + outcomeScore}");
    score += moveScore + outcomeScore;
}

Console.WriteLine(score);

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
