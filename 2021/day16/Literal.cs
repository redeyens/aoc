using System;

namespace day16
{
    internal class Literal : Packet
    {
        private int ver;
        private int typeId;
        private long num;

        public Literal(int ver, int typeId, long num)
        {
            this.ver = ver;
            this.typeId = typeId;
            this.num = num;
        }

        internal static Packet FromBitStream(int ver, int typeId, BitStream stream)
        {
            long num = 0;
            byte cont = 1;
            
            while (cont != 0)
            {
                cont = stream.Read(1);
                var part = stream.Read(4);

                num = num << 4 | part;
            }

            return new Literal(ver, typeId, num);
        }

        internal override int SumVersions() => ver;
    }
}