using DotNetPractice.RestApiWithNLayer.Db;
using DotNetPractice.RestApiWithNLayer.Models;

namespace DotNetPractice.RestApiWithNLayer.Features.Blogs
{
    public class DA_Blogs
    {
        private readonly AppDbContext _context;

        public DA_Blogs()
        {
            _context = new AppDbContext();
        }

        public List<BlogModel> GetBlogs()
        {
            List<BlogModel> lst = _context.Blogs.ToList();
            return lst;
        }

        public BlogModel GetBlog(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x=>x.BlogId == id);
            return item;
        }

        public int CreateBlog(BlogModel requestModel)
        {
            _context.Blogs.Add(requestModel);
            int result = _context.SaveChanges();
            return result;
        }

        public int UpdateBlog(BlogModel requestModel, int id)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null) return 0;

            item.BlogTitle = requestModel.BlogTitle;
            item.BlogContent = requestModel.BlogContent;
            item.BlogAuthor = requestModel.BlogAuthor;
            
            int result = _context.SaveChanges();
            return result;
        }

        public int PatchBlog(BlogModel requestModel, int id)
        {
            var item = _context.Blogs.FirstOrDefault(x=>x.BlogId==id);
            if(item is null) return 0;

            if (!string.IsNullOrEmpty(requestModel.BlogTitle))
            {
                item.BlogTitle = requestModel.BlogTitle;
            }
            if (!string.IsNullOrEmpty(requestModel.BlogContent))
            {
                item.BlogContent= requestModel.BlogContent;
            }
            if(!string.IsNullOrEmpty(requestModel.BlogAuthor))
            {
                item.BlogAuthor = requestModel.BlogAuthor;
            }

            int result = _context.SaveChanges();
            return result;
        }

        public int DeleteBlog(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null) return 0;

            _context.Blogs.Remove(item);
            int result = _context.SaveChanges();
            return result;
        }
    }
}
