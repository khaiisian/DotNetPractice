using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using DotNetPractice.RestApiRedo1.Model;

namespace DotNetPractice.RestApiRedo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DapperBlogController : ControllerBase
    {
        public readonly IDbConnection _connection;

        public DapperBlogController()
        {
            _connection = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "Select * from Blog_tbl";
            List<BlogModel> blogs = _connection.Query<BlogModel>(query).ToList();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var blog = findById(id);
            if (blog == null) return NotFound("No data found");
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Blog_tbl]
           ([BlogTitle]
           ,[BlogContent]
           ,[BlogAuthor])
     VALUES
           (@BlogTitle
           ,@BlogContent
           ,@BlogAuthor)";

            int result = _connection.Execute(query, blog);
            string message = result > 0 ? "Create Blog Success" : "Create Blog Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(BlogModel model, int id)
        {
            var blog = findById(id);
            if (blog is null) return NotFound("no data found");

            string query = @"UPDATE [dbo].[Blog_tbl]
   SET [BlogTitle] = @BlogTitle
      ,[BlogContent] = @BlogContent
      ,[BlogAuthor] = @BlogAuthor
 WHERE BlogId = @BlogId";

            model.BlogId = id;
            int result = _connection.Execute(query, model);
            string message = result > 0 ? "Update Blog Success" : "Update Blog Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(BlogModel model, int id)
        {
            var blog = findById(id);
            if (blog is null) return NotFound("No data found");

            string condition = string.Empty;
            if (!string.IsNullOrEmpty(model.BlogTitle))
            {
                condition += "[BlogTitle] = @BlogTitle, ";
            }
            if(!string.IsNullOrEmpty(model.BlogContent))
            {
                condition += "[BlogContent] = @BlogContent, ";
            }
            if (!string.IsNullOrEmpty(model.BlogAuthor))
            {
                condition += "[BlogAuthor] = @BlogAuthor, ";
            }

            if (condition.Length == 0) return NotFound("No data to update");
            condition = condition.Substring(0, condition.Length - 2);
            
            string query = $@"UPDATE [dbo].[Blog_tbl]
   SET {condition}
 WHERE BlogId = @BlogId";

            model.BlogId = id;
            int result = _connection.Execute(query, model);
            string message = result > 0 ? "Patch Success" : "Patch Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var blog = findById(id);
            if (blog is null) return NotFound("No data found to delete");

            string query = "Delete from Blog_tbl where BlogId = @BlogId";
            int result = _connection.Execute(query, new BlogModel { BlogId = id});
            string message = result > 0 ? "Delete success" : "Delete Failed";
            return Ok(message);
        }

        private BlogModel ?findById (int id)
        {
            string query0 = "select * from Blog_tbl where BlogId = @BlogId";
            var item = _connection.Query<BlogModel>(query0, new BlogModel { BlogId = id }).FirstOrDefault();
            return item;
        }
    }
}
