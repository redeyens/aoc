using System;
using System.Collections.Generic;

namespace day08
{
    internal class SeventSegDisplay
    {
        private string[] pattern;
        private string[] output;

        private SeventSegDisplay(string[] pattern, string[] output)
        {
            this.pattern = pattern;
            this.output = output;
        }

        public string[] Output => output;

        internal static SeventSegDisplay FromString(string line)
        {
            string[] sequences = line.Split('|');
            string[] pattern = sequences[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] output = sequences[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return new SeventSegDisplay(pattern, output);
        }
    }
}