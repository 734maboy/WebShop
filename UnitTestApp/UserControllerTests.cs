using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using WebShop;
using WebShop.Controllers;
using WebShop.Models;
using WebShop.Repositories;
using Xunit;

namespace UnitTestApp
{
	public class UserControllerTests
	{
		
		[Fact]
		public  void TokenCheck()
		{
			//var controller = new UserController();	
		}


	}
}


