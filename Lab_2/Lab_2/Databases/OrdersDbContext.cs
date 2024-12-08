using Lab_1.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab_2.Databases
{
	public class OrdersDbContext : DbContext
	{
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }

		public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
		: base(options)
		{

		}
	}
}
