using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop;
using WebShop.Models;

namespace WebShop
{
	public class Startup
	{
		readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
		private IConfigurationRoot _confString;
		public Startup(IWebHostEnvironment hostEnv)
		{
			_confString = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors();
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
										.AddJwtBearer(options =>
										{
											options.RequireHttpsMetadata = false;
											options.TokenValidationParameters = new TokenValidationParameters
											{
												ValidateIssuer = true,
												ValidIssuer = AuthOptions.ISSUER,

												ValidateAudience = true,
												ValidAudience = AuthOptions.AUDIENCE,
												
												ValidateLifetime = true,

												// установка ключа безопасности
												IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
												// валидация ключа безопасности
												ValidateIssuerSigningKey = true,
											};
										});
			services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection"),
				b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
			services.AddMvc();
			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseDeveloperExceptionPage();
			app.UseCors(builder => builder.AllowAnyHeader().AllowAnyOrigin());
			app.UseCors(MyAllowSpecificOrigins);
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
