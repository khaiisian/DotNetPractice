using System;
using System.Collections.Generic;

namespace DotNetPractice.RealtimeChartApp.Models;

public partial class TblPieChartt
{
    public int PieChartId { get; set; }

    public string PieChartName { get; set; } = null!;

    public decimal PieChartValue { get; set; }
}
