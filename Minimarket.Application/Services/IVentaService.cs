using MiniMarket.web.DTOs;

namespace MiniMarket.web.Services;

public interface IVentaService
{
    Task<IEnumerable<VentaDTO>> GetAllAsync();
    Task<VentaDTO?> GetByIdAsync(int id);
    Task<VentaDTO> CreateAsync(VentaDTO venta);
    Task<bool> DeleteAsync(int id);
}