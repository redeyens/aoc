Console.WriteLine(PuzzleInput().Select(GetDoubledItem).Select(GetPriority).Sum());

int GetPriority(char item)
{
    if (item >= 'a' && item <= 'z')
    {
        return item - 'a' + 1;
    }
    return item - 'A' + 27;
}

char GetDoubledItem(string rucksack)
{
    var firstCompartment = new HashSet<int>();
    int mid = rucksack.Length / 2;
    for (int i = 0; i < mid; i++)
    {
        firstCompartment.Add(rucksack[i]);
    }
    for (int i = mid; i < rucksack.Length; i++)
    {
        var item = rucksack[i];
        if (firstCompartment.Contains(item))
        {
            return item;
        }
    }
    return default;
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
