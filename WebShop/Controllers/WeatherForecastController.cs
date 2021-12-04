using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models;
using WebShop.Repositories;

namespace WebShop.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{

		private readonly EFGenericRepository<User> genereicRepo;
		private AppDbContext appDbContext;

		private static readonly string[] Summaries = new[]
		{
						"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
				};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(IServiceProvider serviceProvider)
		{
			appDbContext = serviceProvider.GetService<AppDbContext>();
			genereicRepo = new EFGenericRepository<User>(appDbContext);
		}

		[HttpGet]
		public IEnumerable<User> Get()
		{
			var result = new List<User>();
			result = genereicRepo.Get().ToList();
			return result;
			/*
			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			})
			.ToArray();
			*/
		}
	}
}
