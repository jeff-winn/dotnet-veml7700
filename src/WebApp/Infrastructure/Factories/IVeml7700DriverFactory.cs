using Adafruit.Devices.Veml7700;

namespace WebApp.Infrastructure.Factories {
    interface IVeml7700DriverFactory {
        IAdafruit_VEML7700 Create(int deviceAddress);
    }
}