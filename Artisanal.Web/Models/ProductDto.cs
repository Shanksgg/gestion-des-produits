using Microsoft.AspNetCore.Mvc.Rendering;

namespace Artisanal.Web.Models
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int CategoryId{ get; set; }
        public CategoryDto Category{ get; set; }
        public string ImageURL { get; set; }
    }
}