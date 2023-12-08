var input = PuzzleInput();
var instructions = input.First().ToCharArray().Select(c => c == 'L' ? 0 : 1).ToList();
var map = input.Skip(2)
    .Select(l => l.Split("=", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
    .Select(arr => (k: arr[0], v: arr[1].Replace("(", string.Empty).Replace(")", string.Empty).Split(",", StringSplitOptions.TrimEntries)))
    .ToDictionary(x => x.k, x => x.v);

var currentElements = map.Keys.Where(k => k.EndsWith("A")).ToList();
var steps = 0;
var pathLen = new Dictionary<string, int>();

while (currentElements.Any())
{
    foreach (var e in currentElements.Where(e => e[2] == 'Z').ToList())
    {
        pathLen[e] = steps;
        currentElements.Remove(e);
    }
    currentElements = currentElements.Select(currentElement => map[currentElement][instructions[steps % instructions.Count]]).ToList();
    steps++;
}

Console.WriteLine(pathLen.Values.Aggregate(1L, (a, n) => Lcm(a, n)));
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

static long Gcd(long a, long b)
{
    while (b != 0)
    {
        (a, b) = (b, a % b);
    }
    return a;
}

static long Lcm(long a, long b)
{
    return a * b / Gcd(a, b);
}
