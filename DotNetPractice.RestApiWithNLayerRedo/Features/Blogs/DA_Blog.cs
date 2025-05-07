using DotNetPractice.RestApiWithNLayerRedo.Db;
using DotNetPractice.RestApiWithNLayerRedo.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetPractice.RestApiWithNLayerRedo.Features.Blogs
{
    public class DA_Blog
    {
        private readonly AppDbContext _appDbContext;

        public DA_Blog()
        {
            _appDbContext = new AppDbContext();
        }

        public async Task<List<BlogModel>> getBlogsAsync()
        {
            var blogs = await _appDbContext.Blogs.ToListAsync();
            return blogs;
        }

        public async Task<BlogModel> getBlogAsync(int id)
        {
            var blog = await _appDbContext.Blogs.FirstOrDefaultAsync(x=>x.BlogId == id);
            return blog;
        }

        public async Task<int> createBlogAsync(BlogModel requestModel)
        {
            await _appDbContext.Blogs.AddAsync(requestModel);
            int result = await _appDbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> updateBlogAsync(int id, BlogModel requestModel)
        {
            var blog = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (blog == null) return 0;

            blog.BlogTitle = requestModel.BlogTitle;
            blog.BlogAuthor = requestModel.BlogAuthor;
            blog.BlogContent = requestModel.BlogContent;

            int result = await _appDbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteBlogAsync(int id)
        {
            var blog = await _appDbContext.Blogs.FirstOrDefaultAsync(x=>x.BlogId==id);
            if (blog == null) return 0;
            _appDbContext.Remove(blog);
            int result = await _appDbContext.SaveChangesAsync();
            return result;
        }
    }
}
