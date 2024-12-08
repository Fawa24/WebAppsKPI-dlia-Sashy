namespace Lab_3.Models
{
	public class AppConfigDTO
	{
		public AppConfigDTO() { }

		public bool CacheEnabled { get; set; } = false;
		public string CustomersCacheKey { get; set; } = string.Empty;
		public string OrdersCacheKey { get; set; } = string.Empty;
	}
}
