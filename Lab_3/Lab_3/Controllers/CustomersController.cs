using Lab_1.Interfaces;
using Lab_2.Filters;
using Lab_2.Models.DTOs;
using Lab_3.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Lab_1.Controllers
{
	public class CustomersController : ControllerBase
	{
		private readonly IMemoryCache _cache;
		private readonly ICustomersService _customersService;

		public CustomersController(IMemoryCache cache
			, ICustomersService customersService)
		{
			_cache = cache;
			_customersService = customersService;
		}

		/// <summary>
		/// Returns all the existing customers
		/// </summary>
		/// <returns>All the customers from the storage</returns>
		[HttpGet("customers/")]
		public async Task<ActionResult<List<GetCustomerDTO>>> GetAllCustomersAsync()
		{
			if (_cache.TryGetValue(AppConfig.Config.CustomersCacheKey, out List<GetCustomerDTO> customers))
			{
				return Ok(customers);
			}

			customers = await _customersService.GetAllCustomers();

			if (AppConfig.Config.CacheEnabled)
			{
				_cache.Set(AppConfig.Config.CustomersCacheKey, customers, TimeSpan.FromMinutes(5));
			}

			return Ok(customers);
		}

		/// <summary>
		/// Returns the customer with certain ID
		/// </summary>
		/// <param name="id">ID of the customer to return</param>
		/// <returns>Customer with the specified ID</returns>
		[HttpGet("customers/{id}")]
		public async Task<ActionResult<GetCustomerDTO>> GetCustomerByIdAsync(string id)
		{
			var customer = await _customersService.GetCustomerById(id);

			if (customer == null)
			{
				return BadRequest($"Invalid input: no customer has {id} id");
			}

			return Ok(customer);
		}

		/// <summary>
		/// Adds new customer to the storage
		/// </summary>
		/// <param name="customer">Aucustomerthor to add</param>
		/// <returns>Nothing</returns>
		[HttpPost("customers/")]
		[ValidationFilter]
		public async Task<ActionResult> AddCustomerAsync([FromBody] AddCustomerDTO customer)
		{
			if (await _customersService.AddCustomer(customer))
			{
				_cache.Remove(AppConfig.Config.CustomersCacheKey);
				return Ok("Added successfully");
			}

			return BadRequest("Something went wrong");
		}

		/// <summary>
		/// Deletes existing customer by specified ID
		/// </summary>
		/// <param name="id">ID of the customer to delete</param>
		/// <returns>Nothing</returns>
		[HttpDelete("customers/{id}")]
		public async Task<ActionResult> DeleteCustomerAsync(string id)
		{
			if (await _customersService.DeleteCustomerById(id))
			{
				_cache.Remove(AppConfig.Config.CustomersCacheKey);
				return Ok("Deleted successfully");
			}

			return BadRequest($"Invalid input: no customer was found");
		}

		/// <summary>
		/// Updates customer's info
		/// </summary>
		/// <param name="customer">New customer's fileds</param>
		/// <returns>Nothing</returns>
		[HttpPut("customers/{id}")]
		[ValidationFilter]
		public async Task<ActionResult> UpdateCustomerAsync([FromBody] UpdateCustomerDTO customer, string id)
		{
			if (await _customersService.UpdateCustomer(id, customer))
			{
				_cache.Remove(AppConfig.Config.CustomersCacheKey);
				return Ok("Updated successfuly");
			}
			return BadRequest($"Something went wrong");
		}
	}
}
