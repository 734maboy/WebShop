// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebShop.Models;

namespace WebShop.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebShop.Models.Category", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("categoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("WebShop.Models.Customer", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<int>("discount")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("userId")
                        .IsUnique();

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("WebShop.Models.Order", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("customerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("orderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("orderNumber")
                        .HasColumnType("int");

                    b.Property<Guid>("orderStatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("shipmentDate")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("customerId");

                    b.HasIndex("orderStatusId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("WebShop.Models.OrderItem", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("orderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("productId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("productPrice")
                        .HasColumnType("int");

                    b.Property<int>("productsCount")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("orderId");

                    b.HasIndex("productId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("WebShop.Models.OrderStatus", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("id");

                    b.ToTable("OrderStatus");
                });

            modelBuilder.Entity("WebShop.Models.Product", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("categoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("categoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("WebShop.Models.User", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("roleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("roleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("WebShop.Models.UserRole", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("roleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("WebShop.Models.Customer", b =>
                {
                    b.HasOne("WebShop.Models.User", "user")
                        .WithOne("customer")
                        .HasForeignKey("WebShop.Models.Customer", "userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebShop.Models.Order", b =>
                {
                    b.HasOne("WebShop.Models.Customer", "customer")
                        .WithMany("orders")
                        .HasForeignKey("customerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebShop.Models.OrderStatus", "orderStatus")
                        .WithMany("orders")
                        .HasForeignKey("orderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebShop.Models.OrderItem", b =>
                {
                    b.HasOne("WebShop.Models.Order", "order")
                        .WithMany()
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebShop.Models.Product", "product")
                        .WithMany("orderItems")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebShop.Models.Product", b =>
                {
                    b.HasOne("WebShop.Models.Category", "category")
                        .WithMany("products")
                        .HasForeignKey("categoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebShop.Models.User", b =>
                {
                    b.HasOne("WebShop.Models.UserRole", "role")
                        .WithMany("users")
                        .HasForeignKey("roleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
