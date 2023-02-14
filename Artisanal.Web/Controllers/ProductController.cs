using Artisanal.Web.Models;
using Artisanal.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

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

        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> list = new();
            var response = await _productService.GetAllProductsAsync<ResponseDto>();
            if(response != null) { 
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));

                foreach (var item in list)
                {
                    var tmpResponse = await _categoryService.GetCategoryByIdAsync<ResponseDto>(1);
                    System.Diagnostics.Debug.WriteLine("=============================>" + item.CategoryId + item.ImageURL);
                    if (tmpResponse != null && tmpResponse.IsSuccess)
                    {
                        CategoryDto model2 = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(tmpResponse.Result));
                        item.Category = model2;
                    }
                }
            }
            
            return View(list);
        }

        public IActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync<ResponseDto>(model);

                if(response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> ProductEdit(int productId)
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
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductAsync<ResponseDto>(model);

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
                var response = await _productService.GetProductByIdAsync<ResponseDto>(productId);

                if (response != null && response.IsSuccess)
                {
                    ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                    return View(model);
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