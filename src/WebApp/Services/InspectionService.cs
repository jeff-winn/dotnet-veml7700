using Microsoft.Extensions.Logging;
using System;
using WebApp.Models;
using WebApp.Exceptions;
using WebApp.Infrastructure.Factories;

namespace WebApp.Services {
    public class InspectionService : IInspectionService
    {
        private readonly IVeml7700DriverFactory factory;
        private readonly ILogger<InspectionService> logger;
        
        public InspectionService(IVeml7700DriverFactory factory, ILogger<InspectionService> logger) 
        { 
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public LuxResponse Inspect(int deviceId) {            
            var driver = factory.Create(deviceId);
            if (driver == null) {
                throw new DeviceNotFoundException(deviceId);
            }

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