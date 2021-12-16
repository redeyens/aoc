using System.Collections.Generic;
using System.Linq;

namespace day16
{
    internal class LessThan : Operator
    {
        public LessThan(int ver, int typeId, IEnumerable<Packet> subPackets):base(ver, typeId, subPackets){}

        internal override long Value => subPackets.First().Value < subPackets.Skip(1).First().Value ? 1 : 0;
    }
}