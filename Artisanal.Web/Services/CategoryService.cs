using Artisanal.Web.Models;
using Artisanal.Web.Services.IServices;
using Newtonsoft.Json.Linq;

namespace Artisanal.Web.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IHttpClientFactory _httpClient;
        public CategoryService(IHttpClientFactory httpClient):base(httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> CreateCategoryAsync<T>(CategoryDto categoryDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data= categoryDto,   
                Url = SD.ProductAPIBase + "/api/categories",
        });
        }

        public async Task<T> DeleteCategoryAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "/api/categories/" + id,
                //AccessToken = token
            });
        }

        public async Task<T> GetAllCategoriesAsync<T>()
        {
           return await this.SendAsync<T>(new ApiRequest()
            {
                 ApiType=SD.ApiType.GET,
                 Url=SD.ProductAPIBase+ "/api/categories",
                // AccessToken=token
            });
        }

        public async Task<T> GetCategoryByIdAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/categories/" + id,
               // AccessToken = token
            });
        }

        public async Task<T> UpdateCategoryAsync<T>(CategoryDto categoryDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data= categoryDto,
                Url = SD.ProductAPIBase + "/api/categories",
               // AccessToken = token
            });
        }
    }
}