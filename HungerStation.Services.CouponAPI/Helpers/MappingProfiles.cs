using AutoMapper;
using HungerStation.Services.CouponAPI.Models;
using HungerStation.Services.CouponAPI.Models.Dtos;

namespace HungerStation.Services.CouponAPI.Helpers;

public class MappingProfiles : Profile
{
    
    public MappingProfiles()
    {
        CreateMap<CouponDto, Coupon>();
        CreateMap<Coupon, CouponDto>();
    }
}