using System;
using System.Device.I2c;

namespace WebApp.Infrastructure.Primitives {
    public class I2cDeviceWrapper : II2cDevice {
        private readonly I2cDevice wrappedDevice;

        public I2cDeviceWrapper(I2cDevice wrappedDevice) {
            this.wrappedDevice = wrappedDevice;
        }

        public byte ReadByte() {
            return wrappedDevice.ReadByte();
        }

        public void Read(Span<byte> buffer){
            wrappedDevice.Read(buffer);
        }

        public void WriteByte(byte value){
            wrappedDevice.WriteByte(value);
        }

        public void Write(ReadOnlySpan<byte> buffer){
            wrappedDevice.Write(buffer);
        }

        public void WriteRead(ReadOnlySpan<byte> inBuffer, Span<byte> outBuffer) {
            wrappedDevice.WriteRead(inBuffer, outBuffer);
        }
    }
}