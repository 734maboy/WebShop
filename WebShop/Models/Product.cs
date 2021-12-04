using System;
using System.Collections.Generic;
using WebShop.Models;

namespace WebShop.Models
{
	public class Product
	{
		public Guid id { get; set; }
		public string code { get; set; }
		public string name { get; set; }
		public int price { get; set; }
		public Guid categoryId { get; set; }
		public List<OrderItem> orderItems { get; set; }
		public virtual Category category { get; set; }

	}
}
