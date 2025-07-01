using System;
using System.Collections.Generic;

namespace DotNetPractice.RealtimeChartApp.Models;

public partial class TblPizzaOrder
{
    public int OrderId { get; set; }

    public string InvoiceNum { get; set; } = null!;

    public int PizzaId { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual TblPizza Pizza { get; set; } = null!;
}
