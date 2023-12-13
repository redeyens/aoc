var patterns = PuzzleInput().Split(string.Join(string.Empty, Environment.NewLine, Environment.NewLine), StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
var sum = 0;

foreach (var pattern in patterns)
{
    var lines = pattern.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    var mirrorColCandidates = Enumerable.Range(1, lines[0].Length - 1).ToDictionary(x => x, y => 0);
    foreach (var line in lines)
    {
        foreach (var c in mirrorColCandidates.Keys)
        {
            mirrorColCandidates[c] += CountDiffsMirror(line, c);
        }
    }
    sum += mirrorColCandidates.Where(x => x.Value == 1).Select(x => x.Key).FirstOrDefault();

    var mirrorRow = Enumerable.Range(1, lines.Length - 1);
    sum += 100 * mirrorRow.Select(r => (r, d: ContDiffsMirrorLines(lines, r))).Where(x => x.d == 1).Select(x => x.r).FirstOrDefault();
}

System.Console.WriteLine(sum);
Console.WriteLine("day13 completed.");

static string TestInput() => GetLinesFromResource("day13.Input.TestInput.txt");
static string PuzzleInput() => GetLinesFromResource("day13.Input.PuzzleInput.txt");

static int CountDiffsMirror(string line, int left)
{
    int diffs = 0;
    for (int i = 1; left - i >= 0 && i + left - 1 < line.Length; i++)
    {
        if (line[left - i] != line[left + i - 1])
        {
            diffs++;
        }
    }
    return diffs;
}

static int ContDiffsMirrorLines(string[] lines, int top)
{
    int diffs = 0;
    for (int i = 1; top - i >= 0 && i + top - 1 < lines.Length; i++)
    {
        for (int j = 0; j < lines[top - i].Length; j++)
        {
            if (lines[top - i][j] != lines[top + i - 1][j])
            {
                diffs++;
            }
        }
    }
    return diffs;
}

static string GetLinesFromResource(string name)
{
    using Stream inStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(name) ?? throw new InvalidOperationException("Could not open input stream!");
    using TextReader inReader = new StreamReader(inStream);

    return inReader.ReadToEnd();
}
