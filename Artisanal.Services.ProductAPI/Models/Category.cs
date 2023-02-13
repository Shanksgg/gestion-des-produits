using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artisanal.Services.ProductAPI.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required, StringLength(28)]
        public string CategoryName { get; set; }
    }
}
