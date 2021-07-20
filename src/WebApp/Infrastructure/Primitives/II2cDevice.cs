using System;

namespace WebApp.Infrastructure.Primitives {
    public interface II2cDevice {
        byte ReadByte();
        void Read(Span<byte> buffer);

        void WriteByte(byte value);
        void Write(ReadOnlySpan<byte> buffer);
        
        void WriteRead(ReadOnlySpan<byte> inBuffer, Span<byte> outBuffer);
    }
}