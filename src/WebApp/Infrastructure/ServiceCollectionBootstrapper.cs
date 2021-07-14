using System.Device.Gpio;
using WebApp.Infrastructure.Primitives;

namespace Microsoft.Extensions.DependencyInjection {
	public static class ServiceCollectionBootstrapper {
		public static IServiceCollection AddWebAppServices(this IServiceCollection services) {			
			services.AddSingleton<GpioController>();
			services.AddSingleton<IGpioController>(sp => new GpioControllerWrapper(sp.GetRequiredService<GpioController>()));

			return services;
		}
	}
}