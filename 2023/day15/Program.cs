var total = 0;
var boxes = new Dictionary<int, LinkedList<(string label, int f)>>();
foreach (string line in PuzzleInput())
{
    foreach (var cmd in line.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
    {
        var actionIndex = cmd.IndexOf('=');
        var label = string.Empty;
        int f = 0;
        var action = '=';
        if (actionIndex >= 0)
        {
            var parts = cmd.Split('=');
            label = parts[0];
            f = int.Parse(parts[1]);
        }
        else
        {
            action = '-';
            label = cmd.Split('-')[0];
        }

        LinkedList<(string label, int f)> list;
        var key = GetHash(label);
        if (!boxes.TryGetValue(key, out list))
        {
            list = new LinkedList<(string label, int f)>();
            boxes[key] = list;
        }
        LinkedListNode<(string label, int f)> lens = list.First;
        while (lens != null)
        {
            if (lens.Value.label == label)
            {
                break;
            }
            lens = lens.Next;
        }
        if (action == '=')
        {
            if (lens == null)
            {
                list.AddLast((label, f));
            }
            else
            {
                lens.Value = (label, f);
            }
        }
        else
        {
            if (lens != null)
            {
                list.Remove(lens);
            }
        }
    }
}

total = boxes.SelectMany(b => b.Value.Select((lens, i) => (b.Key + 1) * (i + 1) * lens.f)).Sum();

Console.WriteLine(total);
Console.WriteLine("day15 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day15.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day15.Input.PuzzleInput.txt");

static int GetHash(string x)
{
    int current = 0;

    foreach (var c in x)
    {
        current += c;
        current *= 17;
        current %= 256;
    }

    return current;
}

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
