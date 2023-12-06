
var input = PuzzleInput();
var time = long.Parse(input.First().Split(":", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[1].Replace(" ", string.Empty));
var record = long.Parse(input.Skip(1).First().Split(":", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[1].Replace(" ", string.Empty));
var combinations = 1L;

var minStart = FindMinStartTime(0, time, record, time);
var maxStart = time - minStart;
var numStarts = maxStart - minStart + 1;
combinations *= numStarts;

System.Console.WriteLine("---------------------------");
Console.WriteLine(combinations);
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

long FindMinStartTime(long minTime, long maxTime, long record, long raceTime)
{
    if (minTime >= maxTime)
    {
        return minTime;
    }
    var holdTime = (maxTime + minTime) / 2;
    var distance = holdTime * (raceTime - holdTime);
    if (distance > record)
    {
        return FindMinStartTime(minTime, holdTime, record, raceTime);
    }
    else
    {
        return FindMinStartTime(holdTime + 1, maxTime, record, raceTime);
    }
}

