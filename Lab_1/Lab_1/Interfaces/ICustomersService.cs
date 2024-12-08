using Lab_1.Models;

namespace Lab_1.Interfaces
{
	public interface ICustomersService
	{
		public List<Customer> GetAllCustomers();
		public Customer? GetCustomerById(string id);
		public bool DeleteCustomerById(string id);
		public bool AddCustomer(Customer customer);
		public bool UpdateCustomer(Customer customer, string customerId);
	}
}
