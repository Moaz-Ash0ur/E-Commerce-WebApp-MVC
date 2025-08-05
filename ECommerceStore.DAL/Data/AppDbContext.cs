using ECommerceStore.Entities;
using ECommerceStore.Entities.Views;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using static ECommerceStore.Entities.Order;

namespace ECommerceStore.DAL.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductDetailsVM> ProductDetails { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderDetailsVw> OrdersDetailsVw { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductDetailsVM>().HasNoKey().ToView("vw_ProductDetails");
            modelBuilder.Entity<OrderDetailsVw>().HasNoKey().ToView("vw_OrderDetails");


            
            modelBuilder.Entity<Product>()
                    .HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId);


            modelBuilder.Entity<Product>()
                      .HasOne(p => p.Brand)
                      .WithMany(b => b.Products)
                      .HasForeignKey(p => p.BrandId);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            //make soft delete use global query filter to not get the item delete = true
            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Brand>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(b => !b.IsDeleted);


            modelBuilder.Entity<OrderItem>()
           .HasOne(o => o.Product)
           .WithMany()
           .HasForeignKey(o => o.ProductId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(o => o.OrderId);


        
       modelBuilder.Entity<Order>()
          .Property(a => a.paymentMethod)
          .HasConversion(
          v => v.ToString(),
          v => (PaymentMethod)Enum.Parse(typeof(PaymentMethod), v)
      );
            modelBuilder.Entity<Order>()
        .Property(a => a.paymentStatus)
        .HasConversion(
        v => v.ToString(),
        v => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), v)
    );

            modelBuilder.Entity<Order>()
        .Property(a => a.orderStatus)
        .HasConversion(
        v => v.ToString(),
        v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v)
    );

        }


    }
}
