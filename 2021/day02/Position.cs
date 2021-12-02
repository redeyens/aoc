namespace day02
{
    internal struct Position
    {
        public int Depth { get; }
        public int HPos { get; }
        public Position(int depth, int hPos)
        {
            this.Depth = depth;
            this.HPos = hPos;
        }
    }
}