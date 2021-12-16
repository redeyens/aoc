using System;
using System.Collections.Generic;

namespace day16
{
    internal class BitStream
    {
        private byte[] data;

        private int pos = 0;
        private int maxPos;

        public BitStream(byte[] data)
        {
            this.data = data;
            this.maxPos = data.Length * 8;
        }

        private BitStream(byte[] data, int pos, int len) : this(data)
        {
            this.pos = pos;
            this.maxPos = pos + len;
        }

        internal byte Read(int numberOfBits)
        {
            if(numberOfBits > 8)
            {
                throw new ArgumentException("Can read at most 8 bits at a time.");
            }

            int startOffset = pos % 8;

            if(startOffset + numberOfBits <= 8)
            {
                int index = pos / 8;
                byte mask = (byte)((1 << numberOfBits) - 1);

                pos += numberOfBits;

                return (byte)(data[index] >> (8 - startOffset - numberOfBits) & mask);
            }
            
            int firstPart = 8 - startOffset;
            int secondPart = numberOfBits - firstPart;
            return (byte)(Read(firstPart) << secondPart | Read(secondPart));
        }

        internal BitStream ReadSubstream(int len)
        {
            int currentPos = pos;
            pos += len;
            return new BitStream(data, currentPos, len);
        }

        public bool HasMore => maxPos - pos >= 4;
    }
}