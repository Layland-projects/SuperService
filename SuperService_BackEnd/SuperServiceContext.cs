using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Text;
using SuperService_BackEnd.Models;
using System.Security.Cryptography.X509Certificates;

namespace SuperService_BackEnd
{
    public class SuperServiceContext : DbContext
    {
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<ItemIngredients> ItemIngredients { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuperService;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            # region ItemIngredientsMappingTable
            //modelBuilder.Entity<ItemIngredients>()
            //    .HasKey(x => x.ItemIngredientID);
            //modelBuilder.Entity<ItemIngredients>()
            //    .HasOne(ii => ii.Item)
            //    .WithMany(ing => ing.ItemIngredients)
            //    .HasForeignKey(ii => ii.ItemID);
            //modelBuilder.Entity<ItemIngredients>()
            //    .HasOne(ii => ii.Ingredient)
            //    .WithMany(it => it.ItemIngredients)
            //    .HasForeignKey(ii => ii.IngredientID);
            #endregion
            #region OrderItemsMappingTable
            //modelBuilder.Entity<OrderItems>()
            //    .HasKey(x => new { x.OrderID, x.ItemID });
            //modelBuilder.Entity<OrderItems>()
            //    .HasOne(x => x.Item)
            //    .WithMany(x => x.Orders)
            //    .HasForeignKey(x => x.ItemID);
            //modelBuilder.Entity<OrderItems>()
            //    .HasOne(x => x.Order)
            //    .WithMany(x => x.Items)
            //    .HasForeignKey(x => x.OrderID);
            #endregion
            #region UsersTable
            //modelBuilder.Entity<User>()
            //    .HasOne(x => x.UserType)
            //    .WithMany(x => x.Users)
            //    .HasForeignKey(x => x.UserTypeID);
            #endregion
        }
    }
}
