using System;
using System.Collections.Generic;

namespace day19_at2
{
    internal class Reference : Rule
    {
        private string ruleId;
        private Func<string, Rule> ruleLookup;

        public Reference(string ruleId, Func<string, Rule> ruleLookup)
        {
            this.ruleId = ruleId;
            this.ruleLookup = ruleLookup;
        }

        internal override IEnumerable<string> MatchMessage(string message)
        {
            return ruleLookup(ruleId).MatchMessage(message);
        }
    }
}