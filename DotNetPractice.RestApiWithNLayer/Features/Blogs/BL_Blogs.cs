using DotNetPractice.RestApiWithNLayer.Models;

namespace DotNetPractice.RestApiWithNLayer.Features.Blogs
{
    public class BL_Blogs
    {
        private readonly DA_Blogs _daBlogs;

        public BL_Blogs()
        {
            _daBlogs = new DA_Blogs();
        }

        public List<BlogModel> GetBlogs()
        {
            List<BlogModel> lst = _daBlogs.GetBlogs();
            return lst;
        }

        public BlogModel GetBlog(int id)
        {
            var item = _daBlogs.GetBlog(id);
            return item;
        }

        public int CreateBlog(BlogModel requestModel)
        {
            int result = _daBlogs.CreateBlog(requestModel);
            return result;
        }

        public int UpdateBlog(BlogModel requestModel, int id)
        {
            int result = _daBlogs.UpdateBlog(requestModel, id);
            return result;
        }

        public int PatchBlog(BlogModel requestModel, int id)
        {
            int result = _daBlogs.PatchBlog(requestModel, id);
            return result;
        }

        public int DeleteBlog(int id)
        {
            int result = _daBlogs.DeleteBlog(id);
            return result;
        }
    }
}
