var rowSupport = new int[0];
var input = PuzzleInput().ToList();
var totalLoad = 0;
var row = 0;
foreach (string line in input)
{
    if (rowSupport.Length == 0)
    {
        rowSupport = new int[line.Length];
    }
    for (int i = 0; i < line.Length; i++)
    {
        if (line[i] == 'O')
        {
            totalLoad += input.Count - rowSupport[i];
            rowSupport[i]++;
        }
        if (line[i] == '#')
        {
            rowSupport[i] = row + 1;
        }
    }

    row++;
}

Console.WriteLine(totalLoad);
Console.WriteLine("day14 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day14.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day14.Input.PuzzleInput.txt");

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
