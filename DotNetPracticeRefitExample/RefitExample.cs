using Microsoft.VisualBasic;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPracticeRefitExample
{
    internal class RefitExample
    {
        private readonly iBlogApi _service = RestService.For<iBlogApi>("https://localhost:7184");

        public async Task RunAsync()
        {
            //await ReadAysnc();
            //await EditAsync(1000);
            //await CreateAsync("refit title", "refit content", "refit author");
            //await UpdateAsync(34, "update title", "update content", "update author");
            await DeleteAsync(34);
            
        }

        public async Task ReadAysnc()
        {
            var lst = await _service.GetBlogs();
            foreach (var item in lst)
            {
                Console.WriteLine($"BlogTitle => {item.BlogId}");
                Console.WriteLine($"BlogTitle => {item.BlogTitle}");
                Console.WriteLine($"BlogTitle => {item.BlogContent}");
                Console.WriteLine($"BlogTitle => {item.BlogAuthor}");
                Console.WriteLine("===================================================");
            }
        }

        public async Task EditAsync(int id)
        {
            try
            {
                var item = await _service.GetBlog(id);
                Console.WriteLine($"BlogId => {item.BlogId}");
                Console.WriteLine($"BlogId => {item.BlogTitle}");
                Console.WriteLine($"BlogId => {item.BlogContent}");
                Console.WriteLine($"BlogId => {item.BlogAuthor}");
            } catch (ApiException ex)
            {
                Console.WriteLine("Status Code: " + ex.StatusCode);
                Console.WriteLine("Message: " + ex.Content);
            }
        }

        public async Task CreateAsync (string title, string content,string author)
        {
            BlogModel blog = new BlogModel()
            {
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };
            var messaage = await _service.CreateBlog(blog);
            Console.WriteLine(messaage);
        }

        public async Task UpdateAsync(int id, string title, string content,string author)
        {
            BlogModel blog = new BlogModel()
            {
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };
            var message = await _service.UpdateBlog(id, blog);
            Console.WriteLine(message);
        }

        public async Task DeleteAsync(int id)
        {
            var message = await _service.DeleteBlog(id);
            Console.WriteLine(message);
        }
    }
}
