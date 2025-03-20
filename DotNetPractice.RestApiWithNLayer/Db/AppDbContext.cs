using DotNetPractice.RestApiWithNLayer.Models;
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
    }
}
