using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebShop.Models;
using WebShop.Repositories;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace WebShop.Controllers
{
	
	public class UserController : Controller
	{

		private readonly EFGenericRepository<User> genereicRepo;
		private readonly EFGenericRepository<Customer> customerRepo;
		private readonly EFGenericRepository<UserRole> roleRepo;
		private readonly EFGenericRepository<Order> orderRepo;
		private AppDbContext appDbContext;
		
		public UserController(IServiceProvider serviceProvider)
		{
			appDbContext = serviceProvider.GetService<AppDbContext>();
			genereicRepo = new EFGenericRepository<User>(appDbContext);
			customerRepo = new EFGenericRepository<Customer>(appDbContext);
			roleRepo = new EFGenericRepository<UserRole>(appDbContext);
			orderRepo = new EFGenericRepository<Order>(appDbContext);
		}
		

		[HttpPost("/getToken")]
		public IActionResult getToken(string login, string password)
		{
			var identity = GetIdentity(login, password);
			if (identity == null)
			{
				return BadRequest(new { message = "Неправильный логин или пароль", status = 400, });
			}

			var now = DateTime.UtcNow;
			// создаем JWT-токен
			var jwt = new JwtSecurityToken(
							issuer: AuthOptions.ISSUER,
							audience: AuthOptions.AUDIENCE,
							notBefore: now,
							claims: identity.Claims,
							expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
							signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
			User user = genereicRepo.Get(x => x.login == login).FirstOrDefault();
			UserRole role = roleRepo.Get(x => x.id == user.roleId).FirstOrDefault();
			
			var response = new
			{
				access_token = encodedJwt,
				id = new Guid(),
				customerId = new Guid(),
				login = user.login,
				name = "",
				roleId = role.id,
				role = role.roleName,
				discount = 0,
				address = "",
				code = "",
				orders = new List<Order>(),
			};

			if (role.roleName == "Заказчик")
			{
				Customer customer = customerRepo.Get(x => x.userId == user.id).FirstOrDefault();
				List<Order> orders = orderRepo.Get(o => o.customerId == customer.id).ToList();
				response = new
				{
					access_token = encodedJwt,
					id = user.id,
					customerId = customer.id,	
					login = user.login,
					name = customer.name,
					roleId = role.id,
					role = role.roleName,
					discount = customer.discount,
					address = customer.address,
					code = customer.code,
					orders = orders,

				};
			}


			return Json(response);
		}

		private ClaimsIdentity GetIdentity(string login, string password)
		{
			User person = genereicRepo.Get(x => x.login == login && x.password == password).FirstOrDefault();
			if (person != null)
			{
				var claims = new List<Claim>
								{
										new Claim(ClaimsIdentity.DefaultNameClaimType, person.login),
										new Claim(ClaimsIdentity.DefaultRoleClaimType, person.roleId.ToString()),
								};
				ClaimsIdentity claimsIdentity =
				new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
						ClaimsIdentity.DefaultRoleClaimType);
				return claimsIdentity;
			}

			// если пользователя не найдено
			return null;
		}

		[Authorize]
		[HttpPost("/getUsers")]
		public IActionResult getUsers()
		{
			List<User> users = (List<User>)genereicRepo.Get();
			foreach (var user in users)
			{
				user.role = roleRepo.Get(r => r.id == user.roleId).FirstOrDefault();
				user.customer = customerRepo.Get(c => c.userId == user.id).FirstOrDefault();
			}
			return Json(users);
		}

		[Authorize]
		[HttpPost("/getCustomerInfo")]
		public IActionResult getCustomerInfo(Guid userId)
		{
			User user = genereicRepo.Get(u => u.id == userId).FirstOrDefault();
			Customer customer = customerRepo.Get(c => c.userId == userId).FirstOrDefault();
			if (customer != null)
			{
				return Json(customer);
			}
			return BadRequest("Информация о заказчике не найдена");
		}

		[HttpPost("/createUser")]
		public IActionResult createUser( string login, string password, Guid roleId, string name = null, string address = "", int discount = 0)
		{
			var isUserExists = genereicRepo.Get(u => u.login == login).FirstOrDefault();
			if (isUserExists != null)
			{
				return BadRequest(new { 
					message = "Пользователь с таким логином существует!",
					status = 400,
				});
			}
			User newUser = new User()
			{
				id = Guid.NewGuid(),
				login = login,
				password = password,
				roleId = roleId,
			};
			genereicRepo.Create(newUser);
			newUser.role = roleRepo.Get(r => r.id == newUser.roleId).FirstOrDefault();
			Random random = new Random();
			if (name != null)
			{
				Customer newCustomer = new Customer()
				{
					id= Guid.NewGuid(),
					code = $"{random.Next(1000, 9999)}-{DateTime.Today.Year}",
					name = name,
					address = address,
					discount = discount,
					userId = newUser.id,
				};
				customerRepo.Create(newCustomer);
				newUser.customer = newCustomer;
				return Json(new
					{
						message = "Пользователь с ролью заказчик успешно создан",
						status = 200,
						user = newUser,

					});
			}
			return Json(new
			{
				message = "Пользователь успешно создан",
				status = 200,
				user = newUser,
			});
		}

		[Authorize]
		[HttpPost("/deleteUser")]
		public IActionResult deleteUser(Guid id)
		{
			User deletableUser = genereicRepo.Get(x => x.id == id).FirstOrDefault();
			
			if (deletableUser != null)
			{
				genereicRepo.Remove(deletableUser);
				return Ok();
			} else
			{
				return BadRequest();
			}
		}

		[Authorize]
		[HttpPost("/updateUser")]
		public IActionResult updateUser(Guid id,string login, string password, Guid roleId, string name = null, string address = "", int discount = 0)
		{
			var user = genereicRepo.FindById(id);
			var customer = customerRepo.Get(c => c.userId == user.id).FirstOrDefault();
			var role = roleRepo.Get(c => c.id == roleId).FirstOrDefault();

			user.login = login;
			user.password = password;

			Random random = new Random();

			switch (role.roleName) {
				case "Заказчик":
					{
						if (customer == null)
						{
							Customer newCustomer = new Customer()
							{
								id = Guid.NewGuid(),
								name = name,
								address = address,
								discount = discount,
								code = $"{random.Next(1000, 9999)}-{DateTime.Today.Year}",
								userId = user.id,
							};
							customerRepo.Create(newCustomer);
							user.roleId = roleId;
							genereicRepo.Update(user);
							user.customer = newCustomer;
							user.role = roleRepo.Get(r => r.id == roleId).FirstOrDefault();
							return Ok(
							new	{
								message = "Пользователь обновлён -> Переведен в роль 'Заказчика'",
								status = 200,
								user = user,
							});
						} else
						{
							customer.name = name;
							customer.discount= discount;
							customer.address = address;
							customerRepo.Update(customer);
							genereicRepo.Update(user);
							user.customer = customer;
							user.role = role;
							return Ok(new
							{
								message = "Заказчик успешно обновлён",
								status = 200,
								user,
							});
						}
						break;
					}
				default: 
					{
						if (customer != null)
						{
							var orders = orderRepo.Get(o => o.customerId == customer.id).FirstOrDefault();
							if (orders != null) return BadRequest("У данного пользователя имеются активные заказы"); else
							{
								customerRepo.Remove(customer);
								user.roleId = roleId;
								genereicRepo.Update(user);
								user.role = roleRepo.Get( r => r.id == roleId ).FirstOrDefault();
								return Ok(new
								{
									user = user,
								});
							}
						} else
						{
							user.roleId = roleId;
							genereicRepo.Update(user);
							user.role = roleRepo.Get(r => r.id == roleId).FirstOrDefault();
							return Ok(new
							{
								user = user,
							});
						}
						break;
					}
			}
		}

		[HttpPost("/getRoles")]
		public IActionResult getRoles()
		{
			return Json(roleRepo.Get());
		}
	}

}

