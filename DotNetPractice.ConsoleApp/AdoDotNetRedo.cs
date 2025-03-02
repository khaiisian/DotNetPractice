using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPractice.ConsoleApp
{
    internal class AdoDotNetRedo
    {
        private readonly SqlConnectionStringBuilder _sqcConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "DotNetSelfStudy",
            UserID = "sa",
            Password = "sa@123"
        };

        public void Read()
        {
            SqlConnection connection = new SqlConnection(_sqcConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");
            connection.Close();

            string quey = "Select * from Blog_tbl";
            SqlCommand cmd = new SqlCommand(quey, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            foreach(DataRow row in dt.Rows)
            {
                Console.WriteLine("Blog Id => " + row["BlogId"]);
                Console.WriteLine("BlogTitle => " + row["BlogTitle"]);
                Console.WriteLine("BlogContent => " + row["BlogContent"]);
                Console.WriteLine("BlogAuthor => " + row["BlogAuthor"]);
                Console.WriteLine("---------------------------------------------------------------");
            }
        }

        public void Edit(int id)
        {
            SqlConnection connection = new SqlConnection(_sqcConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");
            connection.Close();

            string query = "Select * from Blog_tbl where BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if(dt.Rows.Count == 0)
            {
                Console.WriteLine("There is no data");
                return;
            }

            DataRow dr = dt.Rows[0];

            Console.WriteLine("Blog Id => " + dr["BlogId"]);
            Console.WriteLine("BlogTitle => " + dr["BlogTitle"]);
            Console.WriteLine("BlogContent => " + dr["BlogContent"]);
            Console.WriteLine("BlogAuthor => " + dr["BlogAuthor"]);
            Console.WriteLine("---------------------------------------------------------------");
        }


        public void Create(string title, string content, string author)
        {
            SqlConnection connection = new SqlConnection(_sqcConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");

            string query = @"INSERT INTO [dbo].[Blog_tbl]
           ([BlogTitle]
           ,[BlogContent]
           ,[BlogAuthor])
     VALUES
           (@BlogTitle
           ,@BlogContent
           ,@BlogAuthor)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Saving successful" : "Saving Failed";
            Console.WriteLine(message);
        }

        public void Update(int id, string title, string content, string author)
        {
            SqlConnection connection = new SqlConnection( _sqcConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");

            string query = @"UPDATE [dbo].[Blog_tbl]
   SET [BlogTitle] = @BlogTitle
      ,[BlogContent] = @BlogContent
      ,[BlogAuthor] = @BlogAuthor
 WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            int result = cmd.ExecuteNonQuery();
            connection.Close ();

            string message = result > 0 ? "Update Successful" : "Update Failed";
            Console.WriteLine(message);
        }

        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_sqcConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connnection Open");

            string query = @"DELETE FROM [dbo].[Blog_tbl]
      WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand (query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery ();
            connection.Close ();

            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            Console.Write (message);
        }
    }
}
