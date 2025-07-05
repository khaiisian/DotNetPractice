using System;
using System.Collections.Generic;

namespace DotNetPractice.RealtimeChartApp1.Models;

public partial class StudentTable
{
    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public string StudentAddress { get; set; } = null!;

    public string StudentMail { get; set; } = null!;

    public int StudentAge { get; set; }
}
