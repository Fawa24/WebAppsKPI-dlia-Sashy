using Lab_1.Models;

namespace Lab_2.Models.DTOs
{
	public class GetCustomerDTO
	{
		public string Id { get; set; } = string.Empty;
		public string? Name { get; set; }
	}

	public static class CustomerExtension
	{
		public static GetCustomerDTO ToGetCustomerDTO(this Customer customer)
		{
			return new GetCustomerDTO
			{
				Id = customer.Id,
				Name = customer.Name
			};
		}
	}
}
