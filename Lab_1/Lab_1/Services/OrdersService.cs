using Lab_1.Interfaces;
using Lab_1.Models;

namespace Lab_1.Services
{
	public class OrdersService : IOrdersService
	{
		private List<Order> _orders;

		public OrdersService()
		{
			_orders =
			[
				new Order
				{
					Id = "2287C0AD-F383-4486-81B8-B3DC37C21A59",
					Dish = "Apple pie",
					Customer = new Customer
					{
						Id = "3FD44B89-F9D3-49BC-ABDF-33DAF9B90870",
						Name = "Oleskandra Leshkovych"
					}
				},
				new Order
				{
					Id = "329B3705-41BD-4731-ABD4-51243CA4FF9C",
					Dish = "Chockolate smuzi",
					Customer = new Customer
					{
						Id = "DCA81070-3A59-4033-8F9A-D8AF18A19C9D",
						Name = "Ksenia Chepizhna"
					}
				},
			];
		}

		public bool AddOrder(Order order)
		{
			_orders.Add(order);
			return true;
		}

		public bool DeleteOrderById(string id)
		{
			var order = GetOrderById(id);

			if (order != null)
			{
				return _orders.Remove(order);
			}
			return false;
		}

		public List<Order> GetAllOrders()
		{
			return _orders;
		}

		public Order? GetOrderById(string id)
		{
			return _orders.FirstOrDefault(a => a.Id == id);
		}

		public bool UpdateOrder(Order order, string orderId)
		{
			var orderToUpdate = _orders.FirstOrDefault(b => b.Id == orderId);

			if (orderToUpdate != null)
			{
				orderToUpdate.Dish = order.Dish;
				orderToUpdate.Customer = order.Customer;
				return true;
			}

			return false;
		}
	}
}
