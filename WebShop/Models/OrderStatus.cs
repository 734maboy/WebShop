using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
	public class OrderStatus
	{
		[Key]
		public Guid id { get; set; }
		[Required, MaxLength(50)]
		public string name { get; set; }
		public List<Order> orders { get; set; }
	}
}
