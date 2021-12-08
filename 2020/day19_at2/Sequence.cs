using System;
using System.Collections.Generic;
using System.Linq;

namespace day19_at2
{
    internal class Sequence : Rule
    {
        private Rule firstRule;
        private Rule secondRule;
        private Func<string, Rule> ruleLookup;

        internal Sequence(IEnumerable<string> ruleSequence, Func<string, Rule> ruleLookup):base(null, ruleLookup)
        {
            this.firstRule = new Reference(ruleSequence.First(), ruleLookup);
            if(ruleSequence.Skip(1).Take(2).Count() == 1)
            {
                this.secondRule = new Reference(ruleSequence.Skip(1).First(), ruleLookup);
            }
            else
            {
                this.secondRule = new Sequence(ruleSequence.Skip(1), ruleLookup);
            }

            this.ruleLookup = ruleLookup;
        }

        internal override IEnumerable<string> MatchMessage(string message)
        {
            return firstRule.MatchMessage(message).SelectMany(residual => secondRule.MatchMessage(residual));
        }
    }
}