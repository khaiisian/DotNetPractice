using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPractice.RestClientConsoleAppExampleRedo
{
    public class RestClientExample
    {
        private readonly RestClient _client = new RestClient(new Uri("https://localhost:7060"));
        private readonly string _blogEndPoint = "api/blog";
        public async Task RunAsync()
        {
            //await EditAsync(1);
            //await EditAsync(100);
            //await CreateAsync("new title", "new content", "new author");
            //await UpdateAsync(32, "update title", "update content", "update content");
            //await PatchAsync(32, "", "patch content", "");
            await DeleteAsync(25);
            await ReadAsync();
        }

        private async Task ReadAsync()
        {
            RestRequest restRequest = new RestRequest(_blogEndPoint, Method.Get);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr)!;
                foreach (var item in lst)
                {
                    Console.WriteLine($"BlogId => {item.BlogId}");
                    Console.WriteLine($"BlogTitle => {item.BlogTitle}");
                    Console.WriteLine($"BlogContent => {item.BlogContent}");
                    Console.WriteLine($"BlogAuthor => {item.BlogAuthor}");
                    Console.WriteLine("--------------------------------------------");
                }
            }
        }

        private async Task EditAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}", Method.Get);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                BlogModel item = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;
                Console.WriteLine($"BlogId => {item.BlogId}");
                Console.WriteLine($"BlogTitle => {item.BlogTitle}");
                Console.WriteLine($"BlogContent => {item.BlogContent}");
                Console.WriteLine($"BlogAuthor => {item.BlogAuthor}");
                Console.WriteLine("--------------------------------------------");
            } else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }

        private async Task CreateAsync(string title, string content, string author)
        {
            BlogModel requestModel = new BlogModel()
            {
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };

            string jsonContent = JsonConvert.SerializeObject(requestModel);
            RestRequest restRequest = new RestRequest(_blogEndPoint, Method.Post);
            restRequest.AddJsonBody(jsonContent);
            var respsone = await _client.ExecuteAsync(restRequest);
            if(respsone.IsSuccessStatusCode)
            {
                string message = respsone.Content!;
                Console.WriteLine(message);
            } else
            {
                string message = respsone.Content!;
                Console.WriteLine(message);
            }
        }

        private async Task UpdateAsync(int id, string title, string content, string author)
        {
            BlogModel requestModel = new BlogModel()
            {
                BlogId = id,
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };
            string jsonContent = JsonConvert.SerializeObject(requestModel);
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}", Method.Put);
            restRequest.AddJsonBody(jsonContent);
            var response = await _client.ExecuteAsync(restRequest);
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

        private async Task PatchAsync(int id, string title, string content, string author)
        {
            var requestModel = new BlogModel()
            {
                BlogId = id,
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };
            string jsonContent = JsonConvert.SerializeObject(requestModel);
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}", Method.Patch);
            restRequest.AddJsonBody(jsonContent);
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

        private async Task DeleteAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}", Method.Delete);
            var response = await _client.ExecuteAsync(restRequest);
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
    }
}
