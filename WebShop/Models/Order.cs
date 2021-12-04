using System;
using System.ComponentModel.DataAnnotations;
using WebShop.Models;

namespace WebShop.Models
{
	public class Order
	{
		[Key]
		public Guid id { get; set; }
		[Required]
		public DateTime orderDate { get; set; }
		public DateTime shipmentDate { get; set; }
		[Required]
		public int orderNumber { get; set; }
		[Required]
		public Guid orderStatusId { get; set; }
		public Guid customerId { get; set; }
		public Customer customer { get; set; }
		public OrderStatus orderStatus { get; set; }

	}
}
