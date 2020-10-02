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
        public DbSet<OrderStatus> OrderStatuses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuperService;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
