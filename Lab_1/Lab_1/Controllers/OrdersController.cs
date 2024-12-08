using Lab_1.Interfaces;
using Lab_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab_1.Controllers
{
	public class OrdersController : ControllerBase
	{
		private readonly IOrdersService _ordersService;

		public OrdersController(IOrdersService ordersService)
		{
			_ordersService = ordersService;
		}

		/// <summary>
		/// Returns all the existing orders
		/// </summary>
		/// <returns>All the orders from the storage</returns>
		[HttpGet("orders/")]
		public ActionResult<List<Order>> GetAllOrders()
		{
			return Ok(_ordersService.GetAllOrders());
		}

		/// <summary>
		/// Returns the order with certain ID
		/// </summary>
		/// <param name="id">ID of the order to return</param>
		/// <returns>Order with the specified ID</returns>
		[HttpGet("orders/{id}")]
		public ActionResult<Order> GetOrderById(string id)
		{
			var order = _ordersService.GetOrderById(id);

			if (order == null)
			{
				return BadRequest($"Invalid input: no order has {id} id");
			}

			return Ok(order);
		}

		/// <summary>
		/// Adds new order to the storage
		/// </summary>
		/// <param name="customer">Order to add</param>
		/// <returns>Nothing</returns>
		[HttpPost("orders/")]
		public ActionResult AddOrder([FromBody] Order order)
		{
			if (_ordersService.AddOrder(order))
			{
				return Ok("Added successfully");
			}

			return BadRequest("Something went wrong");
		}

		/// <summary>
		/// Deletes existing order by specified ID
		/// </summary>
		/// <param name="id">ID of the order to delete</param>s
		/// <returns>Nothing</returns>
		[HttpDelete("orders/{id}")]
		public ActionResult DeleteOrderById(string id)
		{
			if (_ordersService.DeleteOrderById(id))
			{
				return Ok("Deleted successfully");
			}

			return BadRequest($"Invalid input: no order has {id} id");
		}

		/// <summary>
		/// Updates order's info
		/// </summary>
		/// <param name="order">New order's fields</param>
		/// <param name="id">ID of the order to update</param>
		/// <returns>Nothing</returns>
		[HttpPut("orders/{id}")]
		public ActionResult UpdateOrder([FromBody] Order order, string id)
		{
			if (_ordersService.UpdateOrder(order, id))
			{
				return Ok("Updated successfuly");
			}
			return BadRequest("Something went wrong");
		}
	}
}
