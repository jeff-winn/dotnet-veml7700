using Adafruit.Devices.Veml7700;

namespace WebApp.Infrastructure.Factories {
    public interface IVeml7700DriverFactory {
        IAdafruit_VEML7700 Create(int deviceId);
    }
}