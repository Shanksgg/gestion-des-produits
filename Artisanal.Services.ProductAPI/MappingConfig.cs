using Artisanal.Services.ProductAPI.Models;
using Artisanal.Services.ProductAPI.Models.Dto;
using AutoMapper;

namespace Artisanal.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
          var mappingConfig = new MapperConfiguration(config =>

            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<CategoryDto, Category>();
                config.CreateMap<Product, ProductDto>();
                config.CreateMap<Category, CategoryDto>();
            });

            return mappingConfig;
        }
    }
}