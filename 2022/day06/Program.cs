var signalBuffer = PuzzleInput().First();

Console.WriteLine(GetPacketStart(signalBuffer));

Console.WriteLine("day06 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day06.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day06.Input.PuzzleInput.txt");

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

static int GetPacketStart(string signalBuffer)
{
    var window = signalBuffer[0..4].ToCharArray();
    int wIndex = window.Length - 1;

    for (int i = wIndex; i < signalBuffer.Length; i++)
    {
        window[wIndex] = signalBuffer[i];
        wIndex = (wIndex + 1) % window.Length;

        if (!window.GroupBy(c => c).Where(g => g.Count() > 1).Any())
        {
            return i + 1;
        }
    }

    return 0;
}