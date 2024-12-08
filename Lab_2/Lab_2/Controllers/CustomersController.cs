using Lab_1.Interfaces;
using Lab_2.Filters;
using Lab_2.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Lab_1.Controllers
{
	public class CustomersController : ControllerBase
	{
		private readonly ICustomersService _customersService;

		public CustomersController(ICustomersService customersService)
		{
			_customersService = customersService;
		}

		/// <summary>
		/// Returns all the existing customers
		/// </summary>
		/// <returns>All the customers from the storage</returns>
		[HttpGet("customers/")]
		public async Task<ActionResult<List<GetCustomerDTO>>> GetAllCustomersAsync()
		{
			return Ok(await _customersService.GetAllCustomers());
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
		/// <param name="customer">Customer to add</param>
		/// <returns>Nothing</returns>
		[HttpPost("customers/")]
		[ValidationFilter]
		public async Task<ActionResult> AddCustomerAsync([FromBody] AddCustomerDTO customer)
		{
			if (await _customersService.AddCustomer(customer))
			{
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
				return Ok("Updated successfuly");
			}
			return BadRequest($"Something went wrong");
		}
	}
}
