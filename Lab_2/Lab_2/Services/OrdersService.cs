using Lab_1.Interfaces;
using Lab_1.Models;
using Lab_2.Interfaces;
using Lab_2.Models.DTOs;

namespace Lab_1.Services
{
	public class OrdersService : IOrdersService
	{
		private readonly IOrderRepository _orderRepository;

		public OrdersService(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public async Task<bool> AddOrder(AddOrderDTO addOrderDto)
		{
			var customer = await _orderRepository.GetCustomerById(addOrderDto.CustomerId);

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
				await _orderRepository.AddOrder(order);
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
				return await _orderRepository.DeleteOrderById(id);
			}
			catch
			{
				return false;
			}
		}

		public async Task<List<GetOrderDTO>> GetAllOrders()
		{
			return (await _orderRepository.GetAllOrders()).Select(x => x.ToGetOrderDTO()).ToList();
		}

		public async Task<GetOrderDTO?> GetOrderById(string id)
		{
			return (await _orderRepository.GetOrderById(id))?.ToGetOrderDTO();
		}

		public async Task<bool> UpdateOrder(string id, UpdateOrderDTO updateOrderDto)
		{
			try
			{
				var orderToUpdate = await ConvertToOrder(id, updateOrderDto);
				return await _orderRepository.UpdateOrder(orderToUpdate);
			}
			catch
			{
				return false;
			}
		}

		private async Task<Order> ConvertToOrder(string id, UpdateOrderDTO updateOrderDTO)
		{
			var customer = await _orderRepository.GetCustomerById(updateOrderDTO.CustomerId);

			return new Order
			{
				Id = id,
				Product = updateOrderDTO.Product,
				Customer = customer
			};
		}
	}
}
