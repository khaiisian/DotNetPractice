using DotNetPractice.MVCApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetPractice.MVCApp.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogModel> Blogs { get; set; }
    }
}
