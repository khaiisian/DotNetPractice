using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPractice.PizzaApiWithMultiplePizzas.Model
{
    [Table("Tbl_Extras")]
    public class ExtraModel
    {
        [Key]
        public int ExtraId { get; set; }
        public string ExtraName { get; set; }
        public decimal Price { get; set; }
    }
}
