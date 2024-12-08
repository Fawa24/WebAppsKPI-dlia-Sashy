using Lab_1.Models;

namespace Lab_2.Interfaces
{
	public interface IOrderRepository
	{
		public Task<List<Order>> GetAllOrders();

		public Task<Order?> GetOrderById(string id);

		public Task AddOrder(Order order);

		public Task<bool> DeleteOrderById(string id);

		public Task<bool> UpdateOrder(Order order);

		public Task<List<Customer>> GetAllCustomers();

		public Task<Customer?> GetCustomerById(string id);

		public Task AddCustomer(Customer customer);

		public Task<bool> DeleteCustomerById(string id);

		public Task<bool> UpdateCustomer(Customer customer);
	}
}
