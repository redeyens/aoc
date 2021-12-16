using System;
using System.Collections.Generic;

namespace day16
{
    internal abstract class Packet
    {
        internal static IEnumerable<Packet> FromBitStream(BitStream stream)
        {
            while (stream.HasMore)
            {
                yield return ReadOnePacket(stream);
            }
        }

        private static Packet ReadOnePacket(BitStream stream)
        {
            int ver = stream.Read(3);
            int typeId = stream.Read(3);

            if (typeId == 4)
            {
                return Literal.FromBitStream(ver, typeId, stream);
            }
            else
            {
                return Operator.FromBitStream(ver, typeId, stream);
            }
        }

        internal abstract int SumVersions();

        internal abstract long Value { get; }
    }
}