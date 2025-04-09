using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPractice.PizzaApiWithMultiplePizzas.Model
{
    [Table("Tbl_Order")]
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }
        public string InvoiceNum { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
