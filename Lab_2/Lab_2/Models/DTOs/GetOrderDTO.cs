using Lab_1.Models;

namespace Lab_2.Models.DTOs
{
	public class GetOrderDTO
	{
		public string Id { get; set; } = string.Empty;
		public string? Product { get; set; }
		public Customer? Customer { get; set; }
	}

	public static class OrderExtension
	{
		public static GetOrderDTO ToGetOrderDTO(this Order order)
		{
			return new GetOrderDTO
			{
				Id = order.Id,
				Customer = order.Customer,
				Product = order.Product
			};
		}
	}
}
