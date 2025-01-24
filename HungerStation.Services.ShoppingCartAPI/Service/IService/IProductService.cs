using HungerStation.Services.ShoppingCartAPI.Models.Dto;

namespace HungerStation.Services.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
