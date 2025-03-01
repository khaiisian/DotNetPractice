using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPractice.ConsoleApp
{
    internal class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "DotNetSelfStudy",
            UserID = "sa",
            Password = "sa@123",
        };


        public void Read()
        {
            //SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            //sqlConnectionStringBuilder.DataSource = ".";
            //sqlConnectionStringBuilder.InitialCatalog = "DotNetSelfStudy";
            //sqlConnectionStringBuilder.UserID = "sa";
            //sqlConnectionStringBuilder.Password = "sa@123";
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            connection.Open();
            Console.WriteLine("Connection Open");
            connection.Close();

            string query = "select * from Blog_tbl";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            foreach(DataRow dr in dt.Rows)
            {
                Console.WriteLine("BlogID =>" + dr["BlogId"]);
                Console.WriteLine("BlogTitle =>" + dr["BlogTitle"]);
                Console.WriteLine("BlogContent =>" + dr["BlogContent"]);
                Console.WriteLine("BlogAuthor =>" + dr["BlogAuthor"]);
            }
        }

        public void Edit(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            string query = "select * from Blog_tbl where BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if(dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found");
                return;
            }

            DataRow dr = dt.Rows[0];
            Console.WriteLine("BlogId => " + dr["BlogId"]);
            Console.WriteLine("BlogTitle => " + dr["BlogTitle"]);
            Console.WriteLine("BlogContent => " + dr["BlogContent"]);
            Console.WriteLine("BlogAuthor => " + dr["BlogAuthor"]);
        }

        public void Create(string title, string content, string author)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"INSERT INTO [dbo].[Blog_tbl]
           ([BlogTitle]
           ,[BlogContent]
           ,[BlogAuthor])
     VALUES
           (@BlogTitle
           ,@BlogContent
           ,@BlogAuthor)";
            SqlCommand cmd = new SqlCommand(query,connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            int result = cmd.ExecuteNonQuery();

            string message = result > 0 ? "Create Successful" : "Create Failed";
            Console.WriteLine(message);

            connection.Close();
        }

        public void Update(int id, string title, string content, string author)
        {
            SqlConnection sqlConnection = new SqlConnection( _sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            string query = @"UPDATE [dbo].[Blog_tbl]
   SET [BlogTitle] = @BlogTitle
      ,[BlogContent] = @BlogContent
      ,[BlogAuthor] = @BlogAuthor
 WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);

            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Update successful" : "Update Failed";
            Console.WriteLine(message);

            sqlConnection.Close();
        }

        public void Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            string query = @"DELETE FROM [dbo].[Blog_tbl]
      WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();

            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            Console.WriteLine(message);

            sqlConnection.Close();
        }
    }

}
