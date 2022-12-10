int cycles = 0;

static void PrintPixel(int cycles, int regX)
{
    int pos = regX - (cycles % 40);
    if (pos > -2 && pos < 2)
    {
        Console.Write("#");
    }
    else
    {
        Console.Write(".");
    }
}

static void CheckEndOfRow(int cycles, int regX, int[] checkpoints, ref int checkIndex)
{
    if (checkIndex < 6)
    {
        if (cycles % checkpoints[checkIndex] == 0)
        {
            checkIndex++;
            Console.WriteLine();
        }
    }
}

int regX = 1;
var checkpoints = new int[] { 40, 80, 120, 160, 200, 240 };
int checkIndex = 0;

foreach (string line in PuzzleInput())
{
    var cmd = line.Split(' ');
    if (cmd[0].Equals("noop"))
    {
        PrintPixel(cycles, regX);
        cycles++;
        CheckEndOfRow(cycles, regX, checkpoints, ref checkIndex);
    }
    else
    {
        PrintPixel(cycles, regX);
        cycles++;
        CheckEndOfRow(cycles, regX, checkpoints, ref checkIndex);
        PrintPixel(cycles, regX);
        cycles++;
        CheckEndOfRow(cycles, regX, checkpoints, ref checkIndex);
        regX += int.Parse(cmd[1]);
    }
}

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
