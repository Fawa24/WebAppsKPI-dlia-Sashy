﻿using Lab_1.Models;
using Lab_2.Databases;
using Lab_2.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
			Log.Information("GetAllOrders was called");
			return await _db.Orders.Include(x => x.Customer).ToListAsync();
		}

		public async Task<Order?> GetOrderById(string id)
		{
			Log.Information("GetOrderById was called");
			return await _db.Orders.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<bool> DeleteOrderById(string id)
		{
			Log.Information("DeleteOrderById was called");
			var orderToRemove = await _db.Orders.FindAsync(id);
			if (orderToRemove != null)
			{
				_db.Orders.Remove(orderToRemove);
				await _db.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<List<Customer>> GetAllCustomer()
		{
			Log.Information("GetAllCustomer was called");
			return await _db.Customers.ToListAsync();
		}

		public async Task<Customer?> GetCustomerById(string id)
		{
			Log.Information("GetCustomerById was called");
			return await _db.Customers.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<bool> DeleteCustomerById(string id)
		{
			Log.Information("DeleteCustomerById was called");
			var autorToRemove = await _db.Customers.FindAsync(id);
			if (autorToRemove != null)
			{
				_db.Customers.Remove(autorToRemove);
				await _db.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task AddOrder(Order order)
		{
			Log.Information("AddOrder was called");
			await _db.Orders.AddAsync(order);
			await _db.SaveChangesAsync();
		}

		public async Task<bool> UpdateOrder(Order order)
		{
			Log.Information("UpdateOrder was called");
			_db.Orders.Update(order);
			await _db.SaveChangesAsync();
			return true;
		}

		public async Task AddCustomer(Customer customer)
		{
			Log.Information("AddCustomer was called");
			await _db.Customers.AddAsync(customer);
			await _db.SaveChangesAsync();
		}

		public async Task<bool> UpdateCustomer(Customer customer)
		{
			Log.Information("UpdateCustomer was called");
			_db.Customers.Update(customer);
			await _db.SaveChangesAsync();
			return true;
		}
	}
}