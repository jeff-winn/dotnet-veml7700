using System;

namespace WebApp.Infrastructure.Devices
{
    public interface IAdafruit_I2CRegister
    {
        ushort ReadUInt16();
        void Write(ushort value);
        IAdafruit_I2CRegisterBits GetRegisterBits(byte startIndex, byte length);
    }
}