using HungerStation.Web.Models;
using HungerStation.Web.Models.Dto;

namespace HungerStation.Web.Service.IService;

public interface IProductService
{
    Task<ResponseDto?> GetProductAsync(string couponCode);
    Task<ResponseDto?> GetAllProductsAsync();
    Task<ResponseDto?> GetProductByIdAsync(int id);
    Task<ResponseDto?> CreateProductsAsync(ProductDto productDto);
    Task<ResponseDto?> UpdateProductsAsync(ProductDto productDto);
    Task<ResponseDto?> DeleteProductsAsync(int id);
} 
