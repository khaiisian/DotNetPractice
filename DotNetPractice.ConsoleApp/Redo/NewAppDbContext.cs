using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPractice.ConsoleApp.Redo
{
    internal class NewAppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(NewConnectionString.SqlConnectionStringBuilder.ConnectionString);
        }

        public DbSet<NewBlogDto> Blogs { get; set; }
    }
}
