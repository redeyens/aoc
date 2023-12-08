var input = PuzzleInput();

var handsValue = input.Select(line => line.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
    .Select(a => (hand: a[0], bid: int.Parse(a[1]), val: GetHandValue(a[0])))
    .OrderBy(h => h, new HandComparer())
    .Select((x, i) => x.bid * (i + 1L))
    .Sum();

System.Console.WriteLine(handsValue);
Console.WriteLine("day07 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day07.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day07.Input.PuzzleInput.txt");

static int GetHandValue(string hand) => hand.GroupBy(c => c).GroupBy(g => g.Count()).Select(g => g.Count() << ((g.Key - 1) * 3)).Sum();

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

internal class HandComparer : IComparer<(string hand, int bid, int val)>
{
    private List<char> cards = new List<char>() { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };

    public int Compare((string hand, int bid, int val) x, (string hand, int bid, int val) y)
    {
        return x.val == y.val ? CompareHands(x.hand, y.hand) : x.val.CompareTo(y.val);
    }

    private int CompareHands(string hand1, string hand2)
    {
        for (int i = 0; i < hand1.Length; i++)
        {
            int res = cards.IndexOf(hand2[i]).CompareTo(cards.IndexOf(hand1[i]));
            if (res != 0)
            {
                return res;
            }
        }
        return 0;
    }
}
