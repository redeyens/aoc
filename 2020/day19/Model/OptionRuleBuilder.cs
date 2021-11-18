using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace day19.Model
{
    internal class OptionRuleBuilder : RuleBuilder
    {
        readonly Regex template = new Regex(@"^(\d+):(\s\d+)+ \|(\s\d+)+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private Match m;
        private Rule[] references1;
        private Rule[] references2;
        
        internal override Rule Build()
        {
            int id = Int32.Parse(m.Groups[1].Value);
            return new OptionRule(id, new SequenceRule(10000 + id, references1), new SequenceRule(20000 + id, references2));
        }

        internal override bool ReadDefinition(string ruleDef)
        {
            m = template.Match(ruleDef);
            return m.Success;
        }

        internal override bool ResolveReferences(Rule[] rules)
        {
            var missingReferences = m.Groups[2].Captures
                .Concat(m.Groups[3].Captures)
                .Select(c => Int32.Parse(c.Value))
                .Where(rid => rules[rid] == null);
            
            if(missingReferences.Any())
            {
                return false;
            }
            
            references1 = m.Groups[2].Captures
                .Select(c => Int32.Parse(c.Value))
                .Select(rid => rules[rid])
                .ToArray();
            
            references2 = m.Groups[3].Captures
                .Select(c => Int32.Parse(c.Value))
                .Select(rid => rules[rid])
                .ToArray();

            return true;
        }
    }
}