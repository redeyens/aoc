using System.Text.RegularExpressions;

var regexNum = new Regex(@"(\d+)", RegexOptions.Compiled);
var regexSym = new Regex(@"([^\d\.]+)", RegexOptions.Compiled);

var prevNums = Enumerable.Empty<Match>();
var prevSym = Enumerable.Empty<Match>();
var partNums = new HashSet<Match>();
foreach (string line in PuzzleInput())
{
    var currentNums = regexNum.Matches(line);
    var nums = prevNums.Concat(currentNums).ToList();
    var currentSym = regexSym.Matches(line);
    var sym = prevSym.Concat(currentSym).ToList();

    foreach (var num in nums)
    {
        if (sym.Where(s => s.Index >= num.Index - 1 && s.Index <= num.Index + num.Length).Any())
        {
            partNums.Add(num);
        }
    }

    prevNums = currentNums;
    prevSym = currentSym;
}

Console.WriteLine(partNums.Sum(m => int.Parse(m.Value)));
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
