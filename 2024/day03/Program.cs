using System.Text.RegularExpressions;

var re = new Regex(@"mul\((\d+),(\d+)\)", RegexOptions.Compiled);
long res = 0;

foreach (string line in PuzzleInput())
{
    foreach (Match match in re.Matches(line))
    {
        res += long.Parse(match.Groups[1].Value) * long.Parse(match.Groups[2].Value);
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
