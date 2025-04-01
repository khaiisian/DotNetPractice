using DotNetPractice.PizzaApi.Model;
using Microsoft.EntityFrameworkCore;

namespace DotNetPractice.PizzaApi.Db
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
        public DbSet<OrderDetailModel> OrderDetails { get; set; }
    }
}
