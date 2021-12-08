using System;
using System.Collections.Generic;
using System.Linq;

namespace day19_at2
{
    internal class Parser
    {
        private Dictionary<string, Rule> grammar;

        private Parser(Dictionary<string, Rule> grammar)
        {
            this.grammar = grammar;
        }

        internal static Parser FromGrammar(string grammarString)
        {
            Dictionary<string, Rule> grammar = new Dictionary<string, Rule>();

            foreach (var ruleString in grammarString.Split(Environment.NewLine))
            {
                var rule = ParseRule(ruleString, ruleId => grammar[ruleId]);

                grammar[rule.Key] = rule.Value;
            }

            return new Parser(grammar);
        }

        private static KeyValuePair<string, Rule> ParseRule(string rule, Func<string, Rule> ruleLookup)
        {
            string[] ruleDef = rule.Split(':');
            var ruleId = ruleDef[0];
            string[] optionsDef = ruleDef[1].Split('|');
            var options = new List<List<string>>(optionsDef.Length);
            foreach (var optionDef in optionsDef)
            {
                string[] sequenceDef = optionDef.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                List<string> sequence = new List<string>(sequenceDef.Length);
                sequence.AddRange(sequenceDef.Select(item => item.Trim('"')));
                options.Add(sequence);
            }

            return new KeyValuePair<string, Rule>(ruleId, new Rule(options, ruleLookup));
        }

        internal void Update(string newOrUpdatedRules)
        {
            foreach (var ruleString in newOrUpdatedRules.Split(Environment.NewLine))
            {
                var rule = ParseRule(ruleString, ruleId => grammar[ruleId]);

                grammar[rule.Key] = rule.Value;
            }
        }

        internal bool TryParse(string message, string rule)
        {
            return grammar["0"].MatchMessage(message).Where(residual => string.IsNullOrEmpty(residual)).Any();
        }
    }
}