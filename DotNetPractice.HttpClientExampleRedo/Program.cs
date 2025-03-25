// See https://aka.ms/new-console-template for more information


using DotNetPractice.HttpClientExampleRedo;

Console.WriteLine("Hello, World!");
HttpClientExampleRedo httpClientExampleRedo = new HttpClientExampleRedo();
await httpClientExampleRedo.RunAsync();






//HttpClient client = new HttpClient();
//var response = await client.GetAsync("https://localhost:7060/api/blog");

//if (response.IsSuccessStatusCode)
//{
//    string jsonStr = await response.Content.ReadAsStringAsync();
//    Console.WriteLine(jsonStr);

//    List<BlogDto> lst = JsonConvert.DeserializeObject<List<BlogDto>>(jsonStr)!;
//    foreach (var blog in lst)
//    {
//        Console.WriteLine("BlogId => " + blog.BlogId);
//        Console.WriteLine("BlogTitle => " + blog.BlogTitle);
//        Console.WriteLine("BlogContent => " + blog.BlogContent);
//        Console.WriteLine("BlogAuthor => " + blog.BlogAuthor);
//    }
//}


//public class BlogDto
//{
//    public int BlogId { get; set; }
//    public string? BlogTitle { get; set; }
//    public string? BlogContent { get; set; }
//    public string? BlogAuthor { get; set; }
//}