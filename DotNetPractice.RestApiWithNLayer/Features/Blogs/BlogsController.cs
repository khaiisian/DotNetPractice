using DotNetPractice.RestApiWithNLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPractice.RestApiWithNLayer.Features.Blogs
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly BL_Blogs _blBlogs;

        public BlogsController()
        {
            _blBlogs = new BL_Blogs();
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogModel> lst = _blBlogs.GetBlogs();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = _blBlogs.GetBlog(id);

            if (item is null)
            {
                return NotFound("No data found");
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel requestModel)
        {
            int result = _blBlogs.CreateBlog(requestModel);

            string message = result > 0 ? "Create Successful" : "Create Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(BlogModel requestModel, int id)
        {
            var item = _blBlogs.GetBlog(id);
            if(item is null)
            {
                return NotFound("No data found");
            }

            int result = _blBlogs.UpdateBlog(requestModel, id);
            string message = result > 0 ? "Update successful" : "Update failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(BlogModel requestModel, int id)
        {
            var item = _blBlogs.GetBlog(id);
            if(item is null)
            {
                return NotFound("No data found");
            }

            int result = _blBlogs.PatchBlog(requestModel, id);
            string message = result > 0 ? "Patch Successful" : "Patch Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = _blBlogs.GetBlog(id);
            if(item is null)
            {
                return NotFound("No data found");
            }

            int result = _blBlogs.DeleteBlog(id);
            string message = result > 0 ? "Delete successful" : "Delete Failed";
            return Ok(message);
        }
    }
}
