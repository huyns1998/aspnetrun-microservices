using AutoMapper;
using Basket.API.Dtos;
using Basket.API.Entities;

namespace Basket.API.Profiles
{
    public class BasketProfile: Profile
    {
        public BasketProfile() 
        {
            // Source --> Target
            CreateMap<ShoppingCart, ShoppingCartReadDto>();
            CreateMap<ShoppingCartItem, ShoppingCartItemReadDto>();
            CreateMap<ShoppingCartCreateDto, ShoppingCart>();
            CreateMap<ShoppingCartUpdateDto, ShoppingCart>()
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                 .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                 .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<ShoppingCartItemUpdateDto, ShoppingCartItem>();
        }  
    }
}
