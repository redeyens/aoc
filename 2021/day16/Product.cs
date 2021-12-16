using System.Collections.Generic;
using System.Linq;

namespace day16
{
    internal class Product : Operator
    {
        public Product(int ver, int typeId, IEnumerable<Packet> subPackets):base(ver, typeId, subPackets){}

        internal override long Value => subPackets.Select(p => p.Value).Aggregate(1L, (total, next) => total *= next);
    }
}