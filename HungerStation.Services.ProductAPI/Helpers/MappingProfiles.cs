using AutoMapper;
using HungerStation.Services.ProductAPI.Models;
using HungerStation.Services.ProductAPI.Models.Dto;


namespace HungerStation.Services.CouponAPI.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
    
  
}