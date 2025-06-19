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
            var blogs = await _context.Blogs
                .OrderByDescending(x=>x.BlogId)
                .ToListAsync();
            return View(blogs);
        }

        [ActionName("Create")]
        public IActionResult CreateBlog()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> CreateBlogAsync(BlogModel requestModel)
        {
            await _context.Blogs.AddAsync(requestModel);
            await _context.SaveChangesAsync();
            return Redirect("/Blog");
        }

        [ActionName("Edit")]
        public async Task<IActionResult> EditBlogAsync(int id)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(x=>x.BlogId == id);
            if (blog is null) return Redirect("/Blog");
            return View("BlogEdit", blog);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> UpdateBlogAsync(int id, BlogModel requestModel)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);

            blog!.BlogTitle = requestModel.BlogTitle;
            blog.BlogContent = requestModel.BlogContent;
            blog.BlogAuthor = requestModel.BlogAuthor;
            await _context.SaveChangesAsync();
            return Redirect("/Blog");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteBlogAsync(int id)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return Redirect("/Blog");
        }
    }
}
