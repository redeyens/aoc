using System.Collections.Generic;

namespace day19.Model
{
    internal class NestingRule : Rule
    {
        private int id;
        private Rule reference1;
        private Rule reference2;

        public NestingRule(int id, Rule reference1, Rule reference2)
        {
            this.id = id;
            this.reference1 = reference1;
            this.reference2 = reference2;
        }

        public override int Id => id;

        internal override IEnumerable<RuleMatch> Match(string line, int offset)
        {
            for(int i = 0; i < 100; i++)
            {
                foreach (var m1 in reference1.Match(line, offset, i))
                {
                    foreach (var m2 in reference2.Match(line, offset + m1.Length, i))
                    {
                        yield return new RuleMatch(offset, m1.Length + m2.Length);
                    }
                }
            }
            
        }
    }
}