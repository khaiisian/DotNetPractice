using DotNetPracticeRefitExample;
using Refit;

//var refitService = RestService.For<iBlogApi>("https://localhost:7184");
//var lst = await refitService.GetBlogs();
//foreach (var item in lst)
//{
//    Console.WriteLine($"BlogTitle => {item.BlogId}");
//    Console.WriteLine($"BlogTitle => {item.BlogTitle}");
//    Console.WriteLine($"BlogTitle => {item.BlogContent}");
//    Console.WriteLine($"BlogTitle => {item.BlogAuthor}");
//    Console.WriteLine("===================================================");
//}

RefitExample refitExample = new RefitExample();
await refitExample.RunAsync();

Console.WriteLine("end.");
Console.ReadKey();