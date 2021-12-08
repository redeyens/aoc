using System;
using System.Collections.Generic;
using System.Linq;

namespace day19_at2
{
    internal class Options : Rule
    {
        private Rule firstOption;
        private Rule secondOption;

        public Options(IEnumerable<Rule> options):base(null, null)
        {
            this.firstOption = options.First();
            this.secondOption = options.Skip(1).First();
        }

        internal override IEnumerable<string> MatchMessage(string message)
        {
            return firstOption.MatchMessage(message).Concat(secondOption.MatchMessage(message));
        }
    }
}