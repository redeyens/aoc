using System.Collections.Generic;
using System.Linq;

namespace day16
{
    internal class Sum : Operator
    {
        public Sum(int ver, int typeId, IEnumerable<Packet> subPackets):base(ver, typeId, subPackets){}

        internal override long Value => subPackets.Select(p => p.Value).Sum();
    }
}