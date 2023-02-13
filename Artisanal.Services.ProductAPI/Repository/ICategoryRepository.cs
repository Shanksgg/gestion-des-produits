
using Artisanal.Services.ProductAPI.Models.Dto;

namespace Artisanal.Services.ProductAPI.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDto>> GetCategories();
        Task<CategoryDto> GetCategoryById(int categoryId);
        Task<CategoryDto> CreateUpdateCategory(CategoryDto categoryDto);
        Task<bool> DeleteCategory(int categoryId);
    }
}