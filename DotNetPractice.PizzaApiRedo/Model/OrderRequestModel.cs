namespace DotNetPractice.PizzaApiRedo.Model
{
    public class OrderRequestModel
    {
        public List<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public int Pizza_Id { get; set; }
        public int[]? Extra_Ids { get; set; }
        public int Quantity { get; set; }
    }
}
