using DotNetPractice.RestApiRedo1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DotNetPractice.RestApiRedo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoDotNetBlogController : ControllerBase
    {
        private SqlConnection _connection;

        public AdoDotNetBlogController()
        {
            _connection = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
        }

        [HttpGet]
        public IActionResult getBlogs()
        {
            //SqlConnection connection = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            _connection.Open();
            Console.WriteLine("Connection Open");
            _connection.Close();
            string query = "select * from Blog_tbl";
            SqlCommand cmd = new SqlCommand(query, _connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            //List<BlogModel> list = new List<BlogModel>();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    BlogModel model = new BlogModel()
            //    {
            //        BlogId = Convert.ToInt32(dr["BlogId"]),
            //        BlogTitle = Convert.ToString(dr["BlogTitle"]),
            //        BlogContent = Convert.ToString(dr["BlogContent"]),
            //        BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            //    };
            //    list.Add(model);
            //}

            List<BlogModel> lst = dt.AsEnumerable().Select(dr=> new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogContent = Convert.ToString(dr["BlogContent"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"])
            }).ToList();

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult getBlogById(int id)
        {
            _connection.Open();
            Console.WriteLine("Connection Open"); 
            string query = "select* from Blog_tbl where BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@BlogId", id);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            _connection.Close();

            
            DataRow dr = dt.Rows[0];
            BlogModel blog = new BlogModel()
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogContent = Convert.ToString(dr["BlogContent"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"])
            };
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel model)
        {
            _connection.Open();
            string query = @"INSERT INTO [dbo].[Blog_tbl]
           ([BlogTitle]
           ,[BlogContent]
           ,[BlogAuthor])
     VALUES
           (@BlogTitle
           ,@BlogContent
           ,@BlogAuthor)";

            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@BlogTitle", model.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);
            cmd.Parameters.AddWithValue("@BlogAuthor", model.BlogAuthor);
            int result = cmd.ExecuteNonQuery();
            _connection.Close();
            string message = result > 0 ? "Create Successful" : "Create Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(BlogModel model, int id)
        {
            _connection.Open();
            string query = @"UPDATE [dbo].[Blog_tbl]
   SET [BlogTitle] = @BlogTitle
      ,[BlogContent] = @BlogContent
      ,[BlogAuthor] = @BlogAuthor
 WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@BlogTitle", model.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);
            cmd.Parameters.AddWithValue("@BlogAuthor", model.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();
            _connection.Close();
            string message = result > 0 ? "Update sucessful" : "Update failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            _connection.Open();
            string query = @"DELETE FROM [dbo].[Blog_tbl]
      WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();
            _connection.Close();
            string message = result > 0 ? "Delete successful" : "Delete Failed";
            return Ok(message);

        }
    }
}
