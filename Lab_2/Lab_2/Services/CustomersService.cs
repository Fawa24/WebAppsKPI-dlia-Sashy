using Lab_1.Interfaces;
using Lab_1.Models;
using Lab_2.Interfaces;
using Lab_2.Models.DTOs;

namespace Lab_1.Services
{
	public class CustomersService : ICustomersService
	{
		private readonly IOrderRepository _orderRepository;

		public CustomersService(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public async Task<bool> AddCustomer(AddCustomerDTO addCustomerDto)
		{
			var customer = new Customer
			{
				Id = Guid.NewGuid().ToString(),
				Name = addCustomerDto.Name
			};
			try
			{
				await _orderRepository.AddCustomer(customer);
			}
			catch
			{
				return false;
			}

			return true;
		}

		public async Task<bool> DeleteCustomerById(string id)
		{
			try
			{
				return await _orderRepository.DeleteCustomerById(id);
			}
			catch
			{
				return false;
			}
		}

		public async Task<List<GetCustomerDTO>> GetAllCustomers()
		{
			return (await _orderRepository.GetAllCustomers()).Select(x => x.ToGetCustomerDTO()).ToList();
		}

		public async Task<GetCustomerDTO?> GetCustomerById(string id)
		{
			return (await _orderRepository.GetCustomerById(id))?.ToGetCustomerDTO();
		}

		public async Task<bool> UpdateCustomer(string id, UpdateCustomerDTO updateCustomerDto)
		{
			var customerToUpdate = ConvertToCustomer(id, updateCustomerDto);
			try
			{
				return await _orderRepository.UpdateCustomer(customerToUpdate);
			}
			catch
			{
				return false;
			}
		}

		private Customer ConvertToCustomer(string id, UpdateCustomerDTO updateCustomerDto)
		{
			return new Customer
			{
				Id = id,
				Name = updateCustomerDto.Name
			};
		}
	}
}
