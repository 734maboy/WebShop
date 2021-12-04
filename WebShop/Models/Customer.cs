using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebShop.Models;

namespace WebShop.Models
{
	public class Customer
	{
		[Key]
		public Guid id { get; set; }
		[Required, MaxLength(50)]
		public string name { get; set; }
		[Required, MaxLength(30)]
		public string code { get; set; }
		[Required]
		public string address { get; set; }
		[Required]
		public int discount { get; set; } = 0;
		public Guid userId	{ get; set; }
		[System.Text.Json.Serialization.JsonIgnore]
		public virtual User user { get; set; }
		public List<Order> orders { get; set; }
	}
}
