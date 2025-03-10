using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DotNetPractice.ConsoleApp
{
    internal class EFCoreExample
    {
        public void Run()
        {
            //Edit(1);
            //Edit(100);
            //Create("new title", "new content", "new author");
            //Update(11,"update title", "update content", "update author");

            //Delete(11);
            Read();
        }

        private readonly AppDbContext dbContext = new AppDbContext();

        private void Read()
        {
            var lst = dbContext.Blogs.ToList();

            foreach(BlogDto blog in lst)
            {
                Console.WriteLine("BlogId => "+ blog.BlogId);
                Console.WriteLine("BlogTitle => "+ blog.BlogTitle);
                Console.WriteLine("BlogContent => "+ blog.BlogContent);
                Console.WriteLine("BlogAuthor => "+ blog.BlogAuthor);
                Console.WriteLine("---------------------------------------------------------");
            }
        }

        private void Edit(int id)
        {
            var item = dbContext.Blogs.FirstOrDefault(x=>x.BlogId==id);
            if (item is null)
            {
                Console.WriteLine("Item is not found");
                return;
            }

            Console.WriteLine("BlogId => "+  item.BlogId);
            Console.WriteLine("BlogTitle => " +  item.BlogTitle);
            Console.WriteLine("BlogContent => " +  item.BlogContent);
            Console.WriteLine("BlogAuthor => " +  item.BlogAuthor);
        }

        private void Create(string title, string content, string author)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };

            dbContext.Blogs.Add(item);
            int result = dbContext.SaveChanges();

            string message = result > 0 ? "saving successful" : "saving failed";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string content, string author)
        {
            var item = dbContext.Blogs.FirstOrDefault(x=>x.BlogId==id);
            if(item is null)
            {
                Console.WriteLine("no data found");
                return;
            }

            item.BlogTitle = title;
            item.BlogContent = content;
            item.BlogAuthor = author;

            int result = dbContext.SaveChanges();
            string message = result > 0 ? "Update successful" : "update failed";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            var item = dbContext.Blogs.FirstOrDefault(x=> x.BlogId==id);
            if(item is null)
            {
                Console.WriteLine("no data is found");
                return;
            }

            dbContext.Blogs.Remove(item);
            int result = dbContext.SaveChanges();

            string message = result > 0 ? "Delete successful" : "Delete Failed";
            Console.WriteLine(message);
        }
    }
}
