using Adafruit.Devices.Veml7700;
using WebApp.Infrastructure.Configuration;

namespace Microsoft.Extensions.Configuration 
{
	public static class ConfigurationExtensions 
	{
		public static Veml7700Options GetVeml7700Options(this IConfiguration configuration) {
 			return configuration.GetSection(nameof(Adafruit_VEML7700)).Get<Veml7700Options>();
		}

		public static bool SwaggerEnabled(this IConfiguration configuration) {
			return configuration.GetValue<bool>("SwaggerEnabled");
		}
	}
}