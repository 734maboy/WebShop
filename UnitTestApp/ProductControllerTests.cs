using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShop.Models;
using Xunit;

namespace UnitTestApp
{
	public class ProductControllerTests
	{
    private List<Category> categories = new List<Category>()
    {
      new Category()
      {
        id = Guid.NewGuid(),
        categoryName = "Столовые приборы"
      },
      new Category()
      {
        id = Guid.NewGuid(),
        categoryName = "Компьютеры"
      },
      new Category()
      {
        id = Guid.NewGuid(),
        categoryName = "Ванная"
      },
      new Category()
      {
        id = Guid.NewGuid(),
        categoryName = "Мебель"
      },
      new Category()
      {
        id = Guid.NewGuid(),
        categoryName = "Телефоны"
      },
      new Category()
      {
        id = Guid.NewGuid(),
        categoryName = "Растения"
      },
    };
    private object[] idxs =
    {
        new
				{
          id = Guid.NewGuid(),
				},
        new
				{
          id = Guid.NewGuid(),
				},
        new
				{
          id = Guid.NewGuid(),
				},
        new
				{
          id = Guid.NewGuid(),
				},
        new
				{
          id = Guid.NewGuid(),
				},
        new
				{
          id = Guid.NewGuid(),
				},
        new
				{
          id = Guid.NewGuid(),
				},
    };

    [Fact]
    public void GetListOfProducts()
		{
      TestProductController controller = new TestProductController(categories);

      List<Product> result = (List<Product>)controller.GetProducts();

      Assert.Equal(6, result.Count);
		}

    [Fact]
    public void GetListOfCategories()
		{
			TestProductController controller = new TestProductController(categories);

      List<Category> cats = (List<Category>)controller.getCategories();

      Assert.Equal(categories.ElementAt(0), cats.ElementAt(0));
		}

    [Fact]
    public void SuccessedDeleteProduct()
		{
      TestProductController controller = new TestProductController(categories);

      List<Product> prods = (List<Product>)controller.GetProducts();

      Boolean isDeleted = controller.deleteProduct(prods.ElementAt(0).id);

      Assert.Equal(isDeleted, true);
		}

    [Fact]
    public void FailedDeleteProduct()
    {
      TestProductController controller = new TestProductController(categories);

      List<Product> prods = (List<Product>)controller.GetProducts();

      Boolean isDeleted = controller.deleteProduct(categories.ElementAt(0).id);

      Assert.Equal(isDeleted, false);
    }

    [Fact]
    public void SuccessedUpdateProduct()
		{
      TestProductController controller = new TestProductController(categories);
      List<Product> prods = (List<Product>)controller.GetProducts();
      String response = controller.updateProduct(prods.ElementAt(0).id, "Another name", 400, categories.ElementAt(4).id);

      Assert.Equal(response, "Product updated");
		}
    [Fact]
    public void FailedUpdateProduct()
		{
      TestProductController controller = new TestProductController(categories);
      List<Product> prods = (List<Product>)controller.GetProducts();
      String response = controller.updateProduct(categories.ElementAt(0).id, "Another name", 400, categories.ElementAt(4).id);

      Assert.Equal(response, "Product Not Found");
		}




  }
}
