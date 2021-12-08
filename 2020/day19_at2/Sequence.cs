using System;
using System.Collections.Generic;
using System.Linq;

namespace day19_at2
{
    internal class Sequence : Rule
    {
        private Rule firstRule;
        private Rule secondRule;

        internal Sequence(IEnumerable<Rule> ruleSequence)
        {
            this.firstRule = ruleSequence.First();
            if(ruleSequence.Skip(1).Take(2).Count() == 1)
            {
                this.secondRule = ruleSequence.Skip(1).First();
            }
            else
            {
                this.secondRule = new Sequence(ruleSequence.Skip(1));
            }

        }

        internal override IEnumerable<string> MatchMessage(string message)
        {
            return firstRule.MatchMessage(message).SelectMany(residual => secondRule.MatchMessage(residual));
        }
    }
}