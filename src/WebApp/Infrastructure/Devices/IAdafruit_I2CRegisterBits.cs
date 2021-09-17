using System;

namespace WebApp.Infrastructure.Devices
{
    public interface IAdafruit_I2CRegisterBits
    {
        byte Read();
        bool ReadBool();

        void Write(bool value);
        void Write(byte value);
    }
}