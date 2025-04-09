using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPractice.PizzaApiWithMultiplePizzas.Model
{
    [Table("Tbl_OrderItems")]
    public class OrderItemModel
    {
        [Key]
        public int OrderItemId { get; set; }
        public int PizzaId { get; set; }
        public int Qauntity { get; set; }
    }
}
