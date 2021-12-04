using System;
using System.ComponentModel.DataAnnotations;
using WebShop.Models;

namespace WebShop.Models
{
	public class User
	{
		public Guid id { get; set; }
		[Required]
		public string login { get; set; }
		[Required]
		public string password { get; set; }
		[Required]
		public Guid roleId { get; set; }
		public virtual UserRole role { get; set; }
		public Customer customer { get; set; }
	}
}
