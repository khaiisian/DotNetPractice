using DotNetPractice.RestApiRedo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DotNetPractice.RestApiRedo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoController : ControllerBase
    {
        private readonly SqlConnection _connection;

        public BlogAdoController()
        {
            _connection = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString); ;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            //SqlConnection connection = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            string query = "Select * from Blog_tbl";
            _connection.Open();
            SqlCommand cmd = new SqlCommand(query, _connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            _connection.Close();

            List<BlogModel> list = new List<BlogModel>();
            foreach(DataRow dr in dt.Rows)
            {
                var blog = new BlogModel()
                {
                    BlogId = Convert.ToInt32(dr["BlogId"]),
                    BlogTitle = Convert.ToString(dr["BlogTitle"]),
                    BlogContent = Convert.ToString(dr["BlogContent"]),
                    BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                };

                list.Add(blog);
            }

            List<BlogModel> lst = dt.AsEnumerable().Select(dr=>new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogContent = Convert.ToString(dr["BlogContent"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            }).ToList();

            return Ok(lst);
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            string query = "Select * from Blog_tbl where BlogId = @BlogId";
            _connection.Open();
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            _connection.Close();

            if (dt.Rows.Count == 0)
            {
                return NotFound("no data found");
            }

            DataRow dr = dt.Rows[0];

            BlogModel blog = new BlogModel()
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"]),
            };
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel model)
        {
            string query = @"INSERT INTO [dbo].[Blog_tbl]
           ([BlogTitle]
           ,[BlogContent]
           ,[BlogAuthor])
     VALUES
           (@BlogTitle
           ,@BlogContent
           ,@BlogAuthor)";

            _connection.Open();
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("BlogTitle", model.BlogTitle);
            cmd.Parameters.AddWithValue("BlogContent", model.BlogContent);
            cmd.Parameters.AddWithValue("BlogAuthor", model.BlogAuthor);
            int result = cmd.ExecuteNonQuery();

            string message = result > 0 ? "Create Successful" : "Create Failed";
            return Ok(message);
        }

        [HttpPut("id")]
        public IActionResult UpdateBlog(BlogModel model, int id)
        {
            string query = @"UPDATE [dbo].[Blog_tbl]
   SET [BlogTitle] = @BlogTitle
      ,[BlogContent] = @BlogContent
      ,[BlogAuthor] = @BlogAuthor
 WHERE BlogId = @BlogId";

            _connection.Open();
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("BlogTitle", model.BlogTitle);
            cmd.Parameters.AddWithValue("BlogContent", model.BlogContent);
            cmd.Parameters.AddWithValue("BlogAuthor", model.BlogAuthor);
            cmd.Parameters.AddWithValue("BlogId", id);
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Update successful" : "Update failed";
            return Ok(message);
        }


        [HttpPatch("id")]
        public IActionResult PatchBlog(BlogModel model,int id)
        {
            string condition = string.Empty;
            List<ParamModel> paramModels = new List<ParamModel>();

            if (!string.IsNullOrEmpty(model.BlogTitle))
            {
                condition += "[BlogTitle] = @BlogTitle, ";
                paramModels.Add(new ParamModel("BlogTitle", model.BlogTitle));
            }
            if (!string.IsNullOrEmpty(model.BlogContent))
            {
                condition += "[BlogContent] = @BlogContent, ";
                paramModels.Add(new ParamModel("BlogContent", model.BlogContent));
            }
            if (!string.IsNullOrEmpty(model.BlogAuthor))
            {
                condition += "[BlogAuthor] = @BlogAuthor, ";
                paramModels.Add(new ParamModel("BlogAuthor", model.BlogAuthor));
            }

            paramModels.Add(new ParamModel("BlogId", id));
            if (condition.Length == 0)
            {
                return BadRequest("No data to update");
            }

            condition = condition.Substring(0, condition.Length - 2);


            string query = $@"UPDATE [dbo].[Blog_tbl]
   SET {condition} WHERE BlogId = @BlogId";

            _connection.Open();
            SqlCommand cmd = new SqlCommand(query, _connection);
            foreach(ParamModel param in paramModels)
            {
                cmd.Parameters.AddWithValue(param.Name, param.Value);
            }

            int result = cmd.ExecuteNonQuery();
            _connection.Close();

            string message = result > 0 ? "Patching successful" : "Patching Failed";
            return Ok(message);

        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Blog_tbl]
      WHERE BlogId = @BlogId";
            _connection.Open();
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("BlogId", id);
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Delete successful" : "Delete failed";
            return Ok(message);
        }
    }
}
