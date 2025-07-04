using HungerStation.Services.OrderAPI.Models.Dto;

namespace HungerStation.Services.OrderAPI.Service.IService;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProducts();
}
