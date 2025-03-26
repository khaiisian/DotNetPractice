using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPractice.ConsoleAppRestClientExample
{
    public class RestClientExample
    {
        private readonly RestClient _client = new RestClient(new Uri("https://localhost:7060"));
        private readonly string _blogEndPoint = "api/blog";

        public async Task RunAsync()
        {
            //await EditAsync(1);
            //await EditAsync(1000);
            //await DeleteAsync(30);
            //await CreateAsync("New Title", "New Content", "New Author");
            //await UpdateAsync(31, "Update Title", "Update Content", "Update Author");
            await PatchAsync(31, "", "Patched Content", "");
            await ReadAsync();
        }

        private async Task ReadAsync()
        {
            RestRequest restRequest = new RestRequest(_blogEndPoint, Method.Get);
            var response = await _client.ExecuteAsync(restRequest);

            if(response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr)!;
                foreach(var blog in lst)
                {
                    Console.WriteLine("BlogId => "+ blog.BlogId);
                    Console.WriteLine("BlogTitle => " + blog.BlogTitle);
                    Console.WriteLine("BlogContent => " + blog.BlogContent);
                    Console.WriteLine("BlogAuthor => " + blog.BlogAuthor);
                    Console.WriteLine("------------------------------------------------");
                }
            }
        }

        private async Task EditAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}", Method.Get);
            var response = await _client.ExecuteAsync(restRequest);

            if(response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                var item = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;
                Console.WriteLine("BlogId => " + item.BlogId);
                Console.WriteLine("BlogTitle => " + item.BlogTitle);
                Console.WriteLine("BlogContent => " + item.BlogContent);
                Console.WriteLine("BlogAuthor => " + item.BlogAuthor);
            } else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }

        private async Task CreateAsync(string title, string content, string author)
        {
            BlogModel blog = new BlogModel()
            {
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };
            RestRequest request = new RestRequest(_blogEndPoint, Method.Post);
            request.AddJsonBody(blog);
            var response = await _client.ExecuteAsync(request);

            if(response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            } else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }

        private async Task UpdateAsync(int id, string title, string content, string author)
        {
            BlogModel blog = new BlogModel()
            {
                BlogId = id,
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };

            RestRequest request = new RestRequest($"{_blogEndPoint}/{id}", Method.Put);
            request.AddJsonBody(blog);
            var response = await _client.ExecuteAsync(request);
            if(response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message) ;
            } else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }

        private async Task PatchAsync(int id, string title, string content, string author)
        {
            BlogModel blog = new BlogModel()
            {
                BlogId = id,
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };

            RestRequest request = new RestRequest($"{_blogEndPoint}/{id}", Method.Patch);
            request.AddJsonBody(blog);
            var response = await _client.ExecuteAsync(request);
            if(response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message) ;
            } else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }

        private async Task DeleteAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}", Method.Delete);
            var response = await _client.ExecuteAsync(restRequest);

            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            } else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }
    }
}
