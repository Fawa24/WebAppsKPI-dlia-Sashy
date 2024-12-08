using Lab_1.Models;

namespace Lab_1.Interfaces
{
	public interface IOrdersService
	{
		public List<Order> GetAllOrders();
		public Order? GetOrderById(string id);
		public bool DeleteOrderById(string id);
		public bool AddOrder(Order order);
		public bool UpdateOrder(Order order, string orderId);
	}
}