using DotNetPractice.RestApiRedo.Model;
using Microsoft.EntityFrameworkCore;

namespace DotNetPractice.RestApiRedo.Db
{
    internal class AppDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
        }

        public DbSet<BlogModel> Blogs { get; set; }
    }
}
