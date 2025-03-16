using DotNetPractice.RestApi.Model;
using DotNetPractice.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DotNetPractice.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdo2Controller : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from Blog_tbl";
            List<BlogModel> lst = _adoDotNetService.QueryList<BlogModel>(query);
            return Ok(lst);
        }


        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from Blog_tbl where BlogId = @BlogId";

            //AdoParameters[] parameters = new AdoParameters[1];
            //parameters[0] = new AdoParameters("BlogId", id);

            //AdoParameters[] parameters = new AdoParameters[]
            //{
            //    new AdoParameters("BlogId", id)
            //};
            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoParameters("BlogId", id));
            if(item is null)
            {
                return NotFound("No data found");
            }

            return Ok(item);
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


            AdoParameters[] parameters = new AdoParameters[]
            {
                new AdoParameters("BlogTitle", blog.BlogTitle),
                new AdoParameters("BlogContent", blog.BlogContent),
                new AdoParameters("BlogAuthor", blog.BlogAuthor),
            };
            int result = _adoDotNetService.Execute(query, parameters);
            string message = result > 0 ? "Saving successful" : "Saving Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            string query = @"UPDATE [dbo].[Blog_tbl]
   SET [BlogTitle] = @BlogTitle
      ,[BlogContent] = @BlogContent
      ,[BlogAuthor] = @BlogAuthor
 WHERE BlogId = @BlogId";


            AdoParameters[] parameters = new AdoParameters[]
            {
                new AdoParameters("BlogTitle", blog.BlogTitle),
                new AdoParameters("BlogContent", blog.BlogContent),
                new AdoParameters("BlogAuthor", blog.BlogAuthor),
                new AdoParameters("BlogId", id),
            };
            int result = _adoDotNetService.Execute(query, parameters);

            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            string condition = string.Empty;
            AdoParameters[] parameters = new AdoParameters[4];
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                condition += "[BlogTitle] = @BlogTitle, ";
                parameters[0] = new AdoParameters("BlogTitle", blog.BlogTitle);
            }
            if(!string.IsNullOrEmpty(blog.BlogContent))
            {
                condition += "[BlogContent] = @BlogContent, ";
                parameters[1] = new AdoParameters("BlogContent", blog.BlogContent);
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                condition += "[BlogAuthor] = @BlogAuthor, ";
                parameters[2] = new AdoParameters("BlogAuthor", blog.BlogAuthor);
            }


            parameters[3] = new AdoParameters("BlogId", id);
            if (condition.Length == 0)
            {
                return NotFound("Nothing to update");
            }

            condition = condition.Substring(0, condition.Length - 2);

            string query = $@"UPDATE [dbo].[Blog_tbl]
   SET {condition} WHERE BlogId = @BlogId";

            int result = _adoDotNetService.PatchExecute(query, parameters);            
            string message = result > 0 ? "Patching Successful" : "Patching Failed";
            return Ok(message);

        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[Blog_tbl]
      WHERE BlogId = @BlogId";


            SqlConnection _connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            _connection.Open();
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("BlogId", id);
            int result = cmd.ExecuteNonQuery();
            _connection.Close();
            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            return Ok(message);
        }
    }
}
