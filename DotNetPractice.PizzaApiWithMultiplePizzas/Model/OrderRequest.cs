namespace DotNetPractice.PizzaApiWithMultiplePizzas.Model
{
    public class OrderItem 
    { 
        public int PizzaId { get; set; }
        public int[]? Extras { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderRequest
    {
        public List<OrderItem> OrderItems { get; set;}
    }
}
