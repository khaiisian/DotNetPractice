namespace DotNetPractice.RestApiWithNLayer.Models.PizzaModels
{
    public class OrderRequest
    {
        public int PizzaId { get; set; }
        public int[] Extra { get; set; }
    }
}
