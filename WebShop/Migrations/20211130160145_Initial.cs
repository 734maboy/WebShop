using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using WebShop.Models;

namespace WebShop.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    categoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    roleName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    code = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    price = table.Column<int>(nullable: false),
                    categoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id);
                    table.ForeignKey(
                        name: "FK_Product_Category_categoryId",
                        column: x => x.categoryId,
                        principalTable: "Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    login = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false),
                    roleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_UserRole_roleId",
                        column: x => x.roleId,
                        principalTable: "UserRole",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    code = table.Column<string>(maxLength: 30, nullable: false),
                    address = table.Column<string>(nullable: false),
                    discount = table.Column<int>(nullable: false),
                    userId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.id);
                    table.ForeignKey(
                        name: "FK_Customer_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    orderDate = table.Column<DateTime>(nullable: false),
                    shipmentDate = table.Column<DateTime>(nullable: false),
                    orderNumber = table.Column<int>(nullable: false),
                    orderStatusId = table.Column<Guid>(nullable: false),
                    customerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_customerId",
                        column: x => x.customerId,
                        principalTable: "Customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_OrderStatus_orderStatusId",
                        column: x => x.orderStatusId,
                        principalTable: "OrderStatus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    productsCount = table.Column<int>(nullable: false),
                    productPrice = table.Column<int>(nullable: false),
                    orderId = table.Column<Guid>(nullable: false),
                    productId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_orderId",
                        column: x => x.orderId,
                        principalTable: "Order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product_productId",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_userId",
                table: "Customer",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_customerId",
                table: "Order",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_orderStatusId",
                table: "Order",
                column: "orderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_orderId",
                table: "OrderItem",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_productId",
                table: "OrderItem",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_categoryId",
                table: "Product",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_User_roleId",
                table: "User",
                column: "roleId");

      // Добавление данных в Category

      List<Category> categories = new List<Category>()
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

      foreach (var category in categories)
      {
        migrationBuilder.InsertData(
        table: "Category",
        columns: new[] { "id", "categoryName" },
        values: new object[] { category.id, category.categoryName }
        );
      }



      // Добавление данных в UserRole
      
      List<UserRole> roles = new List<UserRole>()
      {
        new UserRole() { id = Guid.NewGuid(), roleName = "Админ" },
        new UserRole() { id = Guid.NewGuid(), roleName = "Заказчик" },
        new UserRole() { id = Guid.NewGuid(), roleName = "Менеджер" }
      };

      foreach(var role in roles)
      {
        migrationBuilder.InsertData(
          table: "UserRole",
          columns: new[] { "id", "roleName" },
          values: new object[] { role.id, role.roleName }
        );
      }

      // Добавление данных в OrderStatus

      List<OrderStatus> statuses = new List<OrderStatus>()
      {
        new OrderStatus() { id = Guid.NewGuid(), name = "Новый" },
        new OrderStatus() { id = Guid.NewGuid(), name = "Выполняется" },
        new OrderStatus() { id = Guid.NewGuid(), name = "Выполнен" },

      };

      foreach(var status in statuses)
      {
        migrationBuilder.InsertData(
          table: "OrderStatus",
          columns: new[] { "id","name" },
          values: new object[] { status.id, status.name }
        );
      }




      // Добавление данных в User



      List<User> users = new List<User>()
      {
        new User() { id = Guid.NewGuid(), login = "user1", password = "user1", roleId = roles[1].id},
        new User() { id = Guid.NewGuid(), login = "user2", password = "user2", roleId = roles[1].id},
        new User() { id = Guid.NewGuid(), login = "user3", password = "user3", roleId = roles[1].id},
        new User() { id = Guid.NewGuid(), login = "admin", password = "admin", roleId = roles[0].id},
        new User() { id = Guid.NewGuid(), login = "manager1", password = "manager1", roleId = roles[2].id},

      };

      foreach(User user in users)
      {
        migrationBuilder.InsertData(
          table: "User",
          columns: new[] {"id", "login", "password", "roleId"},
          values: new object[] { user.id, user.login, user.password, user.roleId }
        );

      }

      // Добавление данных в Customer

      Random rnd = new Random(); 
      List<Customer> customers = new List<Customer>()
      {
        new Customer() { id = Guid.NewGuid(), name = "Бобрик Евгений Андреевич", code = $"{rnd.Next(1000, 9999)}-2021", address = "Москва, ул. Шафинская, 1", discount = 0, userId = users[0].id},
        new Customer() { id = Guid.NewGuid(), name = "Мазур Андрей Викторович", code = $"{rnd.Next(1000, 9999)}-2021", address = "Апрелевка, ул. Графинная, 22", discount = 0, userId = users[1].id},
        new Customer() { id = Guid.NewGuid(), name = "Валерий Петрович", code = $"{rnd.Next(1000, 9999)}-2021", address = "Москва, ул. Болотниковская, 33", discount = 0, userId = users[2].id},

      };


      foreach(Customer customer in customers)
      {
        migrationBuilder.InsertData(
          table: "Customer",
          columns: new[] { "id", "name", "code", "address", "discount", "userId" },
          values: new object[] { customer.id, customer.name, customer.code, customer.address, customer.discount, customer.userId}
        );
      }


      // Добавление данных в Product
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

      foreach(Product product in products)
      {
        migrationBuilder.InsertData(
          table: "Product",
          columns: new[] { "id", "code", "name", "price", "categoryId"},
          values: new object[] { product.id, product.code, product.name, product.price, product.categoryId }
        );

      }
    }

    protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserRole");
        }
    }
}
