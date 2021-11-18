using System.Collections.Generic;

namespace day19.Model
{
    internal class RepeatingRule : Rule
    {
        private int id;
        private Rule reference;

        public RepeatingRule(int id, Rule reference)
        {
            this.id = id;
            this.reference = reference;
        }

        public override int Id => id;

        internal override IEnumerable<RuleMatch> Match(string line, int offset)
        {
            RuleMatch lastMatch = default(RuleMatch);
            bool canContinue = false;
            
            do
            {
                canContinue = false;
                foreach(var m in reference.Match(line, offset + lastMatch.Length))
                {
                    canContinue = true;
                    lastMatch = new RuleMatch(offset, lastMatch.Length + m.Length);
                    yield return lastMatch;
                }
            }
            while (canContinue);
        }

        public override string ToString()
        {
            return string.Format("{0}: {1} | {1} {0}", id, reference.Id);
        }
    }
}