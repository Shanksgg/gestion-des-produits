using Artisanal.Web.Models;
using Artisanal.Web.Services.IServices;
using Newtonsoft.Json.Linq;

namespace Artisanal.Web.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory _httpClient;
        public ProductService(IHttpClientFactory httpClient) :base(httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> CreateProductAsync<T>(ProductDto productDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data= productDto,   
                Url = SD.ProductAPIBase + "/api/products",
        });
        }

        public async Task<T> DeleteProductAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "/api/products/"+id,
                //AccessToken = token

            });
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
           return await this.SendAsync<T>(new ApiRequest()
            {
                 ApiType=SD.ApiType.GET,
                 Url=SD.ProductAPIBase+"/api/products",
                // AccessToken=token
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/products/"+id,
               // AccessToken = token

            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto productDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data= productDto,
                Url = SD.ProductAPIBase + "/api/products",
               // AccessToken = token

            });
        }
    }
}