using System;

namespace day19.Model
{
    internal abstract class RuleBuilder
    {
        internal abstract bool ReadDefinition(string ruleDef);

        internal abstract bool ResolveReferences(Rule[] rules);

        internal abstract Rule Build();
    }
}