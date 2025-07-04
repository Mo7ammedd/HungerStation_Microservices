using AutoMapper;
using HungerStation.Services.OrderAPI.Models;
using HungerStation.Services.OrderAPI.Models.Dto;

namespace HungerStation.Services.OrderAPI.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<OrderHeaderDto, CartHeaderDto>()
            .ForMember(dest => dest.CartTotal, u => u.MapFrom(src => src.OrderTotal)).ReverseMap();

        CreateMap<CartDetailsDto, OrderDetailsDto>()
            .ForMember(dest => dest.ProductName, u => u.MapFrom(src => src.Product!.Name))
            .ForMember(dest => dest.Price, u => u.MapFrom(src => src.Product!.Price));

        CreateMap<OrderDetailsDto, CartDetailsDto>();

        CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
        CreateMap<OrderDetailsDto, OrderDetails>().ReverseMap();
    }
}
