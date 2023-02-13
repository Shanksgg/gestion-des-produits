using Artisanal.Web.Models;
using Artisanal.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Artisanal.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> CategoryIndex()
        {
            List<CategoryDto> list = new();
            var response = await _categoryService.GetAllCategoriesAsync<ResponseDto>();
            if(response != null) { 
                list = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(response.Result));
                Console.WriteLine(list);
            }
            
            return View(list);
        }

        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryCreate(CategoryDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _categoryService.CreateCategoryAsync<ResponseDto>(model);

                if(response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CategoryIndex));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> CategoryEdit(int categoryId)
        {
            if (ModelState.IsValid)
            {
                var response = await _categoryService.GetCategoryByIdAsync<ResponseDto>(categoryId);

                if (response != null && response.IsSuccess)
                {
                    CategoryDto model = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(response.Result));
                    return View(model);
                }
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryEdit(CategoryDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _categoryService.UpdateCategoryAsync<ResponseDto>(model);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CategoryIndex));
                }
            }

            return NotFound(model);
        }

        public async Task<IActionResult> CategoryDelete(int categoryId)
        {
            if (ModelState.IsValid)
            {
                var response = await _categoryService.GetCategoryByIdAsync<ResponseDto>(categoryId);

                if (response != null && response.IsSuccess)
                {
                    CategoryDto model = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(response.Result));
                    return View(model);
                }
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryDelete(CategoryDto model)
        {
            System.Diagnostics.Debug.WriteLine("HERE");
            if (ModelState.IsValid)
            {
                var response = await _categoryService.DeleteCategoryAsync<ResponseDto>(model.CategoryId);
                System.Diagnostics.Debug.WriteLine("V");
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CategoryIndex));
                }
            }

            return NotFound(model);
        }
    }
}