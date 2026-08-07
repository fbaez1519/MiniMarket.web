using MiniMarket.web.DTOs;

namespace MiniMarket.web.Services;

public interface IVentaService
{
    // ============================================================
    // MÉTODOS DE CONSULTA
    // ============================================================
    
    /// <summary>
    /// Obtiene todas las ventas
    /// </summary>
    Task<IEnumerable<VentaDTO>> GetAllAsync();
    
    /// <summary>
    /// Obtiene una venta por su ID
    /// </summary>
    Task<VentaDTO?> GetByIdAsync(int id);
    
    /// <summary>
    /// Obtiene ventas por rango de fechas
    /// </summary>
    Task<IEnumerable<VentaDTO>> GetByFechaAsync(DateTime fechaInicio, DateTime fechaFin);
    
    /// <summary>
    /// Obtiene ventas de un cliente específico
    /// </summary>
    Task<IEnumerable<VentaDTO>> GetByClienteAsync(int clienteId);
    
    /// <summary>
    /// Obtiene ventas de un usuario específico
    /// </summary>
    Task<IEnumerable<VentaDTO>> GetByUsuarioAsync(int usuarioId);
    
    /// <summary>
    /// Obtiene ventas por número de factura
    /// </summary>
    Task<VentaDTO?> GetByNumeroFacturaAsync(string numeroFactura);

    // ============================================================
    // MÉTODOS DE CREACIÓN Y ACTUALIZACIÓN
    // ============================================================
    
    /// <summary>
    /// Crea una nueva venta
    /// </summary>
    Task<VentaDTO> CreateAsync(VentaCreateDTO ventaDto);
    
    /// <summary>
    /// Anula una venta
    /// </summary>
    Task<VentaDTO> AnularAsync(int id, string motivo);
    
    /// <summary>
    /// Actualiza el estado de una venta
    /// </summary>
    Task<VentaDTO> UpdateAsync(VentaUpdateDTO ventaDto);

    // ============================================================
    // MÉTODOS DE ELIMINACIÓN
    // ============================================================
    
    /// <summary>
    /// Elimina una venta (borrado físico)
    /// </summary>
    Task<bool> DeleteAsync(int id);
}