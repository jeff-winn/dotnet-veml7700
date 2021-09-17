using System;

namespace Adafruit.Devices.Primitives
{
    public interface II2cRegisterBits
    {
        byte Read();
        bool ReadBool();

        void Write(bool value);
        void Write(byte value);
    }
}