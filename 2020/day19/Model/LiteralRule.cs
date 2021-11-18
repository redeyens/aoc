using System.Collections.Generic;

namespace day19.Model
{
    internal class LiteralRule : Rule
    {
        private readonly int id;
        private readonly char val;

        public LiteralRule(int id, char val)
        {
            this.id = id;
            this.val = val;
        }

        public override int Id => id;

        public override string ToString()
        {
            return string.Format(@"{0}: ""{1}""", id, val);
        }

        internal override IEnumerable<RuleMatch> Match(string line, int offset)
        {
            if(offset < line.Length && line[offset] == val)
            {
                yield return new RuleMatch(offset, 1);
            }
        }
    }
}