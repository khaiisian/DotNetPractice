using DotNetPractice.RestApiRedo1.Model;
using Microsoft.EntityFrameworkCore;

namespace DotNetPractice.RestApiRedo1.Db
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogModel> Blogs { get; set; }
    }
}
