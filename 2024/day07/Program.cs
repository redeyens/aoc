﻿long result = 0;
foreach (string line in PuzzleInput())
{
    var equation = line.Split(':', StringSplitOptions.TrimEntries);
    var elements = equation[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).Reverse().ToList();
    var testResult = long.Parse(equation[0]);
    var bitCount = elements.Count - 1;
    if (Enumerable.Range(0, 1 << 2 * bitCount).Any(operationMap => ProducesTestValue(testResult, elements, operationMap)))
    {
        result += testResult;
    }
}

Console.WriteLine(result);

Console.WriteLine("day07 completed.");

bool ProducesTestValue(long testResult, List<long> elements, int operationMap)
{
    var state = new Stack<long>(elements);
    while (state.Count > 1)
    {
        var currentOperation = operationMap & 3;
        switch (currentOperation)
        {
            case 0:
                state.Push(state.Pop() + state.Pop());
                break;
            case 1:
                state.Push(state.Pop() * state.Pop());
                break;
            case 2:
                var num = state.Pop().ToString();
                num += state.Pop().ToString();
                state.Push(long.Parse(num));
                break;
            default: return false;
        }
        if (state.Peek() > testResult)
        {
            return false;
        }
        operationMap >>= 2;
    }

    return testResult == state.Peek();
}

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
