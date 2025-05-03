using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPracticeRefitExample;

internal interface iBlogApi
{
    [Get("/api/blogs")]
    Task<List<BlogModel>> GetBlogs();

    [Get("/api/blogs/{id}")]
    Task<BlogModel> GetBlog(int id);

    [Post("/api/blogs")]
    Task<string> CreateBlog(BlogModel model); 

    [Put("/api/blogs/{id}")]
    Task<string> UpdateBlog(int id, BlogModel model);

    [Delete("/api/blogs/{id}")]
    Task<string> DeleteBlog(int id);


}

public class BlogModel
{
    public int BlogId { get; set; }
    public string? BlogTitle { get; set;}
    public string? BlogContent { get; set; }
    public string? BlogAuthor { get; set; }
}