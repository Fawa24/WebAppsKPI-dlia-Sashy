using Lab_1.Interfaces;
using Lab_1.Models;
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
		public ActionResult<List<Customer>> GetAllCustomers()
		{
			return Ok(_customersService.GetAllCustomers());
		}

		/// <summary>
		/// Returns the customers with certain ID
		/// </summary>
		/// <param name="id">ID of the customers to return</param>
		/// <returns>Customers with the specified ID</returns>
		[HttpGet("customers/{id}")]
		public ActionResult<Customer> GetCustomerById(string id)
		{
			var customer = _customersService.GetCustomerById(id);

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
		public ActionResult AddCustomer([FromBody] Customer customer)
		{
			if (_customersService.AddCustomer(customer))
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
		public ActionResult DeleteCustomer(string id)
		{
			if (_customersService.DeleteCustomerById(id))
			{
				return Ok("Deleted successfully");
			}

			return BadRequest($"Invalid input: no customer was found");
		}

		/// <summary>
		/// Updates customer's info
		/// </summary>
		/// <param name="customer">New customer's fileds</param>
		/// <param name="id">ID of the customer to update</param>
		/// <returns>Nothing</returns>
		[HttpPut("customers/{id}")]
		public ActionResult UpdateCustomer([FromBody] Customer customer, string id)
		{
			if (_customersService.UpdateCustomer(customer, id))
			{
				return Ok("Updated successfuly");
			}
			return BadRequest($"Invalid input: no customer was found");
		}
	}
}
