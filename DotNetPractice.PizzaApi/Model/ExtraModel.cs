using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPractice.PizzaApi.Model
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
