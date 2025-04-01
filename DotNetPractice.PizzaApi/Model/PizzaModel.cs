using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPractice.PizzaApi.Model
{
    [Table("Tbl_Pizza")]
    public class PizzaModel
    {
        [Key]
        public int Pizza_Id { get; set; }
        public string Pizza_Name { get; set; }
        public decimal Price { get; set; }
    }
}
