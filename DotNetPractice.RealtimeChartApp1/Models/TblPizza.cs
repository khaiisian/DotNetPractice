using System;
using System.Collections.Generic;

namespace DotNetPractice.RealtimeChartApp1.Models;

public partial class TblPizza
{
    public int PizzaId { get; set; }

    public string PizzaName { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<TblPizzaOrder> TblPizzaOrders { get; set; } = new List<TblPizzaOrder>();
}
