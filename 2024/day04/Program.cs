var lines = PuzzleInput().ToList();
int res = 0;

for (int i = 0; i < lines.Count - 2; i++)
{
    for (int j = 0; j < lines[i].Length - 2; j++)
    {
        var c1 = new char[3];
        var c2 = new char[3];
        for (int k = 0; k < 3; k++)
        {
            c1[k] = lines[i + k][j + k];
            c2[k] = lines[i + k][j + 2 - k];
        }
        var w1 = new string(c1);
        var w2 = new string(c2);
        if ((w1.Equals("MAS") || w1.Equals("SAM")) && (w2.Equals("MAS") || w2.Equals("SAM")))
        {
            res++;
        }
    }
}

System.Console.WriteLine(res);
Console.WriteLine("day04 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day04.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day04.Input.PuzzleInput.txt");

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
