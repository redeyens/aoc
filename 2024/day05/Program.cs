var input = string.Join(Environment.NewLine, PuzzleInput());
var parts = input.Split(Environment.NewLine + Environment.NewLine);

var rules = new HashSet<string>(parts[0].Split(Environment.NewLine));

var res = 0;

foreach (var manual in parts[1].Split(Environment.NewLine))
{
    var pages = manual.Split(",");
    if (IsManualCorrected(rules, pages))
    {
        res += int.Parse(pages[pages.Length / 2]);
    }
}

System.Console.WriteLine(res);

Console.WriteLine("day05 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day05.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day05.Input.PuzzleInput.txt");

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

static bool IsManualCorrected(HashSet<string> rules, string[] pages)
{
    var wasCorrected = false;
    for (int i = 0; i < pages.Length - 1; i++)
    {
        var inversionPerformed = false;
        for (int j = i + 1; j < pages.Length; j++)
        {
            var orderedPair = string.Join("|", pages[j], pages[i]);
            if (rules.Contains(orderedPair))
            {
                (pages[i], pages[j]) = (pages[j], pages[i]);
                wasCorrected = true;
                inversionPerformed = true;
                break;
            }
        }
        if (inversionPerformed)
        {
            i--;
        }
    }

    return wasCorrected;
}