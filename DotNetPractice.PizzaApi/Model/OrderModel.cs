using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPractice.PizzaApi.Model
{
    [Table("Tbl_Order")]
    public class OrderModel
    {
        [Key]
        public int Order_Id { get; set; }
        public string Invoice_Num { get; set; }
        public int Pizza_Id { get; set; }
        public decimal Total_Amount { get; set; }
    }
}
