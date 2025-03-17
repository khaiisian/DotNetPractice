using DotNetPractice.RestApiRedo.Db;
using DotNetPractice.RestApiRedo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPractice.RestApiRedo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogEFCoreController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BlogEFCoreController()
        {
            _context = new AppDbContext();
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var blogs = _context.Blogs.ToList();
            return Ok(blogs);
        }

        [HttpGet("id")]
        public IActionResult GetId(int id) 
        {
            var blog = _context.Blogs.FirstOrDefault(x=>x.BlogId== id);
            if(blog is null)
            {
                return NotFound("no data found");
            }
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel model)
        {
            _context.Add(model);
            int result = _context.SaveChanges();

            string message = result > 0 ? "Create successful" : "Create failed";
            return Ok(message);
        }

        [HttpPut("id")]
        public IActionResult UpdateBlog(BlogModel model, int id)
        {
            var blog = _context.Blogs.FirstOrDefault(x=>x.BlogId== id);
            if(blog is null)
            {
                return NotFound("no data found");
            }

            blog.BlogTitle = model.BlogTitle;
            blog.BlogContent = model.BlogContent;
            blog.BlogAuthor = model.BlogAuthor;
            int result = _context.SaveChanges();

            string message = result > 0 ? "Upating Successful" : "Upating Failed";
            return Ok(message);
        }

        [HttpPatch("id")]
        public IActionResult PatchBlog(BlogModel model, int id)
        {
            var blog = _context.Blogs.FirstOrDefault(x=>x.BlogId== id);
            if(blog is null)
            {
                return NotFound("no data found");
            }

            if (!string.IsNullOrEmpty(model.BlogTitle))
            {
                blog.BlogTitle = model.BlogTitle;
            }

            if (!string.IsNullOrEmpty(model.BlogContent))
            {
                blog.BlogContent = model.BlogContent;
            }

            if (!string.IsNullOrEmpty(model.BlogAuthor))
            {
                blog.BlogAuthor = model.BlogAuthor;
            }

            int result = _context.SaveChanges();
            string message = result > 0 ? "Patching Successsful" : "Patching Failed";
            return Ok(message);
        }

        [HttpDelete("id")]
        public IActionResult DeleteBlog(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(x=>x.BlogId== id);
            if(blog is null )
            {
                return NotFound("no data found");
            }

            _context.Remove(blog);
            int result = _context.SaveChanges();
            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            return Ok(message);
        }
    }
}
