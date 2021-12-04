using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using WebShop.Models;
using WebShop.Repositories;

namespace WebShop.Controllers
{
	
	public class ProductController : Controller
	{
		private readonly EFGenericRepository<Product> productRepo;
		private readonly EFGenericRepository<Category> categoryRepo;
		private readonly EFGenericRepository<OrderItem> orderItemRepo;
		private readonly EFGenericRepository<OrderStatus> orderStatusRepo;
		private AppDbContext appDbContext;

		public ProductController(IServiceProvider serviceProvider)
		{
			appDbContext = serviceProvider.GetService<AppDbContext>();
			productRepo = new EFGenericRepository<Product>(appDbContext);
			categoryRepo = new EFGenericRepository<Category>(appDbContext);
			orderItemRepo = new EFGenericRepository<OrderItem>(appDbContext);
		}

		[Authorize]
		[HttpPost("/getProducts")]
		public IActionResult GetProducts()
		{
			List<Product> products = (List<Product>)productRepo.Get();
			foreach (Product product in products)
			{
				product.category = categoryRepo.FindById(product.categoryId);
			}
			return Json(products);
		}

		[Authorize]
		[HttpPost("/getCategories")]
		public IActionResult getCategories()
		{
			return Json(categoryRepo.Get());
		}

		[Authorize]
		[HttpPost("/createProduct")]
		public IActionResult createProduct(string name, int price, Guid categoryId)
		{
			Random productRnd = new Random();
			Random letterRnd = new Random();
			Product product = new Product()
			{
				id = Guid.NewGuid(),
				code = $"{productRnd.Next(10, 99)}-{productRnd.Next(1000, 9999)}-{letterRnd.Next('A', 'Z' + 1).ToString()}{letterRnd.Next('A', 'Z' + 1).ToString()}{productRnd.Next(10, 99)}",
				name = name,
				price = price,
				categoryId = categoryId,
			};
			productRepo.Create(product);
			product.category = categoryRepo.Get(c => c.id == categoryId).FirstOrDefault();
			return Ok(product);
		}

		[Authorize]
		[HttpPost("/deleteProduct")]
		public IActionResult deleteProduct(Guid id)
		{
			OrderItem orderItem = orderItemRepo.Get( x => x.productId == id).FirstOrDefault();
			Console.WriteLine(orderItem);
			if (orderItem != null) return BadRequest(new {
				message = "Данный товар принадлежит заказам",
				status = 400,
			});
			productRepo.Remove(productRepo.FindById(id));
			return Ok();
		}
		[Authorize]
		[HttpPost("/updateProduct")]
		public IActionResult updateProduct(Guid id, string name, int price, Guid categoryId)
		{
			var product = productRepo.FindById(id);
			product.name = name;
			product.price = price;
			product.categoryId = categoryId;
			productRepo.Update(product);
			return Ok();
		}

		

	}
}
