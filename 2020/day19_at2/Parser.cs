using System;
using System.Collections.Generic;
using System.Linq;

namespace day19_at2
{
    internal class Parser
    {
        private Dictionary<string, Rule> grammar = new Dictionary<string, Rule>();

        private Parser(){ }

        internal static Parser FromGrammar(string grammarString)
        {
            var result = new Parser();

            result.Update(grammarString);

            return result;
        }

        internal void Update(string newOrUpdatedRules)
        {
            foreach (var ruleString in newOrUpdatedRules.Split(Environment.NewLine))
            {
                string[] ruleDef = ruleString.Split(':');
                var rule = Rule.FromString(ruleDef[1], ruleId => grammar[ruleId]);

                grammar[ruleDef[0]] = rule;
            }
        }

        internal bool TryParse(string message, string ruleId)
        {
            return grammar[ruleId].MatchMessage(message).Where(residual => string.IsNullOrEmpty(residual)).Any();
        }
    }
}