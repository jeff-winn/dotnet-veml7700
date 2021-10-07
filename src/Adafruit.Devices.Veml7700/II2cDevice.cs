using System;

namespace System.Device.I2c
{
    public interface II2cDevice : IDisposable {
        byte ReadByte();
        void Read(Span<byte> buffer);

        void WriteByte(byte value);
        void Write(ReadOnlySpan<byte> buffer);
        
        void WriteRead(ReadOnlySpan<byte> inBuffer, Span<byte> outBuffer);
    }
}