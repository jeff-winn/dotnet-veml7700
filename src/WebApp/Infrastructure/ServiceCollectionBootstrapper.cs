using System.Device.I2c;
using Adafruit.Devices.Primitives;
using Adafruit.Devices.Veml7700;
using Microsoft.Extensions.Configuration;
using WebApp.Infrastructure.Configuration;
using WebApp.Services;

namespace Microsoft.Extensions.DependencyInjection {
	public static class ServiceCollectionBootstrapper {
		public static IServiceCollection AddWebAppServices(this IServiceCollection services, IConfiguration configuration) 
		{		
			services.AddTransient<IInspectionService, InspectionService>();

			services.AddSingleton<II2cBus>(sp => {
				var options = configuration.GetVeml7700Options();

				return new I2cBusWrapper(
					I2cBus.Create(options.BusId));
			});

			services.AddSingleton<IAdafruit_VEML7700>(sp => {
				IAdafruit_VEML7700 driver = null;
				
				#if DEBUG
					driver = new FakeAdafruit_VEML7700(300, 300.15F, 300, 300.15F);
				#else
					var bus = sp.GetRequiredService<II2cBus>();
					var options = configuration.GetVeml7700Options();
					
					var driver = new Adafruit_VEML7700(
						bus.CreateDevice(options.DeviceAddress));
						
					driver.Init();

					driver.IsEnabled = true;
					driver.Gain = options.Gain;
					driver.IntegrationTime = options.IntegrationTime;
				#endif

				return driver;
			});

			return services;
		}
	}
}