using WebApp.Infrastructure.Devices;

namespace WebApp.Services {
    public class InspectionService : IInspectionService
    {
        private readonly IAdafruit_VEML7700 driver;
        
        public InspectionService(IAdafruit_VEML7700 driver) {
            this.driver = driver;
        }

        public bool Inspect()
        {
            return true;
        }
    }
}