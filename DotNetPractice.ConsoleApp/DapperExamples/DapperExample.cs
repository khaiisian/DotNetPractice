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
    internal class DapperExample
    {
        public void Run()
        {
            //Edit(1);
            //Edit(1111);
            //Create("New TITLE", "NEW CONTENET", "NEW AUTHOR");
            //Update(8, "UPDATED TITLE", "UPDATED CONTENET", "UPDATED AUTHOR");
            Delete(9);
            Read();
        }

        private void Read()
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            List<BlogDto> lst = db.Query<BlogDto>("Select * from Blog_tbl").ToList();

            foreach (BlogDto item in lst)
            {
                Console.WriteLine("Blog Id => " + item.BlogId);
                Console.WriteLine("BlogTitle => " + item.BlogTitle);
                Console.WriteLine("BlogContent => " + item.BlogContent);
                Console.WriteLine("BlogAuthor => " + item.BlogAuthor);
                Console.WriteLine("--------------------------------------------------------------");
            }
        }

        private void Edit(int id)
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogDto>("Select * from Blog_tbl where BlogId = @BlogId", new BlogDto { BlogId = id }).FirstOrDefault();
            if (item is null)
            {
                Console.WriteLine("No data Found");
                return;
            }

            Console.WriteLine("BlogId => " + item.BlogId);
            Console.WriteLine("BlogTitle => " + item.BlogTitle);
            Console.WriteLine("BlogContent => " + item.BlogContent);
            Console.WriteLine("BlogAuthor => " + item.BlogAuthor);
            Console.WriteLine("---------------------------------------------------------------");
        }

        private void Create(string title, string content, string author)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };

            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            string query = @"INSERT INTO [dbo].[Blog_tbl]
           ([BlogTitle]
           ,[BlogContent]
           ,[BlogAuthor])
     VALUES
           (@BlogTitle
           ,@BlogContent
           ,@BlogAuthor)";
            int result = db.Execute(query, item);

            string message = result > 0 ? "Saving successful" : "Saving failed";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string content, string author)
        {
            var item = new BlogDto
            {
                BlogId = id,
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };

            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            string query = @"UPDATE [dbo].[Blog_tbl]
            SET [BlogTitle] = @BlogTitle
            ,[BlogContent] = @BlogContent
            ,[BlogAuthor] = @BlogAuthor
            WHERE BlogId = @BlogId";
            int result = db.Execute(query, item);
            string message = result > 0 ? "Update Successful" : "Update Failed";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            string query = @"DELETE FROM [dbo].[Blog_tbl]
      WHERE BlogId = @BlogId";
            int result = db.Execute(query, new BlogDto { BlogId=id });
            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            Console.WriteLine(message);
        }
    }
}
