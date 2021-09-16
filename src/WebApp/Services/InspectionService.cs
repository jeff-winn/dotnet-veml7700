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
            var current = driver.IsEnabled;
            logger.LogInformation($"Current: {current}");

            var changed = !current;
            
            driver.IsEnabled = changed;
            logger.LogInformation($"Changed: {changed}");

            var now = driver.IsEnabled;
            logger.LogInformation($"Now: {now}");

            return now;
        }
    }
}