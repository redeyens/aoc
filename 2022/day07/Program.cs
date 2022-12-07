long diskSize = 70000000;
long freeSpaceRequired = 30000000;
var root = new Directory("/", null, new List<FSNode>());
var current = root;

foreach (string line in PuzzleInput())
{
    var entry = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

    if (entry[0].Equals("$"))
    {
        if (entry[1].Equals("cd"))
        {
            switch (entry[2])
            {
                case "/":
                    current = root;
                    break;
                case "..":
                    current = current?.Parent ?? throw new InvalidOperationException($"Directory {current?.Name} has no parent.");
                    break;
                default:
                    current = current.Contents.OfType<Directory>().Where(d => d.Name.Equals(entry[2])).First();
                    break;
            }
        }
    }
    else if (entry[0].Equals("dir"))
    {
        current.Contents.Add(new Directory(entry[1], current, new List<FSNode>()));
    }
    else
    {
        current.Contents.Add(new File(entry[1], current, long.Parse(entry[0])));
    }
}

long currentFreeSpace = diskSize - root.GetSize();
long needToFree = freeSpaceRequired - currentFreeSpace;

Console.WriteLine(root.Find(e => e.GetSize() >= needToFree).OrderBy(e => e.GetSize()).First().GetSize());

Console.WriteLine();
Console.WriteLine("day07 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day07.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day07.Input.PuzzleInput.txt");

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

abstract record FSNode(string Name, Directory? Parent)
{
    public abstract long GetSize();

    protected int IndentLevel => (Parent is null) ? 0 : Parent.IndentLevel + 1;
}

record Directory(string Name, Directory? Parent, List<FSNode> Contents) : FSNode(Name, Parent)
{
    public override long GetSize() => Contents.Sum(d => d.GetSize());

    public IEnumerable<FSNode> Find(Func<FSNode, bool> criteria)
    {
        IEnumerable<FSNode> contentResults = Contents.OfType<Directory>().SelectMany(d => d.Find(criteria));

        if (criteria(this))
        {
            return contentResults.Prepend(this);
        }
        else
        {
            return contentResults;
        }
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, Contents.Select(e => e.ToString()).Prepend(string.Join(string.Empty, Enumerable.Repeat("  ", IndentLevel).Append($"- {Name} (dir, size={GetSize()})"))));
    }
}

record File(string Name, Directory Parent, long Size) : FSNode(Name, Parent)
{
    public override long GetSize() => Size;

    public override string ToString()
    {
        return string.Join(string.Empty, Enumerable.Repeat("  ", IndentLevel).Append($"- {Name} (file, size={GetSize()})"));
    }
}
