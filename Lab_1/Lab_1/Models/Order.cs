namespace Lab_1.Models
{
	public class Order
	{
		public string Id { get; set; }
		public string? Dish { get; set; }
		public Customer? Customer { get; set; }
	}
}
