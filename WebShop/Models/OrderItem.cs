using System;
using System.ComponentModel.DataAnnotations;
using WebShop.Models;

namespace WebShop.Models
{
	public class OrderItem
	{
		[Key]
		public Guid id { get; set; }
		[Required]
		public int productsCount { get; set; }
		[Required]
		public int productPrice { get; set; }
		[Required]
		public Guid orderId { get; set; }
		[Required]
		public Guid productId { get; set; }
		public virtual Product product { get; set; }
		public virtual Order order { get; set; }
	}
}
