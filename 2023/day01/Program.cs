using System.Linq;

long result = 0;
foreach (string line in PuzzleInput())
{
    var digits = line.Where(c => char.IsDigit(c)).ToList();
    result += (digits.FirstOrDefault() - '0') * 10 + (digits.LastOrDefault() - '0');
}
Console.WriteLine(result);

Console.WriteLine("day01 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day01.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day01.Input.PuzzleInput.txt");

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
