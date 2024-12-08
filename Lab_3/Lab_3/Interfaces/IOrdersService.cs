using Lab_2.Models.DTOs;

namespace Lab_1.Interfaces
{
	public interface IOrdersService
	{
		public Task<List<GetOrderDTO>> GetAllOrders();
		public Task<GetOrderDTO?> GetOrderById(string id);
		public Task<bool> DeleteOrderById(string id);
		public Task<bool> AddOrder(AddOrderDTO order);
		public Task<bool> UpdateOrder(string id, UpdateOrderDTO order);
	}
}