using System;
using System.Device.I2c;

namespace Adafruit.Devices.Primitives
{
    public class I2cDeviceWrapper : II2cDevice
    {
        private readonly I2cDevice device;

        public I2cDeviceWrapper(I2cDevice device)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device));
        }

        ~I2cDeviceWrapper() {
            Dispose(false);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                device.Dispose();
            }
        }
        public byte ReadByte()
        {
            return device.ReadByte();
        }

        public void Read(Span<byte> buffer)
        {
            device.Read(buffer);
        }

        public void WriteByte(byte value)
        {
            device.WriteByte(value);
        }

        public void Write(ReadOnlySpan<byte> buffer)
        {
            device.Write(buffer);
        }

        public void WriteRead(ReadOnlySpan<byte> inBuffer, Span<byte> outBuffer)
        {
            device.WriteRead(inBuffer, outBuffer);
        }
    }
}