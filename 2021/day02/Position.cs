namespace day02
{
    internal struct Position
    {
        public int Depth { get; }
        public int HPos { get; }
        public int Aim { get; }
        public Position(int depth, int hPos, int aim)
        {
            this.Depth = depth;
            this.HPos = hPos;
            this.Aim = aim;
        }
    }
}