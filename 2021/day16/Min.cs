using System.Collections.Generic;
using System.Linq;

namespace day16
{
    internal class Min : Operator
    {
        public Min(int ver, int typeId, IEnumerable<Packet> subPackets):base(ver, typeId, subPackets){}

        internal override long Value => subPackets.Select(p => p.Value).Min();
    }
}