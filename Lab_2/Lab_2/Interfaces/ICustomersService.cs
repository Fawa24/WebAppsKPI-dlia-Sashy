using Lab_2.Models.DTOs;

namespace Lab_1.Interfaces
{
	public interface ICustomersService
	{
		public Task<List<GetCustomerDTO>> GetAllCustomers();
		public Task<GetCustomerDTO?> GetCustomerById(string id);
		public Task<bool> DeleteCustomerById(string id);
		public Task<bool> AddCustomer(AddCustomerDTO customer);
		public Task<bool> UpdateCustomer(string id, UpdateCustomerDTO customer);
	}
}
