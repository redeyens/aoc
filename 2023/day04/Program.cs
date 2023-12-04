var totalVal = 0;
var cardStack = new Stack<int>();

foreach (string line in PuzzleInput())
{
    var thisCardCopies = 1;
    if (cardStack.Count > 0)
    {
        thisCardCopies += cardStack.Pop();
    }

    var splitOpt = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
    var numbers = line.Split(":", splitOpt)[1].Split("|", splitOpt);
    var winningNumbers = numbers[0].Split(" ", splitOpt).Select(s => int.Parse(s));
    var myNumbers = numbers[1].Split(" ", splitOpt).Select(s => int.Parse(s));

    var cardsWon = myNumbers.Intersect(winningNumbers).Count();
    var innerCardStack = new Stack<int>();
    for (int i = 0; i < cardsWon; i++)
    {
        innerCardStack.Push(cardStack.Count > 0 ? cardStack.Pop() : 0);
    }
    while (innerCardStack.Count > 0)
    {
        cardStack.Push(innerCardStack.Pop() + thisCardCopies);
    }

    totalVal += thisCardCopies;
}

Console.WriteLine(totalVal);
Console.WriteLine("day04 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day04.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day04.Input.PuzzleInput.txt");

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
