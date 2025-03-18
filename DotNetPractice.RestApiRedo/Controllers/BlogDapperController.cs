using Dapper;
using DotNetPractice.RestApiRedo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DotNetPractice.RestApiRedo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        private readonly IDbConnection _connection;

        public BlogDapperController()
        {
            _connection = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from Blog_tbl";
            List<BlogModel> lst = _connection.Query<BlogModel>(query).ToList();
            return Ok(lst);
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {

            var blog = FindById(id);

            if (blog is null)
            {
                return NotFound("No data found");
            }

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

            int result = _connection.Execute(query, model);
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

            var blog = FindById(id);
            if (blog is null)
            {
                return NotFound("No data found to update");
            }

            model.BlogId = blog.BlogId;

            int result = _connection.Execute(query, model);
            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }


        [HttpPatch("id")]
        public IActionResult PatchBlogs(BlogModel model, int id)
        {
            string condition = string.Empty;

            if(!string.IsNullOrEmpty(model.BlogTitle))
            {
                condition += " [BlogTitle] = @BlogTitle, ";
            }
            if(!string.IsNullOrEmpty(model.BlogContent))
            {
                condition += " [BlogContent] = @BlogContent, ";
            }
            if (!string.IsNullOrEmpty(model.BlogAuthor))
            {
                condition += " [BlogAuthor] = @BlogAuthor, ";
            }

            if(condition.Length == 0)
            {
                return BadRequest("No data to update");
            }
            condition = condition.Substring(0, condition.Length - 2);

            var blog = FindById(id);
            if (blog is null)
            {
                return NotFound("No data found");
            }

            model.BlogId = blog.BlogId;

            string query = $@"UPDATE [dbo].[Blog_tbl]
            SET {condition}
            WHERE BlogId = @BlogId";

            model.BlogId = blog.BlogId;
            int result = _connection.Execute(query, model);

            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }

        [HttpDelete("id")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[Blog_tbl]
      WHERE BlogId = @BlogId";

            var blog = FindById(id);
            if(blog is null)
            {
                return NotFound("no data found");
            }
            int result = _connection.Execute(query, new BlogModel { BlogId=id});

            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            return Ok(message);
        }


        private BlogModel? FindById(int id)
        {
            string query = "select * from Blog_tbl where BlogId = @BlogId";
            var blog = _connection.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();
            return blog;
        }
    }

}
