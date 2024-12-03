using System.Text.RegularExpressions;

var re = new Regex(@"((mul)\((\d+),(\d+)\))|((do)\(\))|((don't)\(\))", RegexOptions.Compiled);
long res = 0;
bool enabled = true;

foreach (string line in PuzzleInput())
{
    foreach (Match match in re.Matches(line))
    {
        System.Console.WriteLine(match.Groups[0].Value);
        if (match.Groups[2].Value.Equals("mul"))
        {
            if (enabled)
            {
                res += long.Parse(match.Groups[3].Value) * long.Parse(match.Groups[4].Value);
            }
        }
        else if (match.Groups[6].Value.Equals("do"))
        {
            enabled = true;
        }
        else
        {
            enabled = false;
        }
    }
}

System.Console.WriteLine(res);

Console.WriteLine("day03 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day03.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day03.Input.PuzzleInput.txt");

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
