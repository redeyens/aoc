using System.Text.RegularExpressions;

var regexNum = new Regex(@"(\d+)", RegexOptions.Compiled);
var regexSym = new Regex(@"([^\d\.]+)", RegexOptions.Compiled);

var prevNums = Enumerable.Empty<Match>();
var prevSym = Enumerable.Empty<Match>();
var gears = new Dictionary<Match, HashSet<Match>>();
foreach (string line in PuzzleInput())
{
    var currentNums = regexNum.Matches(line);
    var nums = prevNums.Concat(currentNums).ToList();
    var currentSym = regexSym.Matches(line);
    var sym = prevSym.Concat(currentSym).ToList();

    foreach (var g in sym.Where(s => s.Value.Equals("*")))
    {
        if (!gears.TryGetValue(g, out HashSet<Match> gearRatio))
        {
            gearRatio = new HashSet<Match>();
            gears[g] = gearRatio;
        }

        foreach (var num in nums.Where(n => g.Index >= n.Index - 1 && g.Index <= n.Index + n.Length))
        {
            gearRatio.Add(num);
        }
    }

    prevNums = currentNums;
    prevSym = currentSym;
}

Console.WriteLine(gears.Values.Where(gr => gr.Count == 2).Sum(g => g.Aggregate(1L, (product, next) => product * long.Parse(next.Value))));
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
