﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPractice.MVCApp.Models
{
    [Table("Blog_tbl")]
    public class BlogModel
    {
        [Key]
        public int BlogId { get; set; }
        public string ? BlogTitle { get; set; }
        public string ? BlogContent { get; set; }
        public string ? BlogAuthor { get; set; }
    }
}
