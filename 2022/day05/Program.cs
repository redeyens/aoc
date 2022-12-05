var input = PuzzleInput();
var startingConfiguration = input.TakeWhile(line => !string.IsNullOrEmpty(line)).ToList();

var stackIndexes = startingConfiguration.Last().Select((c, i) => (c != ' ') ? i : -1).Where(i => i != -1).ToList();

var stacks = new Stack<char>[stackIndexes.Count];

for (int i = 0; i < stacks.Length; i++)
{
    stacks[i] = new Stack<char>();
}

for (int i = startingConfiguration.Count - 2; i >= 0; i--)
{
    var line = startingConfiguration[i];
    for (int j = 0; j < stackIndexes.Count; j++)
    {
        var crate = line[stackIndexes[j]];
        if (crate != ' ')
        {
            stacks[j].Push(crate);
        }
    }
}

foreach (string line in input.Skip(startingConfiguration.Count + 1))
{
    var cmd = line.Split(' ');
    var cnt = int.Parse(cmd[1]);
    var src = int.Parse(cmd[3]) - 1;
    var dst = int.Parse(cmd[5]) - 1;
    var tmp = new Stack<char>();

    for (int i = 0; i < cnt; i++)
    {
        tmp.Push(stacks[src].Pop());
    }
    while (tmp.Count > 0)
    {
        stacks[dst].Push(tmp.Pop());
    }
}

foreach (var stack in stacks)
{
    Console.Write(stack.Peek());
}
Console.WriteLine();

Console.WriteLine("day05 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day05.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day05.Input.PuzzleInput.txt");

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
