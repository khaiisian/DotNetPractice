using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPractice.ConsoleApp
{
    internal class DapperRedo
    {
        public void Run()
        {
            Read();
        }

        public void Read()
        {
            using IDbConnection db = new SqlConnection (ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            List<NewBlogDto> lst = db.Query<NewBlogDto>("select * from Blog_tbl").ToList();

            foreach(NewBlogDto item in lst)
            {
                Console.WriteLine("BlogId => "+ item.BlogId);
                Console.WriteLine("BlogTitle => "+ item.BlogTitle);
                Console.WriteLine("BlogContent => "+ item.BlogContent);
                Console.WriteLine("BlogAuthor => "+ item.BlogAuthor);
                Console.WriteLine("----------------------------------------------------");
            }
        }
    }
}
