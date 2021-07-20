using WebApp.Infrastructure.Devices;

namespace WebApp.Services {
    public class SensorService : ISensorService
    {
        private readonly IAdafruit_VEML7700 driver;
        
        public SensorService(IAdafruit_VEML7700 driver) {
            this.driver = driver;
        }

        public bool Inspect()
        {
            return true;
        }
    }
}