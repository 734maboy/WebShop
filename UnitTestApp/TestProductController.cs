using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebShop.Models;

namespace UnitTestApp
{
	public  class TestProductController
	{
		private List<Product> _products;
		private List<Category> _categories;
		public TestProductController(List<Category> categories)
		{

      
    Random productRnd = new Random();
    Random letterRnd = new Random();
     List<Product> products = new List<Product>()
      {
        new Product()
        {
          id = Guid.NewGuid(),
          code = $"{productRnd.Next(10, 99)}-{productRnd.Next(1000, 9999)}-{letterRnd.Next('A', 'Z' + 1).ToString()}{letterRnd.Next('A', 'Z' + 1).ToString()}{productRnd.Next(10, 99)}",
          name = "Стул",
          price = productRnd.Next(1000, 9999),
          categoryId = categories[0].id
        },
        new Product()
        {
          id = Guid.NewGuid(),
          code = $"{productRnd.Next(10, 99)}-{productRnd.Next(1000, 9999)}-{letterRnd.Next('A', 'Z' + 1).ToString()}{letterRnd.Next('A', 'Z' + 1).ToString()}{productRnd.Next(10, 99)}",
          name = "Компьютер PC High Gaming Z390",
          price = productRnd.Next(30000, 40000),
          categoryId = categories[1].id
        },
        new Product()
        {
          id = Guid.NewGuid(),
          code = $"{productRnd.Next(10, 99)}-{productRnd.Next(1000, 9999)}-{letterRnd.Next('A', 'Z' + 1).ToString()}{letterRnd.Next('A', 'Z' + 1).ToString()}{productRnd.Next(10, 99)}",
          name = "Вилка обычная",
          price = productRnd.Next(30, 99),
          categoryId = categories[2].id
        },
        new Product()
        {
          id = Guid.NewGuid(),
          code = $"{productRnd.Next(10, 99)}-{productRnd.Next(1000, 9999)}-{letterRnd.Next('A', 'Z' + 1).ToString()}{letterRnd.Next('A', 'Z' + 1).ToString()}{productRnd.Next(10, 99)}",
          name = "Светильник XS-3000",
          price = productRnd.Next(1000, 5000),
          categoryId = categories[3].id
        },
        new Product()
        {
          id = Guid.NewGuid(),
          code = $"{productRnd.Next(10, 99)}-{productRnd.Next(1000, 9999)}-{letterRnd.Next('A', 'Z' + 1).ToString()}{letterRnd.Next('A', 'Z' + 1).ToString()}{productRnd.Next(10, 99)}",
          name = "Тазик waterContainer 12hgg100",
          price = productRnd.Next(1000, 9999),
          categoryId = categories[4].id
        },
        new Product()
        {
          id = Guid.NewGuid(),
          code = $"{productRnd.Next(10, 99)}-{productRnd.Next(1000, 9999)}-{letterRnd.Next('A', 'Z' + 1).ToString()}{letterRnd.Next('A', 'Z' + 1).ToString()}{productRnd.Next(10, 99)}",
          name = "Крем для чистки обуви",
          price = productRnd.Next(1000, 9999),
          categoryId = categories[5].id
        },


      };
      _products = products;
			_categories = categories;
		}


		public IEnumerable<Product> GetProducts()
		{
			List<Product> products = _products;
			foreach (Product product in products)
			{
				product.category = _categories.FirstOrDefault(c => c.id == product.categoryId);
			}
			return products;
		}

		public IEnumerable<Category> getCategories()
		{
			return _categories;
		}


		public Product createProduct(string name, int price, Guid categoryId)
		{
			Random productRnd = new Random();
			Random letterRnd = new Random();
			Product product = new Product()
			{
				id = Guid.NewGuid(),
				code = $"{productRnd.Next(10, 99)}-{productRnd.Next(1000, 9999)}-{letterRnd.Next('A', 'Z' + 1).ToString()}{letterRnd.Next('A', 'Z' + 1).ToString()}{productRnd.Next(10, 99)}",
				name = name,
				price = price,
				categoryId = categoryId,
			};
			_products.Add(product);
			product.category = _categories.FirstOrDefault(c => c.id == categoryId);
			return product;
		}


		public Boolean deleteProduct(Guid id)
		{
			Boolean isDeleted = _products.Remove(_products.FirstOrDefault( p => p.id == id));
			return isDeleted;
		}

		public String updateProduct(Guid id, string name, int price, Guid categoryId)
		{
			var product = _products.FirstOrDefault(p => p.id == id);
			if (product == null) return "Product Not Found";
			product.name = name;
			product.price = price;
			product.categoryId = categoryId;
			return "Product updated";
		}
	}
}
