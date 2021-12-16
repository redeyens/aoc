using System;
using System.Collections.Generic;
using System.Linq;

namespace day16
{
    internal abstract class Operator : Packet
    {
        protected int ver;
        private int typeId;
        protected List<Packet> subPackets;

        public Operator(int ver, int typeId, IEnumerable<Packet> packets)
        {
            this.ver = ver;
            this.typeId = typeId;
            this.subPackets = packets.ToList();
        }

        internal static Packet FromBitStream(int ver, int typeId, BitStream stream)
        {
            byte lenType = stream.Read(1);
            IEnumerable<Packet> subPackets = null;

            if(lenType == 0)
            {
                int substreamLen = stream.Read(7);
                substreamLen = substreamLen << 8 | stream.Read(8);
                subPackets = Packet.FromBitStream(stream.ReadSubstream(substreamLen));
            }
            else
            {
                int numSubPackets = stream.Read(3);
                numSubPackets = numSubPackets << 8 | stream.Read(8);
                subPackets = Packet.FromBitStream(stream).Take(numSubPackets);
            }

            switch (typeId)
            {
                case 0:
                    return new Sum(ver, typeId, subPackets);
                case 1:
                    return new Product(ver, typeId, subPackets);
                case 2:
                    return new Min(ver, typeId, subPackets);
                case 3:
                    return new Max(ver, typeId, subPackets);
                case 5:
                    return new GreaterThan(ver, typeId, subPackets);
                case 6:
                    return new LessThan(ver, typeId, subPackets);
                case 7:
                    return new EqualTo(ver, typeId, subPackets);
                default:
                    throw new ArgumentException("Unknown operator type.");
            }
        }

        internal override int SumVersions() => subPackets.Select(p => p.SumVersions()).Sum() + ver;
    }
}