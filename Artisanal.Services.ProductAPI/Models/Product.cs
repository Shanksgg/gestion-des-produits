


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artisanal.Services.ProductAPI.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required, StringLength(28)]
        public string ProductName { get; set; }
        public double Price { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required, StringLength(32)]
        public string ImageURL { get; set; }
    }
}
