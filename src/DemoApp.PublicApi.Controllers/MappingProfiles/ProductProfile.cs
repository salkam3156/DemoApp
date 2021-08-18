using AutoMapper;
using DemoApp.ApplicationCore.Entities;
using DemoApp.PublicApi.Controllers.DTO;

namespace DemoApp.PublicApi.Controllers.MappingProfiles
{
    internal sealed class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
