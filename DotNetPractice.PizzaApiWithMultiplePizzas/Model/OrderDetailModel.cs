using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPractice.PizzaApiWithMultiplePizzas.Model
{
    [Table("Tbl_OrderDetails")]
    public class OrderDetailModel
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int OrderItemId { get; set; }
        public int ExtraId { get; set; }
    }
}
