using System;
using System.Collections;
using WebApp.Infrastructure.Primitives;

namespace WebApp.Infrastructure.Devices
{
    public class Adafruit_I2CRegisterBits : IAdafruit_I2CRegisterBits
    {
        private readonly IAdafruit_I2CRegister register;
        private readonly byte startIndex;
        private readonly byte length;

        public Adafruit_I2CRegisterBits(IAdafruit_I2CRegister register, byte startIndex, byte length)
        {
            this.register = register;
            this.startIndex = startIndex;
            this.length = length;
        }

        public bool ReadBool()
        {
            return Read() != 0;
        }

        private ushort Read()
        {
            var value = register.ReadUInt16();
            value >>= startIndex;

            return (ushort)(value & ((1 << length) - 1));
        }

        public void Write(bool value)
        {
            Write(value ? (ushort)1 : (ushort)0);
        }

        private void Write(ushort data)
        {
            int d = data;            
            int val = register.ReadUInt16();

            var mask = (1 << length) - 1;
            d &= mask;

            mask <<= startIndex;
            
            val &= ~mask;
            val |= data << startIndex;

            register.Write((ushort)val);
        }
    }
}