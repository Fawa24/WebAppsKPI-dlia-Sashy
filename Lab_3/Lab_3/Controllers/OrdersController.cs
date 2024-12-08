using Lab_1.Interfaces;
using Lab_2.Filters;
using Lab_2.Models.DTOs;
using Lab_3.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Lab_1.Controllers
{
	public class OrdersController : ControllerBase
	{
		private readonly IMemoryCache _cache;
		private readonly IOrdersService _ordersService;

		public OrdersController(IMemoryCache cache
			, IOrdersService ordersService)
		{
			_cache = cache;
			_ordersService = ordersService;
		}

		/// <summary>
		/// Returns all the existing orders
		/// </summary>
		/// <returns>All the orders from the storage</returns>
		[HttpGet("orders/")]
		public async Task<ActionResult<List<GetOrderDTO>>> GetAllOrders()
		{
			if (_cache.TryGetValue(AppConfig.Config.OrdersCacheKey, out List<GetOrderDTO> orders))
			{
				return Ok(orders);
			}

			orders = await _ordersService.GetAllOrders();
			if (AppConfig.Config.CacheEnabled)
			{
				_cache.Set(AppConfig.Config.OrdersCacheKey, orders, TimeSpan.FromMinutes(5));
			}

			return Ok(orders);
		}

		/// <summary>
		/// Returns the order with certain ID
		/// </summary>
		/// <param name="id">ID of the order to return</param>
		/// <returns>Order with the specified ID</returns>
		[HttpGet("orders/{id}")]
		public async Task<ActionResult<GetOrderDTO>> GetOrderById(string id)
		{
			var order = await _ordersService.GetOrderById(id);

			if (order == null)
			{
				return BadRequest($"Invalid input: no order has {id} id");
			}

			return Ok(order);
		}

		/// <summary>
		/// Adds new order to the storage
		/// </summary>
		/// <param name="order">Order to add</param>
		/// <returns>Nothing</returns>
		[HttpPost("orders/")]
		[ValidationFilter]
		public async Task<ActionResult> AddOrders([FromBody] AddOrderDTO order)
		{
			if (await _ordersService.AddOrder(order))
			{
				_cache.Remove(AppConfig.Config.OrdersCacheKey);
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
		public async Task<ActionResult> DeleteOrderById(string id)
		{
			if (await _ordersService.DeleteOrderById(id))
			{
				_cache.Remove(AppConfig.Config.OrdersCacheKey);
				return Ok("Deleted successfully");
			}

			return BadRequest($"Invalid input: no order has {id} id");
		}

		/// <summary>
		/// Updates order's info
		/// </summary>
		/// <param name="order">New order's fields</param>
		/// <returns>Nothing</returns>
		[HttpPut("orders/{id}")]
		[ValidationFilter]
		public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderDTO order, string id)
		{
			if (await _ordersService.UpdateOrder(id, order))
			{
				_cache.Remove(AppConfig.Config.OrdersCacheKey);
				return Ok("Updated successfuly");
			}
			return BadRequest("Something went wrong");
		}
	}
}
