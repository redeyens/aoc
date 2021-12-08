using System;
using System.Collections.Generic;

namespace day19_at2
{
    internal class Literal : Rule
    {
        private string value;

        internal Literal(string value)
        {
            this.value = value;
        }

        internal override IEnumerable<string> MatchMessage(string message)
        {
            if(message.StartsWith(value))
            {
                yield return message.Substring(value.Length);
            }
        }
    }
}