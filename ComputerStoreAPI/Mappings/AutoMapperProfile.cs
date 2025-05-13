using AutoMapper;
using ComputerStoreAPI.DTOs;
using ComputerStoreAPI.Entities;

namespace ComputerStoreAPI.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductResponseDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<Category, CategoryResponseDto>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
