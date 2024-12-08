using Lab_3.Interfaces;
using Lab_3.Models;
using System.Text.Json;

namespace Lab_3.Services
{
	public class ConfigurationService : IConfigurationService
	{
		private static readonly string AppConfigFilePath = "AppConfig.json";

		public AppConfigDTO GetAppConfig()
		{
			var config = JsonSerializer.Deserialize<AppConfigDTO>(File.ReadAllText(AppConfigFilePath));

			if (config == null)
			{
				throw new Exception($"Cannot read values from {AppConfigFilePath}");
			}

			return config;
		}
	}
}
