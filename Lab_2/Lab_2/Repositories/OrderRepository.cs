using Lab_1.Models;
using Lab_2.Databases;
using Lab_2.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab_2.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly OrdersDbContext _db;

		public OrderRepository(OrdersDbContext db)
		{
			_db = db;
		}

		public async Task<List<Order>> GetAllOrders()
		{
			return await _db.Orders.Include(x => x.Customer).ToListAsync();
		}

		public async Task<Order?> GetOrderById(string id)
		{
			return await _db.Orders.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<bool> DeleteOrderById(string id)
		{
			var orderToRemove = await _db.Orders.FindAsync(id);
			if (orderToRemove != null)
			{
				_db.Orders.Remove(orderToRemove);
				await _db.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<List<Customer>> GetAllCustomers()
		{
			return await _db.Customers.ToListAsync();
		}

		public async Task<Customer?> GetCustomerById(string id)
		{
			return await _db.Customers.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<bool> DeleteCustomerById(string id)
		{
			var customerToRemove = await _db.Customers.FindAsync(id);
			if (customerToRemove != null)
			{
				_db.Customers.Remove(customerToRemove);
				await _db.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task AddOrder(Order order)
		{
			await _db.Orders.AddAsync(order);
			await _db.SaveChangesAsync();
		}

		public async Task<bool> UpdateOrder(Order order)
		{
			_db.Orders.Update(order);
			await _db.SaveChangesAsync();
			return true;
		}

		public async Task AddCustomer(Customer customer)
		{
			await _db.Customers.AddAsync(customer);
			await _db.SaveChangesAsync();
		}

		public async Task<bool> UpdateCustomer(Customer customer)
		{
			_db.Customers.Update(customer);
			await _db.SaveChangesAsync();
			return true;
		}
	}
}
