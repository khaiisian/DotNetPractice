using System;
using System.Collections.Generic;

namespace DotNetPractice.RealtimeChartApp1.Models;

public partial class TblPizzaDetailOrder
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int ExtraId { get; set; }
}
