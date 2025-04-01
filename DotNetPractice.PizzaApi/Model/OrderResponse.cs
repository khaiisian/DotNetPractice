namespace DotNetPractice.PizzaApi.Model
{
    public class OrderResponse
    {
        public string Invoice_Number { get; set; }
        public string Pizza_Name { get; set; }
        public string[] Extras { get; set; }
        public string Total_Amount { get; set; }
    }
}
