using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
	public class Category
	{
		[Key]
		public Guid id { get; set; }
		public string categoryName { get; set; }
		public List<Product> products { get; set; }

	}
}
