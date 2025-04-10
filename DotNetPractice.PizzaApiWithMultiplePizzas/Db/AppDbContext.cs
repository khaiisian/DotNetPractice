using DotNetPractice.PizzaApiWithMultiplePizzas.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace DotNetPractice.PizzaApiWithMultiplePizzas.Db
{
    public class AppDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.connectionString.ConnectionString);
        }

        public DbSet<PizzaModel> Pizzas { get; set; }
        public DbSet<ExtraModel> Extras { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderItemModel> OrderItems { get; set; }
        public DbSet<OrderDetailModel> OrderDetails { get; set; }
    }
}
