using System.ComponentModel.DataAnnotations;

namespace Lab_2.Models.DTOs
{
	public class UpdateOrderDTO
	{
		[MaxLength(30, ErrorMessage = "Dish name cannot be longer than 30 characters")]
		public string? Product { get; set; }

		[Length(36, 36, ErrorMessage = "Invalid customer's Id format")]
		public string CustomerId { get; set; } = string.Empty;
	}
}