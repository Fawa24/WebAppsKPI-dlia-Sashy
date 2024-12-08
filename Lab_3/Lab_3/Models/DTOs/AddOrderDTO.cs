using System.ComponentModel.DataAnnotations;

namespace Lab_2.Models.DTOs
{
	public class AddOrderDTO
	{
		[MaxLength(30, ErrorMessage = "Order name cannot be longer than 30 characters")]
		public string? Product { get; set; }

		[Length(36, 36, ErrorMessage = "Invalid customer Id format")]
		public string CustomerId { get; set; } = string.Empty;
	}
}
