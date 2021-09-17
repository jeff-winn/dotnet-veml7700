using System;

namespace WebApp.Infrastructure.Devices
{
    public interface IAdafruit_I2CRegisterBits
    {
        bool ReadBool();

        void Write(bool value);
    }
}