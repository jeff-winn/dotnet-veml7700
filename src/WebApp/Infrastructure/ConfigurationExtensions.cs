using WebApp.Infrastructure.Configuration;
using WebApp.Infrastructure.Devices;

namespace Microsoft.Extensions.Configuration {
	public static class ConfigurationExtensions {
		public static Adafruit_VEML7700_Options GetVeml7700Options(this IConfiguration configuration) {
 			return configuration.GetSection(nameof(Adafruit_VEML7700)).Get<Adafruit_VEML7700_Options>();
		}

		public static bool SwaggerEnabled(this IConfiguration configuration) {
			return configuration.GetValue<bool>("SwaggerEnabled");
		}
	}
}