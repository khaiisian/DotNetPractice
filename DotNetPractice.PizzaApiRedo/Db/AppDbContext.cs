using DotNetPractice.PizzaApiRedo.Model;
using Microsoft.EntityFrameworkCore;

namespace DotNetPractice.PizzaApiRedo.Db
{
    public class AppDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
        }

        public DbSet<PizzaModel> Pizzas { get; set; }
        public DbSet<ExtraModel> Extras { get; set; }
    }
}
