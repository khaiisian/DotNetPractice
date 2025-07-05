using System;
using System.Collections.Generic;

namespace DotNetPractice.RealtimeChartApp1.Models;

public partial class BlogTbl
{
    public int BlogId { get; set; }

    public string BlogTitle { get; set; } = null!;

    public string BlogContent { get; set; } = null!;

    public string BlogAuthor { get; set; } = null!;
}
