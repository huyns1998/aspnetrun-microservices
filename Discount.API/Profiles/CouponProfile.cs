using AutoMapper;
using Discount.API.Dtos;
using Discount.API.Entities;

namespace Discount.API.Profiles
{
    public class CouponProfile: Profile
    {
        public CouponProfile() 
        {
            // Source --> Target
            CreateMap<Coupon, CouponReadDto>();
            CreateMap<CouponCreateDto, Coupon>();
            CreateMap<CouponUpdateDto, Coupon>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
