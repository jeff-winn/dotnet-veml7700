using System;

namespace Adafruit.Devices.Primitives
{
    public class I2cRegisterBits : II2cRegisterBits
    {
        private readonly II2cRegister register;
        private readonly byte startIndex;
        private readonly byte length;

        public I2cRegisterBits(II2cRegister register, byte startIndex, byte length)
        {
            this.register = register;
            this.startIndex = startIndex;
            this.length = length;
        }

        public bool ReadBool()
        {
            return Read() != 0;
        }

        public byte Read()
        {
            var value = register.ReadUInt16();
            value >>= startIndex;

            return (byte)(value & ((1 << length) - 1));
        }

        public void Write(bool value)
        {
            Write(value ? (byte)1 : (byte)0);
        }

        public void Write(byte value)
        {
            int d = value;            
            int val = register.ReadUInt16();

            var mask = (1 << length) - 1;
            d &= mask;

            mask <<= startIndex;
            
            val &= ~mask;
            val |= value << startIndex;

            register.Write((ushort)val);
        }
    }
}