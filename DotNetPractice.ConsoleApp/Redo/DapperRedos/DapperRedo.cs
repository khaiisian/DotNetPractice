using Dapper;
using DotNetPractice.ConsoleApp;
using DotNetPractice;
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
            //Read();
            //Edit(1);
            //Edit(200);
            //Create("new Title", "new Content", "new Author");
            //Update(10 ,"updated title", "update content", "updated author");
            Delete(10);
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

        public void Edit (int id)
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogDto>("Select * from Blog_tbl where BlogId = @BlogId", new BlogDto { BlogId = id}).FirstOrDefault();

            if(item is null)
            {
                Console.WriteLine("There is no data");
                return;
            }

            Console.WriteLine("BlogId => "+ item.BlogId);
            Console.WriteLine("BlogTitle => " + item.BlogTitle);
            Console.WriteLine("BlogContent => " + item.BlogContent);
            Console.WriteLine("BlogAuthor => " + item.BlogAuthor);
        }

        public void Create(string title, string content, string author)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };
            string query = @"INSERT INTO [dbo].[Blog_tbl]
           ([BlogTitle]
           ,[BlogContent]
           ,[BlogAuthor])
     VALUES
           (@BlogTitle
           ,@BlogContent
           ,@BlogAuthor)";

            IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            Console.WriteLine(message);
            
        }

        public void Update(int id, string title, string content, string author)
        {
            var item = new BlogDto
            {
                BlogId = id,
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };

            string query = @"UPDATE [dbo].[Blog_tbl]
            SET [BlogTitle] = @BlogTitle
            ,[BlogContent] = @BlogContent
            ,[BlogAuthor] = @BlogAuthor
            WHERE BlogId = @BlogId";

            using IDbConnection db = new SqlConnection(NewConnectionString.SqlConnectionStringBuilder.ConnectionString);
            int result  = db.Execute(query, item);

            string message = result > 0 ? "Update Successful" : "Update Failed";
            Console.WriteLine(message);
        }

        public void Delete(int id)
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            string query = @"DELETE FROM [dbo].[Blog_tbl]
      WHERE BlogId = @BlogId";

            int result = db.Execute(query, new BlogDto { BlogId = id });

            string message = result > 0 ? "Delete successful" : "Delete failed";
            Console.WriteLine(message);
        }
    }
}
