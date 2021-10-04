using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.IO;
using Adafruit.Devices.Veml7700;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApp.Exceptions;

namespace WebApp.Infrastructure.Factories {
    class Veml7700DriverFactory : IVeml7700DriverFactory
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly II2cBus bus;
        private readonly IConfiguration configuration;
        private readonly Dictionary<int, IAdafruit_VEML7700> devices = new Dictionary<int, IAdafruit_VEML7700>();

        public Veml7700DriverFactory(ILoggerFactory loggerFactory, II2cBus bus, IConfiguration configuration) {
            this.loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            this.bus = bus ?? throw new ArgumentNullException(nameof(bus));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        
        public IAdafruit_VEML7700 Create(int deviceAddress)
        {
            var driver = GetDeviceById(deviceAddress);				
            var options = configuration.GetVeml7700Options();

			driver.IsEnabled = true;
			driver.Gain = options.Gain;
			driver.IntegrationTime = options.IntegrationTime;

			return driver;
        }

        private IAdafruit_VEML7700 GetDeviceById(int deviceAddress) {
            if (devices.TryGetValue(deviceAddress, out var device)) {
                return device;
            }

            try {
                device = new Adafruit_VEML7700(
                    bus.CreateDevice(deviceAddress),
                    loggerFactory.CreateLogger<Adafruit_VEML7700>());
                            
                device.Init();

                devices.Add(deviceAddress, device);
                return device;
            }
            catch (IOException ex) {
                throw new DeviceNotFoundException(deviceAddress, ex);
            }
        }
    }
}