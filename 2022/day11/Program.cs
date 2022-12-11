using System.Linq.Expressions;

var monkeys = new Monkey[] { };
monkeys = new Monkey[] {
    // new Monkey(new int[] {79, 98 }, old => old * 19, w => w % 23 == 0, item => ThrowTo(2, item), item => ThrowTo(3, item)),
    // new Monkey(new int[] {54, 65, 75, 74 }, old => old + 6, w => w%19==0, item => ThrowTo(2, item), item => ThrowTo(0, item)),
    // new Monkey(new int[] {79, 60, 97 }, old => old*old, w => w%13==0, item => ThrowTo(1, item), item => ThrowTo(3, item)),
    // new Monkey(new int[] {74 }, old => old +3, w => w%17==0, item => ThrowTo(0, item), item => ThrowTo(1, item))

    new Monkey(new long[] {99, 67, 92, 61, 83, 64, 98 }, old => old * 17L, 3, item => ThrowTo(4, item), item => ThrowTo(2, item)),
    new Monkey(new long[] {78, 74, 88, 89, 50 }, old => old * 11L, 5, item => ThrowTo(3, item), item => ThrowTo(5, item)),
    new Monkey(new long[] {98, 91 }, old => old + 4L, 2, item => ThrowTo(6, item), item => ThrowTo(4, item)),
    new Monkey(new long[] {59, 72, 94, 91, 79, 88, 94, 51 }, old => old * old, 13, item => ThrowTo(0, item), item => ThrowTo(5, item)),
    new Monkey(new long[] {95, 72, 78 }, old => old + 7L, 11, item => ThrowTo(7, item), item => ThrowTo(6, item)),
    new Monkey(new long[] {76 }, old => old + 8L, 17, item => ThrowTo(0, item), item => ThrowTo(2, item)),
    new Monkey(new long[] {69, 60, 53, 89, 71, 88 }, old => old + 5L, 19, item => ThrowTo(7, item), item => ThrowTo(1, item)),
    new Monkey(new long[] {72, 54, 63, 80 }, old => old + 3L, 7, item => ThrowTo(1, item), item => ThrowTo(3 ,item)),
};

for (int i = 0; i < 10000; i++)
{
    PlayOneRound(monkeys);
}

for (int i = 0; i < monkeys.Length; i++)
{
    Console.WriteLine($"Monkey {i} inspected items {monkeys[i].Inspections} times.");
}

Console.WriteLine(monkeys.OrderByDescending(m => m.Inspections).Take(2).Aggregate(1L, (prod, m) => prod *= m.Inspections));

void ThrowTo(int index, long item)
{
    monkeys[index].Catch(item);
}

void PlayOneRound(IEnumerable<Monkey> monkeys)
{
    foreach (var monkey in monkeys)
    {
        monkey.PlayOneTurn();
    }
}

public class Monkey
{
    Queue<long> items;
    Func<long, long> CalculateWorryLevel;
    private readonly long div;
    Action<long> ActionIfTrue;
    Action<long> ActionIfFalse;

    public int Inspections { get; private set; }

    public Monkey(IEnumerable<long> items, Expression<Func<long, long>> calculateWorryLevel, int div, Action<long> actionIfTrue, Action<long> actionIfFalse)
    {
        this.items = new Queue<long>(items);
        CalculateWorryLevel = GenerateDelegate(calculateWorryLevel, 9699690);
        this.div = div;
        ActionIfTrue = actionIfTrue;
        ActionIfFalse = actionIfFalse;
    }

    private static Func<long, long> GenerateDelegate(Expression<Func<long, long>> calculateWorryLevel, long div)
    {
        var newExpressionLeft = Expression.Modulo(((BinaryExpression)calculateWorryLevel.Body).Left, Expression.Constant(div));
        var newExpressionRight = Expression.Modulo(((BinaryExpression)calculateWorryLevel.Body).Right, Expression.Constant(div));
        var newExpressionOp =
        (((BinaryExpression)calculateWorryLevel.Body).NodeType) switch
        {
            ExpressionType.Add => Expression.Add(newExpressionLeft, newExpressionRight),
            ExpressionType.Multiply => Expression.Multiply(newExpressionLeft, newExpressionRight),
            _ => throw new ArgumentException()
        };
        var newExpression = Expression.Lambda<Func<long, long>>(Expression.Modulo(newExpressionOp, Expression.Constant(div)), calculateWorryLevel.Parameters);
        return newExpression.Compile();
    }

    public void PlayOneTurn()
    {
        while (items.Count > 0)
        {
            var itemWorryLevel = items.Dequeue();
            var newWorryLevel = CalculateWorryLevel(itemWorryLevel);
            Inspections++;
            if (TestWorry(newWorryLevel))
            {
                ActionIfTrue(newWorryLevel);
            }
            else
            {
                ActionIfFalse(newWorryLevel);
            }
        }
    }

    private bool TestWorry(long newWorryLevel) => newWorryLevel % div == 0;

    internal void Catch(long item)
    {
        items.Enqueue(item);
    }
}