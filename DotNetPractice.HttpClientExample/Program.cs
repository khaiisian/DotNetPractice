using DotNetPractice.HttpClientExample;
using Newtonsoft.Json;

//Console.WriteLine("Hello, World!");

//HttpClient httpClient = new HttpClient();
//var response = await httpClient.GetAsync("https://localhost:7060/api/blog");

//if (response.IsSuccessStatusCode)
//{
//    //data from httprequest are json
//    string jsonString = await response.Content.ReadAsStringAsync();
//    //Console.WriteLine(jsonString);

//    //retrieve as C# objects
//    List<BlogDto> lst = JsonConvert.DeserializeObject<List<BlogDto>>(jsonString)!;
//    foreach (var blog in lst)
//    {
//        Console.WriteLine("BlogId => "+ blog.BlogId);
//        Console.WriteLine("BlogTitle => " + blog.BlogTitle);
//        Console.WriteLine("BlogContent => " + blog.BlogContent);
//        Console.WriteLine("BlogAuthor => " + blog.BlogAuthor);
//    }
//}



//public class BlogDto
//{
//    public int? BlogId { get; set; }
//    public string? BlogTitle { get; set; }
//    public string? BlogContent { get; set; }
//    public string? BlogAuthor { get; set; }
//}

HttpClientExample httpClientExample = new HttpClientExample();
await httpClientExample.RunAsync();

