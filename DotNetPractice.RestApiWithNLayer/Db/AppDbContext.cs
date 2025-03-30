using DotNetPractice.RestApiWithNLayer.Models;
using DotNetPractice.RestApiWithNLayer.Models.PizzaModels;
using Microsoft.EntityFrameworkCore;

namespace DotNetPractice.RestApiWithNLayer.Db
{
    internal class AppDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.connectionStringBuilder.ConnectionString);
        }

        public DbSet<BlogModel> Blogs { get; set; }
        public DbSet<PizzaModel> Pizzas { get; set; }
        public DbSet<ExtraModel> Extras { get; set; }
    }
}
