var pair = new Packet[2];
int pi = 0;
int sum = 0;

foreach (string line in PuzzleInput())
{
    if (string.IsNullOrEmpty(line))
    {
        if (pair[0].CompareTo(pair[1]) < 0)
        {
            sum += pi / 2;
            Console.WriteLine(pair[0]);
            Console.WriteLine(pair[1]);
            Console.WriteLine();
        }
    }
    else
    {
        var p = Packet.Parse(line);
        pair[pi++ % 2] = p;
    }
}

Console.WriteLine(sum);

Console.WriteLine("day13 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day13.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day13.Input.PuzzleInput.txt");

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

abstract class Packet
{
    internal static Packet Parse(string line)
    {
        var packetStack = new Stack<Packet>();

        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == '[')
            {
                packetStack.Push(new List());
            }
            if (line[i] == ']')
            {
                if (packetStack.Count > 1)
                {
                    var p = packetStack.Pop();
                    packetStack.Peek().Add(p);
                }
            }

            int numLen = 0;
            while (char.IsDigit(line[i + numLen])) numLen++;
            if (numLen > 0)
            {
                packetStack.Peek().Add(new Number(int.Parse(line[i..(i + numLen)])));
                i += numLen - 1;
            }
        }

        return packetStack.Pop();
    }

    public abstract void Add(Packet p);

    internal abstract int CompareTo(Packet packet);
}

internal class Number : Packet
{
    public int Value { get; private set; }

    public Number(int v)
    {
        this.Value = v;
    }

    internal override int CompareTo(Packet packet)
    {
        if (packet is Number n)
        {
            return Value.CompareTo(n.Value);
        }

        return new List(this).CompareTo(packet);
    }

    public override void Add(Packet p)
    {
        throw new InvalidOperationException();
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

class List : Packet
{
    private List<Packet> elements = new List<Packet>();

    public List(Number number)
    {
        elements.Add(number);
    }

    public List()
    {
    }

    public override void Add(Packet p)
    {
        elements.Add(p);
    }

    internal override int CompareTo(Packet packet)
    {
        if (packet is Number n)
        {
            return CompareTo(new List(n));
        }

        if (packet is List other)
        {
            var count = Math.Min(elements.Count, other.elements.Count);
            for (int i = 0; i < count; i++)
            {
                var result = elements[i].CompareTo(other.elements[i]);
                if (result != 0) return result;
            }

            return (elements.Count == other.elements.Count)
                ? 0
                : (elements.Count == count)
                ? -1
                : 1;
        }
        return 0;
    }

    public override string ToString()
    {
        return '[' + string.Join(',', elements) + ']';
    }
}
