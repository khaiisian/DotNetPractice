using Dapper;
using DotNetPractice.RestApi.Model;
using DotNetPractice.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DotNetPractice.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapper2Controller : ControllerBase
    {
        private readonly DapperService _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

        [HttpGet]
        public IActionResult Read()
        {
            string query = "select * from Blog_tbl";
            //List<BlogModel> lst = db.Query<BlogModel>(query).ToList();

            List <BlogModel> lst = _dapperService.QueryList<BlogModel>(query);
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound("No data found");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Blog_tbl]
           ([BlogTitle]
           ,[BlogContent]
           ,[BlogAuthor])
     VALUES
           (@BlogTitle
           ,@BlogContent
           ,@BlogAuthor)";

            //int result = db.Execute(query, blog);
            int result = _dapperService.Execute(query, blog);

            string message = result > 0 ? "Saving successful" : "Saving Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound("No data Found");
            }

            string query = @"UPDATE [dbo].[Blog_tbl]
            SET [BlogTitle] = @BlogTitle
            ,[BlogContent] = @BlogContent
            ,[BlogAuthor] = @BlogAuthor
            WHERE BlogId = @BlogId";

            blog.BlogId = id;
            //int result = db.Execute(query, blog);

            int result = _dapperService.Execute(query, blog);

            string message = result > 0 ? "Update Successful" : "Update Failed";

            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data found");
            }

            string condition = string.Empty;
            if(!string.IsNullOrEmpty(blog.BlogTitle))
            {
                condition += "[BlogTitle] = @BlogTitle, ";
            }
            if(!string.IsNullOrEmpty(blog.BlogContent))
            {
                condition += "[BlogContent] = @BlogContent, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                condition += "[BlogAuthor] = @BlogAuthor, ";
            }

            if(condition.Length == 0)
            {
                return NotFound("No data to update");
            }

            condition = condition.Substring(0, condition.Length - 2);
            blog.BlogId = id;

            string query = $@"UPDATE [dbo].[Blog_tbl]
            SET {condition}
            WHERE BlogId = @BlogId";
            //int result = db.Execute(query, blog);

            int result = _dapperService.Execute(query, blog);
            string message = result > 0 ? "Patch Successful" : "Patch Failed";

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound("No data found");
            }
            string query = @"DELETE FROM [dbo].[Blog_tbl]
      WHERE BlogId = @BlogId";

            //int result = db.Execute(query, new BlogModel { BlogId=id});

            int result = _dapperService.Execute(query, new BlogModel { BlogId = id });  
            string message = result > 0 ? "Deleting successful" : "Deleting Failed";
            return Ok(message);
        }


        private BlogModel? FindById(int id)
        {
            string query = "Select * FROM Blog_tbl WHERE BlogId = @BlogId";
            //var item = db.Query<BlogModel>(query, new BlogModel { BlogId=id }).FirstOrDefault();
            
            var item = _dapperService.QueryFirstOrDefault<BlogModel>(query, new BlogModel { BlogId = id});
            return item;
        }
    }
}
