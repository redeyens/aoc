var input = PuzzleInput();
var instructions = input.First().ToCharArray().Select(c => c == 'L' ? 0 : 1).ToList();
var map = input.Skip(2)
    .Select(l => l.Split("=", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
    .Select(arr => (k: arr[0], v: arr[1].Replace("(", string.Empty).Replace(")", string.Empty).Split(",", StringSplitOptions.TrimEntries)))
    .ToDictionary(x => x.k, x => x.v);

var currentElement = "AAA";
var steps = 0;

while (!currentElement.Equals("ZZZ"))
{
    currentElement = map[currentElement][instructions[steps++ % instructions.Count]];
}

Console.WriteLine(steps);
Console.WriteLine("day08 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day08.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day08.Input.PuzzleInput.txt");

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
