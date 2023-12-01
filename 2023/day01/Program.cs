long result = 0;
foreach (string line in PuzzleInput())
{
    var l = line;
    l = l.Replace("one", "o1ne");
    l = l.Replace("two", "t2wo");
    l = l.Replace("three", "th3ree");
    l = l.Replace("four", "fo4ur");
    l = l.Replace("five", "fi5ve");
    l = l.Replace("six", "si6x");
    l = l.Replace("seven", "se7ven");
    l = l.Replace("eight", "ei8ght");
    l = l.Replace("nine", "ni9ne");

    var digits = l.Where(c => char.IsDigit(c)).ToList();
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
