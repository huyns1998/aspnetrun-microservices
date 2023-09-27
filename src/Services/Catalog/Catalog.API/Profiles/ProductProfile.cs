using AutoMapper;
using Catalog.API.Dtos;
using Catalog.API.Entities;
using System.Runtime.InteropServices;

namespace Catalog.API.Profiles
{
    public class ProductProfile: Profile
    {  
        public ProductProfile()
        {
            // Source --> Target
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
