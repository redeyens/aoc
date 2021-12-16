using System;
using System.Collections.Generic;
using System.Linq;

namespace day16
{
    internal class Operator : Packet
    {
        private int ver;
        private int typeId;
        private List<Packet> subPackets;

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

            return new Operator(ver, typeId, subPackets);
        }

        internal override int SumVersions() => subPackets.Select(p => p.SumVersions()).Sum() + ver;
    }
}