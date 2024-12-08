using System.ComponentModel.DataAnnotations;

namespace Lab_2.Models.DTOs
{
	public class UpdateOrderDTO
	{
		[MaxLength(30, ErrorMessage = "Order name cannot be longer than 30 characters")]
		public string? Name { get; set; }

		[Length(36, 36, ErrorMessage = "Invalid customer's Id format")]
		public string CustomerId { get; set; } = string.Empty;
	}
}