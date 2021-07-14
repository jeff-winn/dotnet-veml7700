namespace Microsoft.Extensions.Configuration {
	public static class ConfigurationExtensions {
		public static bool SwaggerEnabled(this IConfiguration configuration) {
			return configuration.GetValue<bool>("SwaggerEnabled");
		}
	}
}