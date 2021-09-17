using System;

namespace Adafruit.Devices.Primitives
{
    public interface II2cRegister
    {
        ushort ReadUInt16();
        void Write(ushort value);
        II2cRegisterBits GetRegisterBits(byte startIndex, byte length);
    }
}