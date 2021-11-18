using System.Collections.Generic;
using System.Linq;

namespace day19.Model
{
    internal class SequenceRule : Rule
    {
        private readonly int id;
        private readonly Rule[] references;

        public SequenceRule(int id, Rule[] references)
        {
            this.id = id;
            this.references = references;
        }

        public override int Id => id;

        public override string ToString()
        {
            if(id >= 10000)
            {
                return string.Format(string.Join(" ", references.Select(r => r.Id)));
            }
            return string.Format("{0}: {1}", id, string.Join(" ", references.Select(r => r.Id)));
        }

        internal override IEnumerable<RuleMatch> Match(string line, int offset)
        {
            ICollection<RuleMatch> lastMatches = new RuleMatch[] {new RuleMatch(offset, 0)};
            for(int i = 0; i < references.Length && lastMatches.Count > 0; i++)
            {
                var newMatches = new List<RuleMatch>();
                foreach(var lm in lastMatches)
                {
                    newMatches.AddRange(
                        references[i].Match(line, lm.Offset + lm.Length)
                        .Select(m => new RuleMatch(lm.Offset, lm.Length + m.Length))
                    );
                }

                lastMatches = newMatches;
            }

            return lastMatches;
        }
    }
}