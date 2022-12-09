
class RopeModel
{
    private (int x, int y) head = new();
    private (int x, int y) tail = new();
    private readonly HashSet<(int x, int y)> tailHistory = new();

    internal void MoveHead(char dir, int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            head = dir switch
            {
                'U' => (head.x, head.y + 1),
                'D' => (head.x, head.y - 1),
                'R' => (head.x + 1, head.y),
                'L' => (head.x - 1, head.y),
                _ => throw new ArgumentException($"Unknown direction '{dir}'.")
            };

            var dx = head.x - tail.x;
            var dy = head.y - tail.y;

            if (Math.Abs(dx) > 1)
            {
                tail = (tail.x + Math.Sign(dx), tail.y + Math.Sign(dy));
            }
            if (Math.Abs(dy) > 1)
            {
                tail = (tail.x + Math.Sign(dx), tail.y + Math.Sign(dy));
            }

            tailHistory.Add(tail);
        }
    }

    public int Score => tailHistory.Count;
}
