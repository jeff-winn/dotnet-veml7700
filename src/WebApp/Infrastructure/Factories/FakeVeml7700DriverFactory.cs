using Adafruit.Devices.Veml7700;

namespace WebApp.Infrastructure.Factories {
    class FakeVeml7700DriverFactory : IVeml7700DriverFactory {
        public IAdafruit_VEML7700 Create(int deviceAddress) {
            return new FakeAdafruit_VEML7700(2, 2, 5, 5);
        }
    }
}