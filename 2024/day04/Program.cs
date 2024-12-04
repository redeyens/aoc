var lines = PuzzleInput().ToList();
int res = 0;

for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        if (j < lines[i].Length - 3)
        {
            var w = lines[i].Substring(j, 4);
            if (w.Equals("XMAS") || w.Equals("SAMX"))
            {
                res++;
            }
        }

        if (i < lines.Count - 3)
        {
            var c = new char[4];
            for (int k = 0; k < 4; k++)
            {
                c[k] = lines[i + k][j];
            }
            var w = new string(c);
            if (w.Equals("XMAS") || w.Equals("SAMX"))
            {
                res++;
            }
        }

        if (j < lines[i].Length - 3 && i < lines.Count - 3)
        {
            var c = new char[4];
            for (int k = 0; k < 4; k++)
            {
                c[k] = lines[i + k][j + k];
            }
            var w = new string(c);
            if (w.Equals("XMAS") || w.Equals("SAMX"))
            {
                res++;
            }
        }

        if (j > 2 && i < lines.Count - 3)
        {
            var c = new char[4];
            for (int k = 0; k < 4; k++)
            {
                c[k] = lines[i + k][j - k];
            }
            var w = new string(c);
            if (w.Equals("XMAS") || w.Equals("SAMX"))
            {
                res++;
            }
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
