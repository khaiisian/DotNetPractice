using DotNetPractice.CustomService;
using DotNetPractice.RestApiRedo1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPractice.RestApiRedo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapper2Controller : ControllerBase
    {
        private readonly DapperService _dapperService;
        public BlogDapper2Controller()
        {
            _dapperService = new DapperService(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
        }

        [HttpGet]
        public IActionResult GetBlog()
        {
            string query = "select * from Blog_tbl";
            List<BlogModel> lst = _dapperService.QueryToList<BlogModel>(query);
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            var item = FindById(id);
            if (item is null) return NotFound("No data found");
            return Ok(item);
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
            int result = _dapperService.ExecuteQuery(query, model);
            string message = result > 0 ? "Create Success" : "Create Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel model)
        {
            var item = FindById(id);
            if (item == null) return NotFound("No data found");

            string query = @"UPDATE [dbo].[Blog_tbl]
            SET [BlogTitle] = @BlogTitle
            ,[BlogContent] = @BlogContent
            ,[BlogAuthor] = @BlogAuthor
            WHERE BlogId = @BlogId";
            model.BlogId = id;
            int result = _dapperService.ExecuteQuery(query, model);
            string message = result > 0 ? "Update Success" : "Update Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel model)
        {
            var item = FindById(id);
            if (item is null) return NotFound("No data found");
            string condition = string.Empty;

            if (!string.IsNullOrEmpty(model.BlogTitle))
            {
                condition += "[BlogTitle] = @BlogTitle, ";
            }
            if(!string.IsNullOrEmpty(model.BlogContent))
            {
                condition += "[BlogContent] = @BlogContent, ";
            }
            if(!string.IsNullOrEmpty(model.BlogAuthor))
            {
                condition += "[BlogAuthor] = @BlogAuthor, ";
            }
            if (condition.Length == 0) return BadRequest("Nothing to update");
            condition = condition.Substring(0, condition.Length - 2);

            model.BlogId = id;

            string query = $@"UPDATE [dbo].[Blog_tbl]
            SET {condition}
            WHERE BlogId = @BlogId";
            int result = _dapperService.ExecuteQuery(query, model);
            string message = result > 0 ? "Update Success" : "Update Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = "Delete from Blog_tbl where BlogId = @BlogId";
            int result = _dapperService.ExecuteQuery(query, new BlogModel { BlogId = id });
            string message = result > 0 ? "Delete Success" : "Delete Failed";
            return Ok(message);
        }

        private BlogModel? FindById(int id)
        {
            string query = "select * from Blog_tbl where BlogId = @BlogId";
            var item = _dapperService.QueryFirstOrDefault<BlogModel>(query, new BlogModel { BlogId = id });
            return item;
        }
    }
}
