namespace DotNetPractice.PizzaApiWithMultiplePizzas.Model
{
    public class OrderResponse
    {
        public string InvoiceNum { get; set; }
        public List<OrderResItem> OrderResItems { get; set; }
        public string TotalAmount { get; set; }
    }

    public class OrderResItem
    {
        public string PizzaName { get; set; }
        public string PizzaAmount { get; set; }
        public List<OrderResExtra> OrderResExtra { get; set; }
    }

    public class OrderResExtra
    {
        public string ExtraName { get; set; }
        public string ExtraAmount { get; set; }
    }
}
