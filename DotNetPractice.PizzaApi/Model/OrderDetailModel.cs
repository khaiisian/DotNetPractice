using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPractice.PizzaApi.Model
{
    [Table("Tbl_OrderDetail")]
    public class OrderDetailModel
    {
        [Key]
        public int OrderDetail_Id { get; set; }
        public int Order_Id { get; set; }
        public int Extra_Id { get; set; }
    }
}
