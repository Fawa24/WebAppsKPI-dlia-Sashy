using System.ComponentModel.DataAnnotations;

namespace Lab_2.Models.DTOs
{
	public class AddCustomerDTO
	{
		[MaxLength(30, ErrorMessage = "Customer name cannot be longer than 30 characters")]
		public string Name { get; set; } = string.Empty;
	}
}
