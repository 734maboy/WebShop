using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
	public class UserRole
	{
		[Key]
		public Guid id { get; set; }
		[Required]
		public string roleName { get; set; }
		[System.Text.Json.Serialization.JsonIgnore]
		public List<User> users { get; set; }

	}
}
