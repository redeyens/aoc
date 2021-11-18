namespace day19.Model
{
    internal struct RuleMatch
    {
        public RuleMatch(int offset, int length)
        {
            Offset = offset;
            Length = length;
        }

        public int Length { get; private set; }
        public int Offset { get; private set;}
    }
}