using ComputerStoreAPI.DTOs;

namespace ComputerStoreAPI.Services
{
    public interface IProductService
    {
        Task<List<ProductResponseDto>> GetAllProductsAsync();
        Task<ProductResponseDto> GetProductByIdAsync(int id);
        Task<List<ProductResponseDto>> CreateProductAsync(ProductDto productDto);
        Task<List<ProductResponseDto>> UpdateProductAsync(int id, ProductDto productDto);
        Task<List<ProductResponseDto>> DeleteProductAsync(int id);
    }
}
