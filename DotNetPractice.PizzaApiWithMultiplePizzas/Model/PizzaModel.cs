using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPractice.PizzaApiWithMultiplePizzas.Model
{
    [Table("Tbl_Pizzas")]
    public class PizzaModel
    {
        [Key]
        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
        public decimal Price { get; set; }
    }
}
