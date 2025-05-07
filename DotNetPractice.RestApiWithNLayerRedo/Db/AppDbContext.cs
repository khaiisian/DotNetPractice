using DotNetPractice.RestApiWithNLayerRedo.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetPractice.RestApiWithNLayerRedo.Db
{
    public class AppDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.sqlConnectionString.ConnectionString);
        }
        public DbSet<BlogModel> Blogs { get; set; }
    }
}
