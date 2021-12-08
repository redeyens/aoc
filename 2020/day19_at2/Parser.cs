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
                var rule = ParseRule(ruleString);

                grammar[rule.Key] = rule.Value;
            }

            return new Parser(grammar);
        }

        private static KeyValuePair<string, Rule> ParseRule(string rule)
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

            return new KeyValuePair<string, Rule>(ruleId, new Rule(options));
        }

        internal void Update(string newOrUpdatedRules)
        {
            foreach (var ruleString in newOrUpdatedRules.Split(Environment.NewLine))
            {
                var rule = ParseRule(ruleString);

                grammar[rule.Key] = rule.Value;
            }
        }

        internal bool TryParse(string message, string rule)
        {
            return MatchMessage("0", message).Where(residual => string.IsNullOrEmpty(residual)).Any();
        }

        private IEnumerable<string> MatchMessage(string startingRule, string message)
        {
            Rule ruleOptions = null;

            // if the grammar does not contain this rule then it must be literal
            if(!grammar.TryGetValue(startingRule, out ruleOptions))
            {
                return MatchLiteral(startingRule, message);
            }
            else
            {
                return MatchOptions(grammar[startingRule].Options, message);
            }
        }

        private IEnumerable<string> MatchLiteral(string literal, string message)
        {
            if(message.StartsWith(literal))
            {
                yield return message.Substring(literal.Length);
            }
        }
        private IEnumerable<string> MatchOptions(List<List<string>> ruleOptions, string message)
        {
            if(ruleOptions.Count == 1)
            {
                return MatchSequence(ruleOptions[0], message);
            }
            else
            {
                return MatchSequence(ruleOptions[0], message).Concat(MatchOptions(new List<List<string>>(ruleOptions.Skip(1)), message));
            }
        }
        private IEnumerable<string> MatchSequence(List<string> ruleSequence, string message)
        {
            if(ruleSequence.Count == 1)
            {
                return MatchMessage(ruleSequence[0], message);
            }
            else
            {
                return MatchMessage(ruleSequence[0], message).SelectMany(residual => MatchSequence(new List<string>(ruleSequence.Skip(1)), residual));
            }
        }
    }
}