using System.Device.I2c;
using Adafruit.Devices.Primitives;
using Microsoft.Extensions.Configuration;
using WebApp.Infrastructure.Factories;
using WebApp.Services;

namespace Microsoft.Extensions.DependencyInjection
{
	static class ServiceCollectionBootstrapper
	{
		public static IServiceCollection AddWebAppServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient<IInspectionService, InspectionService>();

			services.AddSingleton<II2cBus>(sp =>
			{
				var options = configuration.GetVeml7700Options();

				return new I2cBusWrapper(
					I2cBus.Create(options.BusId));
			});

			if (configuration.GetValue<bool>("UseFakeI2cBus"))
			{
				services.AddSingleton<IVeml7700DriverFactory, FakeVeml7700DriverFactory>();
			}
			else
			{
				services.AddSingleton<IVeml7700DriverFactory, Veml7700DriverFactory>();
			}

			return services;
		}
	}
}