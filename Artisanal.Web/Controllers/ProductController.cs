using Artisanal.Web.Models;
using Artisanal.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Artisanal.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> ProductIndex(string name)
        {
            List<ProductDto> list = new();
            var response = await _productService.GetAllProductsAsync<ResponseDto>();

            if (response != null) { 
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));

                foreach (var item in list)
                {
                    var tmpResponse = await _categoryService.GetCategoryByIdAsync<ResponseDto>(item.CategoryId);
                    if (tmpResponse != null && tmpResponse.IsSuccess)
                    {
                        CategoryDto model2 = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(tmpResponse.Result));
                        item.Category = model2;
                    }
                }
                return View(list);
            }
            return View();
        }

        public async Task<IActionResult> ProductCreate()
        {
            List<CategoryDto> list = new();
            var model = new ProductCategoryViewModel();
            model.CategoriesSelectList = new List<SelectListItem>();

            var response = await _categoryService.GetAllCategoriesAsync<ResponseDto>();

            if (response != null)
            {
                list = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(response.Result));
                foreach (var country in list)
                {
                    model.CategoriesSelectList.
                        Add(new SelectListItem { Text = country.CategoryName, Value = country.CategoryId.ToString() });
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Product.CategoryId = model.selectedCountry;
                var response = await _productService.CreateProductAsync<ResponseDto>(model.Product);

                if(response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }

            return NotFound();
        }

        public async Task<IActionResult> ProductEdit(int productId)
        {
            if (ModelState.IsValid)
            {
                var response1 = await _productService.GetProductByIdAsync<ResponseDto>(productId);

                if (response1 != null && response1.IsSuccess)
                {
                    ProductCategoryViewModel model1 = new ProductCategoryViewModel();
                    model1.Product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response1.Result));

                    var response2 = await _categoryService.GetCategoryByIdAsync<ResponseDto>(model1.Product.CategoryId);

                    if (response2 != null && response2.IsSuccess)
                    {
                        model1.Category = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(response2.Result));
                        var response3 = await _categoryService.GetAllCategoriesAsync<ResponseDto>();
                        
                        if( response3 != null && response3.IsSuccess)
                        {
                            List<CategoryDto> list = new();
                            list = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(response3.Result));
                            model1.CategoriesSelectList = new List<SelectListItem>();

                            foreach (var country in list)
                            {
                                model1.CategoriesSelectList.
                                    Add(new SelectListItem { Text = country.CategoryName, Value = country.CategoryId.ToString() });
                            }
                        }

                        return View(model1);
                    }
                }
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Product.CategoryId = model.selectedCountry;
                var response = await _productService.UpdateProductAsync<ResponseDto>(model.Product);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }

            return NotFound(model);
        }

        public async Task<IActionResult> ProductDelete(int productId)
        {
            if (ModelState.IsValid)
            {
                var response1 = await _productService.GetProductByIdAsync<ResponseDto>(productId);

                if (response1 != null && response1.IsSuccess)
                {
                    ProductDto model1 = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response1.Result));
                    var response2 = await _categoryService.GetCategoryByIdAsync<ResponseDto>(model1.CategoryId);

                    if (response2 != null && response2.IsSuccess)
                    {
                        CategoryDto model2 = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(response2.Result));
                        model1.Category = model2;
                        return View(model1);
                    }
                }
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.DeleteProductAsync<ResponseDto>(model.ProductId);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }

            return NotFound(model);
        }
    }
}