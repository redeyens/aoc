using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day19.Model
{
    internal class SequenceRuleBuilder : RuleBuilder
    {
        readonly Regex template = new Regex(@"^(\d+):(\s\d+)+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private Match m;
        private Rule[] references;
        
        internal override Rule Build()
        {
            return new SequenceRule(Int32.Parse(m.Groups[1].Value), references);
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
            
            references = m.Groups[2].Captures
                .Select(c => Int32.Parse(c.Value))
                .Select(rid => rules[rid])
                .ToArray();
            return true;
        }
    }
}