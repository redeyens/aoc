using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace day19.Model
{
    internal class RepeatingRuleBuilder : RuleBuilder
    {
        readonly Regex template = new Regex(@"^(\d+):(\s\d+)+ \|\2 \1$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private Match m;
        private Rule reference;

        internal override Rule Build()
        {
            return new RepeatingRule(Int32.Parse(m.Groups[1].Value), reference);
        }

        internal override bool ReadDefinition(string ruleDef)
        {
            m = template.Match(ruleDef);
            return m.Success;
        }

        internal override bool ResolveReferences(Rule[] rules)
        {
            var missingReferences = m.Groups[2].Captures
                .Select(c => Int32.Parse(c.Value))
                .Where(rid => rules[rid] == null);
            if(missingReferences.Any())
            {
                return false;
            }
            
            reference = m.Groups[2].Captures
                .Select(c => Int32.Parse(c.Value))
                .Select(rid => rules[rid])
                .First();
            return true;
        }
    }
}