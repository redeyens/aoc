using System.Collections.Generic;
using System.Linq;

namespace day19.Model
{
    internal class OptionRule : Rule
    {
        private int id;
        private Rule option1;
        private Rule option2;

        public OptionRule(int id, Rule option1, Rule option2)
        {
            this.id = id;
            this.option1 = option1;
            this.option2 = option2;
        }

        public override int Id => id;

        public override string ToString()
        {
            return string.Format("{0}: {1} | {2}", id, option1, option2);
        }

        internal override IEnumerable<RuleMatch> Match(string line, int offset)
        {
            return option1.Match(line, offset).Concat(option2.Match(line, offset));
        }
    }
}