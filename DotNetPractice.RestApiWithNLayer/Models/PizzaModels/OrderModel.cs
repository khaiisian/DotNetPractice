using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPractice.RestApiWithNLayer.Models.PizzaModels
{
    [Table("Tbl_PizzaOrder")]
    public class OrderModel
    {
        [Key]
        public int Order_Id { get; set; }
        public int Invoice_Num { get; set; }
        public int Pizza_Id { get; set; }
        public decimal Total_Amount { get; set; }
    }
}
