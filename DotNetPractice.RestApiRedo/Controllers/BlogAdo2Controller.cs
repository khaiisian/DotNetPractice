using DotNetPractice.RestApiRedo.Model;
using DotNetPractice.SharedRedo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DotNetPractice.RestApiRedo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdo2Controller : ControllerBase
    {
        private readonly AdoDotNetService _adoSerivce;
        private readonly SqlConnection _connection;

        public BlogAdo2Controller()
        {
            _adoSerivce = new AdoDotNetService(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            _connection = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            //SqlConnection connection = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            string query = "Select * from Blog_tbl";
            List<BlogModel> lst = _adoSerivce.QueryToList<BlogModel>(query);

            return Ok(lst);
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            string query = "Select * from Blog_tbl where BlogId = @BlogId";

            var blog = _adoSerivce.QueryFirstOrDefault<BlogModel>(query, new ParamArray("@BlogId", id));
            if(blog is null)
            {
                return NotFound("no data found");
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
            ParamArray[] paramArray = new ParamArray[]
            {
                new ParamArray("BlogTitle", model.BlogTitle),
                new ParamArray("BlogContent", model.BlogContent),
                new ParamArray("BlogAuthor", model.BlogAuthor),
            };

            int result = _adoSerivce.Execute(query, paramArray);

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

            ParamArray[] param = new ParamArray[]
            {
                new ParamArray("BlogId", id),
                new ParamArray("BlogTitle", model.BlogTitle),
                new ParamArray("BlogContent", model.BlogContent),
                new ParamArray("BlogAuthor", model.BlogAuthor)
            };

            int result = _adoSerivce.Execute(query, param);
            string message = result > 0 ? "Update successful" : "Update failed";
            return Ok(message);
        }


        [HttpPatch("id")]
        public IActionResult PatchBlog(BlogModel model,int id)
        {
            string condition = string.Empty;
            //List<ParamModel> paramModels = new List<ParamModel>();
            ParamArray[] paramArrays = new ParamArray[4];

            if (!string.IsNullOrEmpty(model.BlogTitle))
            {
                condition += "[BlogTitle] = @BlogTitle, ";
                paramArrays[0] = new ParamArray("BlogTitle", model.BlogTitle);
            }
            if (!string.IsNullOrEmpty(model.BlogContent))
            {
                condition += "[BlogContent] = @BlogContent, ";
                paramArrays[1] = new ParamArray("BlogContent", model.BlogContent);
            }
            if (!string.IsNullOrEmpty(model.BlogAuthor))
            {
                condition += "[BlogAuthor] = @BlogAuthor, ";
                paramArrays[2] = new ParamArray("BlogAuthor", model.BlogAuthor);
            }

            paramArrays[3] = new ParamArray("BlogId", id);
            if (condition.Length == 0)
            {
                return BadRequest("No data to update");
            }

            condition = condition.Substring(0, condition.Length - 2);


            string query = $@"UPDATE [dbo].[Blog_tbl]
   SET {condition} WHERE BlogId = @BlogId";

            int result = _adoSerivce.PatchExecute(query, paramArrays);

            string message = result > 0 ? "Patching successful" : "Patching Failed";
            return Ok(message);

        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Blog_tbl]
      WHERE BlogId = @BlogId";

            int result = _adoSerivce.Execute(query, new ParamArray("BlogId", id));
            string message = result > 0 ? "Delete successful" : "Delete failed";
            return Ok(message);
        }
    }
}
