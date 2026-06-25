using MiniMarket.web.DTOs;

namespace MiniMarket.web.Services;

public interface IProductoService
{
    Task<IEnumerable<ProductoDTO>> GetAllAsync();
    Task<ProductoDTO?> GetByIdAsync(int id);
    Task<ProductoDTO> CreateAsync(ProductoDTO producto);
    Task<ProductoDTO?> UpdateAsync(int id, ProductoDTO producto);
    Task<bool> DeleteAsync(int id);
}