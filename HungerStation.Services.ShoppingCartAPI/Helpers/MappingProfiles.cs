using AutoMapper;
using HungerStation.Services.ShoppingCartAPI.Models;
using HungerStation.Services.ShoppingCartAPI.Models.Dto;


namespace HungerStation.Services.ShoppingCartAPI.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
        CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
    }
}
    
  
