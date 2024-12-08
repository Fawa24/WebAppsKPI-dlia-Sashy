using Lab_1.Interfaces;
using Lab_1.Models;

namespace Lab_1.Services
{
	public class CustomersService : ICustomersService
	{
		private List<Customer> _customers;

		public CustomersService()
		{
			_customers =
			[
				new Customer
				{
					Id = "3FD44B89-F9D3-49BC-ABDF-33DAF9B90870",
					Name = "Oleskandra Leshkovych"
				},
				new Customer
				{
					Id = "DCA81070-3A59-4033-8F9A-D8AF18A19C9D",
					Name = "Ksenia Chepizhna"
				}
			];
		}

		public bool AddCustomer(Customer customer)
		{
			customer.Id = Guid.NewGuid().ToString();
			_customers.Add(customer);
			return true;
		}

		public bool DeleteCustomerById(string id)
		{
			var customer = GetCustomerById(id);

			if (customer != null)
			{
				return _customers.Remove(customer);
			}
			return false;
		}

		public List<Customer> GetAllCustomers()
		{
			return _customers;
		}

		public Customer? GetCustomerById(string id)
		{
			return _customers.FirstOrDefault(a => a.Id == id);
		}

		public bool UpdateCustomer(Customer customer, string customerId)
		{
			var customerToUpdate = _customers.FirstOrDefault(a => a.Id == customerId);

			if (customerToUpdate != null)
			{
				customerToUpdate.Name = customer.Name;
				return true;
			}

			return false;
		}
	}
}
