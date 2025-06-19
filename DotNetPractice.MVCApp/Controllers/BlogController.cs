using DotNetPractice.MVCApp.Db;
using DotNetPractice.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetPractice.MVCApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var blogs = await _context.Blogs.ToListAsync();
            return View(blogs);
        }

        [ActionName("Create")]
        public IActionResult CreateBlog()
        {
            return View("BlogCreate");
        }


        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> CreateBlogAsync(BlogModel blog)
        {
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return Redirect("/Blog");
        }
    }
}
