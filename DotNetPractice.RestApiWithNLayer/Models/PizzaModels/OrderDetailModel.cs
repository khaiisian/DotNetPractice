using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPractice.RestApiWithNLayer.Models.PizzaModels
{
    [Table("Tbl_PizzaDetailOrder")]
    public class OrderDetailModel
    {
        [Key]
        public int OrderDetail_Id { get; set; }
        public int Order_Id { get; set; }
        public int Extra_Id { get; set; }
    }
}
