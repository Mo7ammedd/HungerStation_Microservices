using HungerStation.Services.ShoppingCartAPI.Models.Dto;

namespace HungerStation.Services.ShoppingCartAPI1.Service.IService;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProducts();
}