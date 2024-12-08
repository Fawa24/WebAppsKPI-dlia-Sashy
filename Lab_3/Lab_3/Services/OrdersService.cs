using Lab_1.Interfaces;
using Lab_1.Models;
using Lab_2.Interfaces;
using Lab_2.Models.DTOs;

namespace Lab_1.Services
{
	public class OrdersService : IOrdersService
	{
		private readonly IOrderRepository _ordersRepository;

		public OrdersService(IOrderRepository ordersRepository)
		{
			_ordersRepository = ordersRepository;
		}

		public async Task<bool> AddOrder(AddOrderDTO addOrderDto)
		{
			var customer = await _ordersRepository.GetCustomerById(addOrderDto.CustomerId);

			if (customer == null)
			{
				return false;
			}

			var order = new Order
			{
				Id = Guid.NewGuid().ToString(),
				Product = addOrderDto.Product,
				Customer = customer,
			};

			try
			{
				await _ordersRepository.AddOrder(order);
			}
			catch
			{
				return false;
			}

			return true;
		}

		public async Task<bool> DeleteOrderById(string id)
		{
			try
			{
				return await _ordersRepository.DeleteOrderById(id);
			}
			catch
			{
				return false;
			}
		}

		public async Task<List<GetOrderDTO>> GetAllOrders()
		{
			return (await _ordersRepository.GetAllOrders()).Select(x => x.ToGetOrderDTO()).ToList();
		}

		public async Task<GetOrderDTO?> GetOrderById(string id)
		{
			return (await _ordersRepository.GetOrderById(id))?.ToGetOrderDTO();
		}

		public async Task<bool> UpdateOrder(string id, UpdateOrderDTO updateOrderDto)
		{
			try
			{
				var orderToUpdate = await ConvertToOrder(id, updateOrderDto);
				return await _ordersRepository.UpdateOrder(orderToUpdate);
			}
			catch
			{
				return false;
			}
		}

		private async Task<Order> ConvertToOrder(string id, UpdateOrderDTO updateOrderDto)
		{
			var customer = await _ordersRepository.GetCustomerById(updateOrderDto.CustomerId);

			return new Order
			{
				Id = id,
				Product = updateOrderDto.Name,
				Customer = customer
			};
		}
	}
}
