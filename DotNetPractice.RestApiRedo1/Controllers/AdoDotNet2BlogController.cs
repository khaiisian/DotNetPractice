using DotNetPractice.CustomService;
using DotNetPractice.RestApiRedo1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPractice.RestApiRedo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoDotNet2BlogController : ControllerBase
    {
        private readonly AdoDotNetService _adoService;

        public AdoDotNet2BlogController(AdoDotNetService adoService)
        {
            _adoService = adoService;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from Blog_tbl";
            List<BlogModel> blogs = _adoService.QueryList<BlogModel>(query);
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from Blog_tbl where BlogId = @BlogId";

            //AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
            //parameters[0] = new AdoDotNetParameter("BlogId", id);

            //AdoDotNetParameter[] parameters = new AdoDotNetParameter[]
            //{
            //    new AdoDotNetParameter("BlogId", id)
            //};
             BlogModel blog = _adoService.QueryFirstOrDefault<BlogModel>(query,  new AdoDotNetParameter("BlogId", id));
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel requestModel)
        {
            string query = @"INSERT INTO [dbo].[Blog_tbl]
           ([BlogTitle]
           ,[BlogContent]
           ,[BlogAuthor])
     VALUES
           (@BlogTitle
           ,@BlogContent
           ,@BlogAuthor)";

            AdoDotNetParameter[] parameters = new AdoDotNetParameter[]
            {
                new AdoDotNetParameter("BlogTitle", requestModel.BlogTitle),
                new AdoDotNetParameter("BlogContent", requestModel.BlogContent),
                new AdoDotNetParameter("BlogAuthor", requestModel.BlogAuthor),
            };

            int result = _adoService.QueryExecute(query, parameters);
            string message = result > 0 ? "Create Successful" : "Create Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel requestModel)
        {
            string query = @"UPDATE [dbo].[Blog_tbl]
            SET [BlogTitle] = @BlogTitle
            ,[BlogContent] = @BlogContent
            ,[BlogAuthor] = @BlogAuthor
            WHERE BlogId = @BlogId";

            AdoDotNetParameter[] parameter = new AdoDotNetParameter[]
            {
                new AdoDotNetParameter("BlogTitle", requestModel.BlogTitle),
                new AdoDotNetParameter("BlogContent", requestModel.BlogContent),
                new AdoDotNetParameter("BlogAuthor", requestModel.BlogAuthor),
                new AdoDotNetParameter("BlogId", id),
            };

            int result = _adoService.QueryExecute(query,parameter);
            string message = result > 0 ? "Update sucessful" : "Update Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel requestModel)
        {
            string condiion = string.Empty;
            AdoDotNetParameter[] parameters = new AdoDotNetParameter[4];

            if (!string.IsNullOrEmpty(requestModel.BlogTitle))
            {
                condiion += "[BlogTitle] = @BlogTitle, ";
                parameters[0] = new AdoDotNetParameter("BlogTitle", requestModel.BlogTitle);
            };
            if (!string.IsNullOrEmpty(requestModel.BlogContent))
            {
                condiion += "[BlogContent] = @BlogContent, ";
                parameters[1] = new AdoDotNetParameter("BlogContent", requestModel.BlogContent);
            };
            if (!string.IsNullOrEmpty(requestModel.BlogAuthor))
            {
                condiion += "[BlogAuthor] = @BlogAuthor, ";
                parameters[2] = new AdoDotNetParameter("BlogAuthor", requestModel.BlogAuthor);
            };
            parameters[3] = new AdoDotNetParameter("BlogId", id);
            if (condiion.Length == 0) return BadRequest("No data to upddate");
            condiion = condiion.Substring(0, condiion.Length - 2);

            string query = $@"UPDATE [dbo].[Blog_tbl]
            SET {condiion} WHERE BlogId = @BlogId";

            int result = _adoService.PatchQueryExecute(query, parameters);
            string message = result > 0 ? "Patch Successful" : "Patch Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[Blog_tbl]
      WHERE BlogId = @BlogId";

            int result = _adoService.QueryExecute(query, new AdoDotNetParameter("BlogId", id));
            string message = result > 0 ? "Delete Failed" : "Delete Successful";
            return Ok(message);
        }
    }
}
