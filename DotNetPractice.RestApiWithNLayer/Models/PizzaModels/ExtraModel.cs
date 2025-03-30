using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPractice.RestApiWithNLayer.Models.PizzaModels
{
    [Table("Tbl_Extra")]
    public class ExtraModel
    {
        [Key]
        public int Extra_Id { get; set; }
        public string Extra_Name { get; set; }
        public decimal Price { get; set; }
    }
}
