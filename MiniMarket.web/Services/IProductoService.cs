using MiniMarket.web.DTOs;

namespace MiniMarket.web.Services;

public interface IProductoService
{
    Task<IEnumerable<ProductoDTO>> GetAllAsync();
    Task<ProductoDTO?> GetByIdAsync(int id);
    Task<ProductoDTO> CreateAsync(ProductoCreateDTO productoDto);
    Task<ProductoDTO> UpdateAsync(ProductoUpdateDTO productoDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DesactivarAsync(int id);
    Task<bool> ActivarAsync(int id);
    Task<ProductoDTO?> GetByCodigoAsync(string codigo);
}