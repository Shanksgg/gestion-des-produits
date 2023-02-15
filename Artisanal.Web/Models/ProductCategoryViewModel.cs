using Microsoft.AspNetCore.Mvc.Rendering;

namespace Artisanal.Web.Models
{
    public class ProductCategoryViewModel
    {
        public ProductDto Product { get; set; }
        public CategoryDto Category { get; set; }
        public int selectedCountry { get; set; }
        public List<SelectListItem> CategoriesSelectList { get; set; }
    }
}
