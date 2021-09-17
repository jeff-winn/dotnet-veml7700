using WebApp.Infrastructure.Devices;
using Microsoft.Extensions.Logging;

namespace WebApp.Services {
    public class InspectionService : IInspectionService
    {
        private readonly IAdafruit_VEML7700 driver;
        private readonly ILogger<InspectionService> logger;
        
        public InspectionService(IAdafruit_VEML7700 driver, ILogger<InspectionService> logger) {
            this.driver = driver;
            this.logger = logger;
        }

        public bool Inspect()
        {
            var lux = driver.ReadLux();
            System.Diagnostics.Debug.WriteLine(lux);
                        
            return false;
        }
    }
}