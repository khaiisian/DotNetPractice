
using DotNetPractice.RestApiWithNLayerRedo.Models;

namespace DotNetPractice.RestApiWithNLayerRedo.Features.Blogs
{
    public class BL_Blog
    {
        private readonly DA_Blog _da_Blog;
        public BL_Blog()
        {
            _da_Blog = new DA_Blog();
        }

        public async Task<List<BlogModel>> getBlogsAsync()
        {
            var blogs = await _da_Blog.getBlogsAsync();
            return blogs;
        }

        public async Task<BlogModel> getBlogAsync(int id)
        {
            var blog = await _da_Blog.getBlogAsync(id);
            return blog;
        }

        public async Task<int> createBlogAsync(BlogModel requestModel)
        {
            int result = await _da_Blog.createBlogAsync(requestModel);
            return result;
        }

        public async Task<int> updateBlogAsync(int id, BlogModel requestModel)
        {
            int result = await _da_Blog.updateBlogAsync(id, requestModel);
            return result;
        }

        public async Task<int> deleteBlogAsync(int id)
        {
            int result = await _da_Blog.DeleteBlogAsync(id);
            return result;
        }
    }
}
