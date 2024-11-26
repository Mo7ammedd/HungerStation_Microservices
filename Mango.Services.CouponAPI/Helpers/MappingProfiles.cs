using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dtos;

namespace Mango.Services.CouponAPI.Helpers;

public class MappingProfiles : Profile
{
    
    public MappingProfiles()
    {
        CreateMap<CouponDto, Coupon>();
        CreateMap<Coupon, CouponDto>();
    }
}