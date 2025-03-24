using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DotNetPractice.HttpClientExample
{
    internal class HttpClientExample
    {
        private readonly HttpClient _httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7060") };
        private readonly string _blogEndPoint = "api/blog";
        public async Task RunAsync()
        {
            //await ReadAsync();
            //await EditAsync(1);
            //await EditAsync(200);
            //await DeleteAsync(27);
            //await DeleteAsync(87);
            //await CreateAsync("new Title", "new Content", "new Author");
            await PatchAsync(29, "Patch Title", "", "");
            await EditAsync(29);
        }

        private async Task ReadAsync()
        {
            var response = await _httpClient.GetAsync(_blogEndPoint);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();

                List<BlogDto> lst = JsonConvert.DeserializeObject<List<BlogDto>>(jsonStr)!;
                foreach (var item in lst)
                {
                    Console.WriteLine("BlogId => " + item.BlogId);
                    Console.WriteLine("BlogTitle => " + item.BlogTitle);
                    Console.WriteLine("BlogAuthor => " + item.BlogAuthor);
                    Console.WriteLine("BlogContent => " + item.BlogContent);
                    Console.WriteLine("-----------------------------------------------------------------");
                }
            }
        }

        private async Task EditAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_blogEndPoint}/{id}");
            if(response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<BlogDto>(jsonStr)!;

                Console.WriteLine($"BlogId => {item.BlogId}");
                Console.WriteLine($"BlogTitle => {item.BlogTitle}");
                Console.WriteLine($"BlogContent => {item.BlogContent}");
                Console.WriteLine($"BlogAuthor => {item.BlogAuthor}");
            } else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task CreateAsync(string title, string content, string author)
        {
            BlogDto blog = new BlogDto()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string jsonContent = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, Application.Json);

            var response = await _httpClient.PostAsync(_blogEndPoint, httpContent);
            if(response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            } else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        public async Task UpdateAsync(int id, string title, string content, string author)
        {
            BlogDto blog = new BlogDto()
            {
                BlogId = id,
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };
            string jsonContent = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, Application.Json);

            var response = await _httpClient.PutAsync($"{_blogEndPoint}/{id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message) ;
            } else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        public async Task PatchAsync(int id, string title, string content, string author)
        {
            BlogDto blog = new BlogDto()
            {
                BlogId= id,
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };
            string jsonContent = JsonConvert.SerializeObject(blog);
            HttpContent _httpcontent = new StringContent(jsonContent, Encoding.UTF8, Application.Json);

            var response = await _httpClient.PatchAsync($"{_blogEndPoint}/{id}", _httpcontent);
            if(response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            } else
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
            } else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }

            // a good practice to write if condition to check success or fail status even both condition retrieve the same type of data
            // since in some scenario, different steps and data wil be retrieved in success and fail condition
        }
    }
}
