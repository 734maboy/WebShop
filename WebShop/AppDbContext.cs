using Microsoft.EntityFrameworkCore;
using WebShop.Models;

namespace WebShop.Models
{
	public class AppDbContext : DbContext 
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<User> User { get; set; }
		public DbSet<Customer> Customer { get; set; }
		public DbSet<Product> Product { get; set; }
		public DbSet<Category> Category { get; set; }
		public DbSet<Order> Order{ get; set; }
		public DbSet<OrderItem> OrderItem { get; set; }
		public DbSet<OrderStatus> OrderStatus { get; set; }
		public DbSet<UserRole> UserRole { get; set; }
	}
}
