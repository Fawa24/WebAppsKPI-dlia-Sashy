namespace Lab_1.Models
{
	public class Order
	{
		public string Id { get; set; } = string.Empty;
		public string? Product { get; set; }
		public Customer? Customer { get; set; }
	}
}
