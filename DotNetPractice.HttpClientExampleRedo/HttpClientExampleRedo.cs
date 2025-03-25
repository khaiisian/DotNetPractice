using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DotNetPractice.HttpClientExampleRedo
{
    internal class HttpClientExampleRedo
    {
        private readonly HttpClient _httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7060") };
        private readonly string _blogEndPoint = "api/blog";

        public async Task RunAsync()
        {
            //await ReadAsync();
            //await EditAsync(1);
            //await EditAsync(200);
            //await DeleteAsync(29);

            await PatchAsync(30, "", "", "Patch Content");
            await ReadAsync();
        }

        private async Task ReadAsync()
        {
            var response = await _httpClient.GetAsync(_blogEndPoint);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(jsonStr);

                List<BlogDto> lst = JsonConvert.DeserializeObject<List<BlogDto>>(jsonStr)!;
                foreach (var blog in lst)
                {
                    Console.WriteLine("BlogId => " + blog.BlogId);
                    Console.WriteLine("BlogTitle => " + blog.BlogTitle);
                    Console.WriteLine("BlogContent => " + blog.BlogContent);
                    Console.WriteLine("BlogAuthor => " + blog.BlogAuthor);
                }
            }
        }

        private async Task EditAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_blogEndPoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<BlogDto>(jsonStr)!;
                Console.WriteLine("BlogId => " + item.BlogId);
                Console.WriteLine("BlogTitle => " + item.BlogTitle);
                Console.WriteLine("BlogContent => " + item.BlogContent);
                Console.WriteLine("BlogAuthor => " + item.BlogAuthor);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task CreateAsync(string title, string content, string author)
        {
            BlogDto blogDto = new BlogDto()
            {
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };
            string jsonContent = JsonConvert.SerializeObject(blogDto);
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, Application.Json);

            var response = await _httpClient.PostAsync(_blogEndPoint, httpContent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task UpdateAsync(int id, string title, string content, string author)
        {
            BlogDto blogDto = new BlogDto()
            {
                BlogId = id,
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };
            string jsonContent = JsonConvert.SerializeObject(blogDto);
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, Application.Json);
            var response = await _httpClient.PutAsync($"{_blogEndPoint}/{id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task PatchAsync(int id, string title, string content, string author)
        {
            BlogDto blogDto = new BlogDto()
            {
                BlogId = id,
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };
            string jsonContent = JsonConvert.SerializeObject(blogDto) ;
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, Application.Json);
            var response = await _httpClient.PatchAsync($"{_blogEndPoint}/{id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_blogEndPoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }
    }
}
