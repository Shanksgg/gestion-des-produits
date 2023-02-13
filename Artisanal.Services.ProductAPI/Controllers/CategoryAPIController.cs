using Artisanal.Services.ProductAPI.Models.Dto;
using Artisanal.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Artisanal.Services.ProductAPI.Controllers
{
    [Route("api/categories")]
    public class CategoryAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private ICategoryRepository _categoryRepository;
        public CategoryAPIController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            this._response = new ResponseDto();
        }


        [HttpGet]
        public async Task<object> Get()
        {
          try
            {
                IEnumerable<CategoryDto> categoryDtos = await _categoryRepository.GetCategories();
                _response.Result = categoryDtos;
               
            }
                catch (Exception ex)
                 {
                   _response.IsSuccess = false;
                  _response.ErrorMessages
                      = new List<string>() { ex.ToString() };
                   }
            return _response;
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id) {
              try
               {
                    CategoryDto categoryDto = await _categoryRepository.GetCategoryById(id);
                    _response.Result = categoryDto;
                   
               }
                catch (Exception ex)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages
                         = new List<string>() { ex.ToString() };
                }
            return _response;
        }


        [HttpPost]
        //[Authorize]
        public async Task<object> Post([FromBody] CategoryDto categoryDto)
        {
            try
            {
                CategoryDto model = await _categoryRepository.CreateUpdateCategory(categoryDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                        = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpPut]
        //[Authorize]
        public async Task<object> Put([FromBody] CategoryDto categoryDto)
        {
            try
            {
                CategoryDto model = await _categoryRepository.CreateUpdateCategory(categoryDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                        = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await _categoryRepository.DeleteCategory(id);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                        = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        }
    }