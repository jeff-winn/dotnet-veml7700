using System.Device.I2c;

namespace WebApp.Infrastructure.Primitives {
    public class I2cBusWrapper : II2cBus {
        private readonly I2cBus bus;

        public I2cBusWrapper(I2cBus bus) {
            this.bus = bus;
        }

        public II2cDevice CreateDevice(int deviceAddress) {
            var device = bus.CreateDevice(deviceAddress);

            return new I2cDeviceWrapper(device);
        }
    }
}