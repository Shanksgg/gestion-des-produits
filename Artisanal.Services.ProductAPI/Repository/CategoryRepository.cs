using Artisanal.Services.ProductAPI.DbContexts;
using Artisanal.Services.ProductAPI.Models;
using Artisanal.Services.ProductAPI.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Artisanal.Services.ProductAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        private IMapper _mapper;

        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CategoryDto> CreateUpdateCategory(CategoryDto categoryDto)
        {
            Category category = _mapper.Map<CategoryDto, Category>(categoryDto);
            if (category.CategoryId > 0)
            {
                _db.Categories.Update(category);
            }
            else
            {
                _db.Categories.Add(category);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Category, CategoryDto>(category);
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            try
            {
                Category category = await _db.Categories.FirstOrDefaultAsync(u => u.CategoryId == categoryId);
                if (category == null)
                {
                    return false;
                }
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<CategoryDto> GetCategoryById(int categoryId)
        {
            Category category= await _db.Categories.Where(x => x.CategoryId == categoryId).FirstOrDefaultAsync();
            
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            List<Category> categoryList = await _db.Categories.ToListAsync();

            return _mapper.Map<List<CategoryDto>>(categoryList);

        }
    }
}