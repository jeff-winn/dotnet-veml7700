using WebApp.Infrastructure.Devices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using WebApp.Infrastructure.Configuration;
using WebApp.Models;

namespace WebApp.Services {
    public class InspectionService : IInspectionService
    {
        private readonly IAdafruit_VEML7700 driver;
        private readonly ILogger<InspectionService> logger;
        private readonly ThresholdOptions thresholdOptions;
        
        public InspectionService(IAdafruit_VEML7700 driver, ILogger<InspectionService> logger, IOptions<ThresholdOptions> thresholdOptions) 
            : this(driver, logger, thresholdOptions.Value)
        { }

        protected InspectionService(IAdafruit_VEML7700 driver, ILogger<InspectionService> logger, ThresholdOptions thresholdOptions) {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.thresholdOptions = thresholdOptions ?? throw new ArgumentNullException(nameof(thresholdOptions));
        }

        public LuxResponse Inspect() {
            var lux = driver.ReadLux();
            logger.LogTrace($"Lux: {lux}");

            return new LuxResponse {
                IsOn = lux >= thresholdOptions.Min,
                Lux = lux
            };
        }
    }
}