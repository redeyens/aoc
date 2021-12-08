using System;
using System.Collections.Generic;
using System.Linq;

namespace day19_at2
{
    internal class Rule
    {
        private List<List<string>> options;
        private Func<string, Rule> ruleLookup;

        private Rule(List<List<string>> options, Func<string, Rule> ruleLookup)
        {
            this.options = options;
            this.ruleLookup = ruleLookup;
        }

        internal static KeyValuePair<string, Rule> FromString(string rule, Func<string, Rule> ruleLookup)
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

        internal IEnumerable<string> MatchMessage(string message)
        {
            // we need to temporarily redefine literal untill we sort out parsing
            // it will be the only entry in the only sequence that is not a number
            IEnumerable<string> terminals = options.SelectMany(options => options);
            if (terminals.Count() == 1 && terminals.Where(t => char.IsLetter(t[0])).Any())
            {
                return MatchLiteral(terminals.First(), message);
            }
            else
            {
                return MatchOptions(options, message);
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
            Rule firstRule = ruleLookup(ruleSequence[0]);
            if(ruleSequence.Count == 1)
            {
                return firstRule.MatchMessage(message);
            }
            else
            {
                return firstRule.MatchMessage(message).SelectMany(residual => MatchSequence(new List<string>(ruleSequence.Skip(1)), residual));
            }
        }
    }
}