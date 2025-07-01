using System;
using System.Collections.Generic;

namespace DotNetPractice.RealtimeChartApp.Models;

public partial class StudentTbl
{
    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public int StudentAge { get; set; }

    public decimal AvgScore { get; set; }
}
