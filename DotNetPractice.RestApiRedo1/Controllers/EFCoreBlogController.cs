using DotNetPractice.RestApiRedo1.Db;
using DotNetPractice.RestApiRedo1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DotNetPractice.RestApiRedo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFCoreBlogController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public EFCoreBlogController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult getBlogs()
        {
            var blogs = _appDbContext.Blogs.ToList();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult getBlog(int id)
        {
            var blog = findById(id);
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult createBlogs(BlogModel requestModel)
        {
            _appDbContext.Blogs.Add(requestModel);
            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult updateBlog(int id, BlogModel requestModel)
        {
            var blog = findById(id);
            if (blog == null) return NotFound("No Blog Found to update");

            blog.BlogTitle = requestModel.BlogTitle;
            blog.BlogContent = requestModel.BlogContent;
            blog.BlogAuthor = requestModel.BlogAuthor;
            int result = _appDbContext.SaveChanges();

            string message = result > 0 ? "Update successful" : "Update Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult patchBlog(int id, BlogModel requestModel)
        {
            var blog = findById(id);
            if (blog == null) return NotFound("No data found to update");

            if (!string.IsNullOrEmpty(requestModel.BlogTitle))
            {
                blog.BlogTitle = requestModel.BlogTitle;
            }
            if (!string.IsNullOrEmpty(requestModel.BlogContent))
            {
                blog.BlogContent = requestModel.BlogContent;
            }
            if (!string.IsNullOrEmpty(requestModel.BlogAuthor))
            {
                blog.BlogAuthor = requestModel.BlogAuthor;
            }

            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Patch Success" : "Patch Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult deleteBlog(int id)
        {
            var blog = findById(id);
            if (blog == null) return NotFound("No blog found to delete");
            _appDbContext.Blogs.Remove(blog);
            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            return Ok(message);
        }
    
        private BlogModel ? findById (int id)
        {
            var blog = _appDbContext.Blogs.FirstOrDefault(x=>x.BlogId == id);
            return blog;
        }
    }
}
