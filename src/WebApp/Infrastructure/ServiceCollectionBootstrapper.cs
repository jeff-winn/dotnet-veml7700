using System.Device.I2c;
using Microsoft.Extensions.Configuration;
using WebApp.Infrastructure.Configuration;
using WebApp.Infrastructure.Devices;
using WebApp.Infrastructure.Primitives;

namespace Microsoft.Extensions.DependencyInjection {
	public static class ServiceCollectionBootstrapper {
		public static IServiceCollection AddWebAppServices(this IServiceCollection services) {		
			services.AddSingleton<II2cBus>(sp => new I2cBusWrapper(
				I2cBus.Create(1)));

			services.AddSingleton<IAdafruit_VEML7700>(sp => {
				var bus = sp.GetRequiredService<II2cBus>();
				var configuration = sp.GetRequiredService<IConfiguration>();

				var options = configuration.GetSection(nameof(Adafruit_VEML7700)).Get<Adafruit_VEML7700_Options>();
				
				var driver = new Adafruit_VEML7700(
					bus.CreateDevice(options.DeviceAddress));

				driver.IsEnabled = true;
				driver.Gain = options.Gain;
				driver.IntegrationTime = options.IntegrationTime;

				driver.Init();

				return driver;
			});

			return services;
		}
	}
}