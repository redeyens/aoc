int cycles = 0;
long regX = 1;
long sigStr = 0;
var checkpoints = new int[] { 20, 60, 100, 140, 180, 220 };
int checkIndex = 0;

foreach (string line in PuzzleInput())
{
    var cmd = line.Split(' ');
    if (cmd[0].Equals("noop"))
    {
        cycles++;
        if (checkIndex < 6 && cycles % checkpoints[checkIndex] == 0)
        {
            sigStr += cycles * regX;
            checkIndex++;
        }
    }
    else
    {
        cycles += 2;
        if (checkIndex < 6)
        {
            var c = cycles % checkpoints[checkIndex];
            if (c == 0 || c == 1)
            {
                sigStr += (cycles / checkpoints[checkIndex]) * checkpoints[checkIndex] * regX;
                checkIndex++;
            }
        }
        regX += int.Parse(cmd[1]);
    }
}

Console.WriteLine(sigStr);

Console.WriteLine("day10 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day10.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day10.Input.PuzzleInput.txt");

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
