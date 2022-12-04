var input = PuzzleInput().ToList();

var badges = new List<char>();
for (int g = 0; g < input.Count / 3; g++)
{
    int i = g * 3;
    var first = input[i++];
    var second = input[i++];
    var third = input[i++];
    for (int j = 0; j < first.Length; j++)
    {
        char b = first[j];
        if (second.IndexOf(b) >= 0 && third.IndexOf(b) >= 0)
        {
            badges.Add(b);
            break;
        }
    }
}

Console.WriteLine(badges.Select(GetPriority).Sum());

int GetPriority(char item)
{
    if (item >= 'a' && item <= 'z')
    {
        return item - 'a' + 1;
    }
    return item - 'A' + 27;
}

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
