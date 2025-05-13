using ComputerStoreAPI.DTOs;

namespace ComputerStoreAPI.Services
{
    public interface IStockImportService
    {
        Task ImportStockAsync(List<ProductStockDto> stockItems);
    }
}
