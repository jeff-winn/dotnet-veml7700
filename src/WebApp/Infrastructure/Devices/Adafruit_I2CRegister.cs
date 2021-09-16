using System;
using System.Collections;
using WebApp.Infrastructure.Primitives;

namespace WebApp.Infrastructure.Devices
{
    public class Adafruit_I2CRegister : IAdafruit_I2CRegister
    {
        private readonly II2cDevice device;
        private readonly byte[] registerAddress;
        private readonly Endianness endianness;

        public Adafruit_I2CRegister(II2cDevice device, byte registerAddress, Endianness endianness = Endianness.Little)
        {
            this.device = device;
            this.registerAddress = new[] {
                registerAddress
            };
            
            this.endianness = endianness;
        }

        public ushort ReadUInt16() 
        {
            var read = Read();                        
            return BitConverter.ToUInt16(read);
        }

        private ReadOnlySpan<byte> Read()
        {
            var buffer = new byte[2];
            device.WriteRead(registerAddress, buffer);

            // Ensure the endianness the data will come from the hardware matches what the CPU running the software will require for conversion.
            if ((endianness == Endianness.Little && !BitConverter.IsLittleEndian) || (endianness == Endianness.Big && BitConverter.IsLittleEndian))
                Array.Reverse<byte>(buffer);

            return buffer;
        }

        public void Write(ushort value) 
        {
            var buffer = BitConverter.GetBytes(value);

            if ((endianness == Endianness.Little && !BitConverter.IsLittleEndian) || (endianness == Endianness.Big && BitConverter.IsLittleEndian))
                Array.Reverse<byte>(buffer);

            Write(buffer);
        }

        private void Write(byte[] data)
        {
            var buffer = new byte[registerAddress.Length + data.Length];

            Array.Copy(registerAddress, buffer, registerAddress.Length);
            Array.Copy(data, 0, buffer, registerAddress.Length, data.Length);

            device.Write(buffer);
        }
    }
}