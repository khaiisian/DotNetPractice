namespace DotNetPractice.PizzaApi.Model
{
    public class OrderRequest
    {
        public int Pizza_Id { get; set; }
        public int[]? Extras_Id { get; set; }
    }
}
