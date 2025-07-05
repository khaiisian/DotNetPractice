using System;
using System.Collections.Generic;

namespace DotNetPractice.RealtimeChartApp1.Models;

public partial class TblExtra
{
    public int ExtraId { get; set; }

    public string ExtraName { get; set; } = null!;

    public decimal Price { get; set; }
}
