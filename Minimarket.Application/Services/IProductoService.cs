using MiniMarket.web.DTOs;
using Minimarket.Domain.Entities;

namespace MiniMarket.web.Services;

public interface IProductoService
{
    // ============================================================
    // MÉTODOS DE CONSULTA
    // ============================================================
    Task<IEnumerable<ProductoDTO>> GetAllAsync();
    Task<ProductoDTO?> GetByIdAsync(int id);
    Task<ProductoDTO?> GetByCodigoAsync(string codigo);
    
    /// <summary>
    /// Obtiene la entidad Producto (no el DTO) para uso interno
    /// </summary>
    Task<Producto?> GetEntityByIdAsync(int id);  // ✅ AGREGADO

    // ============================================================
    // MÉTODOS DE CREACIÓN Y ACTUALIZACIÓN
    // ============================================================
    Task<ProductoDTO> CreateAsync(ProductoCreateDTO productoDto);
    Task<ProductoDTO> UpdateAsync(ProductoUpdateDTO productoDto);

    // ============================================================
    // MÉTODOS DE ELIMINACIÓN Y ESTADO
    // ============================================================
    Task<bool> DeleteAsync(int id);
    Task<bool> DesactivarAsync(int id);
    Task<bool> ActivarAsync(int id);
}