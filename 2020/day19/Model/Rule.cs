using System;
using System.Collections.Generic;
using System.Linq;

namespace day19.Model
{
    internal abstract class Rule
    {
        public abstract int Id { get; }

        internal bool IsExactMatch(string line)
        {
            return Match(line, 0).Where(m => m.Length == line.Length).Any();
        }

        internal abstract IEnumerable<RuleMatch> Match(string line, int offset);

        internal IEnumerable<RuleMatch> Match(string line, int offset, int times)
        {
            IEnumerable<RuleMatch> lastMatches = new RuleMatch[] {new RuleMatch(offset, 0)};
            for (int i = 0; i < times; i++)
            {
                var newMatches = new List<RuleMatch>();
                foreach (var lm in lastMatches)
                {
                    newMatches.AddRange(Match(line, offset + lm.Length).Select(m => new RuleMatch(offset, lm.Length + m.Length)));   
                }
                lastMatches = newMatches;
            }

            return lastMatches;
        }
    }
}