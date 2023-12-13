var patterns = PuzzleInput().Split(string.Join(string.Empty, Environment.NewLine, Environment.NewLine), StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
var sum = 0;

foreach (var pattern in patterns)
{
    var lines = pattern.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    var mirrorCol = Enumerable.Range(1, lines[0].Length - 1).ToList();
    foreach (var line in lines)
    {
        mirrorCol = mirrorCol.Where(c => IsThisMirrorLine(line, c)).ToList();
        if (!mirrorCol.Any())
        {
            break;
        }
    }
    sum += mirrorCol.FirstOrDefault();

    var mirrorRow = Enumerable.Range(1, lines.Length - 1);
    sum += 100 * mirrorRow.Where(r => IsThisMirror(lines, r)).FirstOrDefault();
}

System.Console.WriteLine(sum);
Console.WriteLine("day13 completed.");

static string TestInput() => GetLinesFromResource("day13.Input.TestInput.txt");
static string PuzzleInput() => GetLinesFromResource("day13.Input.PuzzleInput.txt");

static bool IsThisMirrorLine(string line, int left)
{
    for (int i = 1; left - i >= 0 && i + left - 1 < line.Length; i++)
    {
        if (line[left - i] != line[left + i - 1])
        {
            return false;
        }
    }
    return true;
}

static bool IsThisMirror(string[] lines, int top)
{
    for (int i = 1; top - i >= 0 && i + top - 1 < lines.Length; i++)
    {
        if (!lines[top - i].Equals(lines[top + i - 1]))
        {
            return false;
        }
    }
    return true;
}

static string GetLinesFromResource(string name)
{
    using Stream inStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(name) ?? throw new InvalidOperationException("Could not open input stream!");
    using TextReader inReader = new StreamReader(inStream);

    return inReader.ReadToEnd();
}
