namespace DotNetPractice.RestApiWithNLayer.Models.PizzaModels
{
    public class OrderResponse
    {
        public string Message { get; set; }
        public string Order_Invoice { get; set; }
        public string Pizza { get; set; }
        public string[]? Extra {  get; set; }
        //public decimal Total_Amount { get; set; }
        public string? Total_AmountString { get; set; }
    }
}
