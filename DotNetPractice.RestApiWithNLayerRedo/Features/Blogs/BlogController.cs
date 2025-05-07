using DotNetPractice.RestApiWithNLayerRedo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPractice.RestApiWithNLayerRedo.Features.Blogs
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog _bl_Blog;
        public BlogController()
        {
            _bl_Blog = new BL_Blog();
        }
        [HttpGet]
        public async Task<IActionResult> GetBlogsAsync()
        {
            List<BlogModel> blogs = await _bl_Blog.getBlogsAsync();
            return Ok(blogs);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetBlogAsync(int id)
        {
            BlogModel blog = await _bl_Blog.getBlogAsync(id);
            if(blog == null) return NotFound("no data found");
            return Ok(blog);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogAsync(BlogModel requestModel)
        {
            int result = await _bl_Blog.createBlogAsync(requestModel);
            string message = result > 0 ? "Blog create successful" : "Blog create successful";
            return Ok(message);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateBlogAsync(int id, BlogModel requestModel)
        {
            int result = await _bl_Blog.updateBlogAsync(id, requestModel);
            string message = result > 0 ? "Blog update successful" : "Blog update failed";
            return Ok(message);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteBlogAsync(int id)
        {
            int result = await _bl_Blog.deleteBlogAsync(id);
            string message = result > 0 ? "Blog delete successful" : "Blog delete failed";
            return Ok(message);
      }
    }
}
