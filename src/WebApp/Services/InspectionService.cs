using Microsoft.Extensions.Logging;
using System;
using WebApp.Models;
using Adafruit.Devices.Veml7700;

namespace WebApp.Services {
    public class InspectionService : IInspectionService
    {
        private readonly IAdafruit_VEML7700 driver;
        private readonly ILogger<InspectionService> logger;
        
        public InspectionService(IAdafruit_VEML7700 driver, ILogger<InspectionService> logger) 
        { 
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public LuxResponse Inspect() {
            var lux = driver.ReadLuxNormalized();
            logger.LogTrace($"Lux: {lux}");

            var white = driver.ReadWhiteNormalized();
            logger.LogTrace($"White: {white}");

            return new LuxResponse {
                WhiteLight = white,
                Lux = lux
            };
        }
    }
}