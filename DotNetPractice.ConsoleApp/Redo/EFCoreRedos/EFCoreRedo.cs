using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPractice.ConsoleApp.Redo
{
    internal class EFCoreRedo
    {
        public void Run()
        {
            //Edit(1);
            //Edit(1000);
            //Create("new title", "new content", "new author");
            //Update(12, "updated title", "updated content", "updated author");
            //Delete(12);

            Read();
        }

        private readonly NewAppDbContext db = new NewAppDbContext();

        public void Read()
        {
            var lst = db.Blogs.ToList();

            foreach (NewBlogDto blog in lst)
            {
                Console.WriteLine("BlogID => "+ blog.BlogId);
                Console.WriteLine("BlogTitle => "+ blog.BlogTitle);
                Console.WriteLine("BlogContent => " + blog.BlogContent);
                Console.WriteLine("BlogContent => " + blog.BlogContent);
                Console.WriteLine("---------------------------------------------------");
            }
        }

        public void Edit(int id)
        {
            var item = db.Blogs.FirstOrDefault(x=>x.BlogId==id);
            if (item is null)
            {
                Console.WriteLine("No data is found");
                return;
            }


            Console.WriteLine("BlogID => " + item.BlogId);
            Console.WriteLine("BlogTitle => " + item.BlogTitle);
            Console.WriteLine("BlogContent => " + item.BlogContent);
            Console.WriteLine("BlogContent => " + item.BlogContent);
            Console.WriteLine("---------------------------------------------------");
        }

        public void Create(string title, string content, string author)
        {
            var item = new NewBlogDto
            {
                BlogTitle = title,
                BlogContent = content,
                BlogAuthor = author
            };

            db.Blogs.Add(item);
            int result = db.SaveChanges();

            string message = result > 0 ? "Saving successful" : "Saving Failed";
            Console.WriteLine(message);
        }

        public void Update(int id, string title, string content, string author)
        {
            var item = db.Blogs.FirstOrDefault(x=>x.BlogId== id);

            if(item is null)
            {
                Console.WriteLine("no data found.");
                return;
            }

            item.BlogTitle = title;
            item.BlogContent = content;
            item.BlogAuthor = author;

            int result = db.SaveChanges();
            string message = result > 0 ? "Saving successful" : "Saving failed";
            Console.WriteLine(message);
        }

        public void Delete(int id)
        {
            var item = db.Blogs.FirstOrDefault(x=>x.BlogId == id);
            if(item is null)
            {
                Console.WriteLine("no data found");
                return;
            }

            db.Blogs.Remove(item);
            int result = db.SaveChanges();

            string message = result > 0 ? "Delete successful" : "Delete failed";
            Console.WriteLine(message);
        }
    }
}
