
class RopeModel
{
    private (int x, int y)[] knots = new (int x, int y)[10];
    private readonly HashSet<(int x, int y)> tailHistory = new();

    internal void MoveHead(char dir, int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            knots[0] = dir switch
            {
                'U' => (knots[0].x, knots[0].y + 1),
                'D' => (knots[0].x, knots[0].y - 1),
                'R' => (knots[0].x + 1, knots[0].y),
                'L' => (knots[0].x - 1, knots[0].y),
                _ => throw new ArgumentException($"Unknown direction '{dir}'.")
            };

            for (int j = 1; j < knots.Length; j++)
            {
                var dx = knots[j - 1].x - knots[j].x;
                var dy = knots[j - 1].y - knots[j].y;

                if (Math.Abs(dx) > 1)
                {
                    knots[j] = (knots[j].x + Math.Sign(dx), knots[j].y + Math.Sign(dy));
                }
                else if (Math.Abs(dy) > 1)
                {
                    knots[j] = (knots[j].x + Math.Sign(dx), knots[j].y + Math.Sign(dy));
                }
            }

            tailHistory.Add(knots[knots.Length - 1]);
        }
    }

    public int Score => tailHistory.Count;
}
