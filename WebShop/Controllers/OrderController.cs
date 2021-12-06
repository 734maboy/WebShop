using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WebShop.Models;
using WebShop.Repositories;

namespace WebShop.Controllers
{
	public class OrderController : Controller
	{

		private readonly EFGenericRepository<Order> orderRepo;
		private readonly EFGenericRepository<OrderItem> orderItemRepo;
		private readonly EFGenericRepository<OrderStatus> orderStatusRepo;
		private readonly EFGenericRepository<Customer> customerRepo;
		private AppDbContext appDbContext;

		public OrderController(IServiceProvider serviceProvider)
		{
			appDbContext = serviceProvider.GetService<AppDbContext>();
			orderRepo = new EFGenericRepository<Order>(appDbContext);
			orderItemRepo = new EFGenericRepository<OrderItem>(appDbContext);
			orderStatusRepo = new EFGenericRepository<OrderStatus>(appDbContext);
			customerRepo = new EFGenericRepository<Customer>(appDbContext);
		}

		[Authorize]
		[HttpPost("/getOrders")]
		public IActionResult getOrders()
		{
			var allOrders = orderRepo.Get();

			foreach (var order in allOrders)
			{
				Customer customer = customerRepo.Get(c => c.id == order.customerId).FirstOrDefault();
				OrderStatus status = orderStatusRepo.Get(o => o.id == order.orderStatusId).FirstOrDefault(); 
				order.customer = customer;
				order.orderStatus = status;
				
			}
			return Ok(allOrders);
			//if (allOrders == null)
		}

		[Authorize]
		[HttpPost("/getUserOrders")]
		public IActionResult getUserOrders(Guid customerId)
		{
			var userOrders = orderRepo.Get(o => o.customerId == customerId);
			foreach (var order in userOrders)
			{
				OrderStatus status = orderStatusRepo.Get(s => s.id == order.orderStatusId).FirstOrDefault();
				order.orderStatus = status;
			}
			return Json(userOrders);
		}

		[Authorize]
		[HttpPost("/getOrderStatuses")]
		public IActionResult getOrderStatuses()
		{
			return Json(orderStatusRepo.Get());
		}

		

		[Authorize]
		[HttpPost("/deleteOrder")]
		public IActionResult deleteOrder(Guid orderId)
		{
			Order order = orderRepo.Get(o => o.id == orderId).FirstOrDefault();
			List<OrderItem> orderItems = (List<OrderItem>)orderItemRepo.Get(o => o.orderId == order.id);
			foreach (var item in orderItems)
			{
				orderItemRepo.Remove(item);
			}
			orderRepo.Remove(order);
			return Ok("Заказ удален");
		}

		[Authorize]
		[HttpPost("/closeOrder")]
		public IActionResult closeOrder(Guid orderId)
		{
			Order order = orderRepo.Get(o => o.id ==orderId).FirstOrDefault();
			OrderStatus orderStatus = orderStatusRepo.Get(s => s.name == "Выполнен").FirstOrDefault();
			if (order != null)
			{
				order.orderStatusId = orderStatus.id;
				orderRepo.Update(order);
				return Ok("Заказ успешно закрыт");
			}
			return BadRequest("Заказ не найден");
		}

		[Authorize]
		[HttpPost("/acceptOrder")]
		public IActionResult acceptOrder(Guid orderId, DateTime shipmentDate)
		{
			Order order = orderRepo.Get( o => o.id == orderId).FirstOrDefault();
			OrderStatus orderStatus = orderStatusRepo.Get(s => s.name == "Выполняется").FirstOrDefault();
			if (order != null)
			{
				order.shipmentDate = shipmentDate;
				order.orderStatusId = orderStatus.id;
				orderRepo.Update(order);
				return Ok("Заказ подтверждён");
			}
			return BadRequest("Заказ не найден");
		}


		//[Authorize]
		[HttpPost("/createOrder")]
		public IActionResult createOrder([FromForm]Guid customerId, [FromForm]string items) 
		{
			List<OrderItem> desItems = JsonConvert.DeserializeObject<List<OrderItem>>(items);
			OrderStatus status = orderStatusRepo.Get(s => s.name == "Новый").FirstOrDefault();
			Random random = new Random();
			Order order = new Order()
			{
				id = Guid.NewGuid(),
				customerId = customerId,
				orderDate = DateTime.Now,
				orderStatusId = status.id,
				orderNumber = random.Next(1, 9999),
			};
			orderRepo.Create(order);
			foreach (OrderItem item in desItems)
			{
				item.orderId = order.id;
				orderItemRepo.Create(item);
			}
			order.orderStatus = status;
			return Ok(new
			{
				message = "Заказ успешно добавлен",
				status = 200,
				order = order,
			});
		}

	}
}
