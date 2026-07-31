using MiniMarket.web.DTOs;

namespace MiniMarket.web.Services;

public class VentaService : IVentaService
{
    // Base de datos en memoria (temporal)
    private static List<VentaDTO> _ventas = new()
    {
        new VentaDTO 
        { 
            Id = 1, 
            UsuarioId = 2, 
            Fecha = DateTime.Now.AddDays(-1), 
            Total = 15.50m,
            Detalles = new List<VentaDetalleDTO>
            {
                new VentaDetalleDTO { ProductoId = 1, ProductoNombre = "Leche", Cantidad = 2, PrecioUnitario = 2.50m, Subtotal = 5.00m },
                new VentaDetalleDTO { ProductoId = 3, ProductoNombre = "Huevos", Cantidad = 1, PrecioUnitario = 3.00m, Subtotal = 3.00m }
            }
        },
        new VentaDTO 
        { 
            Id = 2, 
            UsuarioId = 3, 
            Fecha = DateTime.Now, 
            Total = 7.40m,
            Detalles = new List<VentaDetalleDTO>
            {
                new VentaDetalleDTO { ProductoId = 2, ProductoNombre = "Pan", Cantidad = 2, PrecioUnitario = 1.20m, Subtotal = 2.40m },
                new VentaDetalleDTO { ProductoId = 4, ProductoNombre = "Arroz", Cantidad = 1, PrecioUnitario = 1.80m, Subtotal = 1.80m }
            }
        }
    };
    private static int _nextId = 3;

    public Task<IEnumerable<VentaDTO>> GetAllAsync()
        => Task.FromResult(_ventas.AsEnumerable());

    public Task<VentaDTO?> GetByIdAsync(int id)
        => Task.FromResult(_ventas.FirstOrDefault(v => v.Id == id));

    public Task<VentaDTO> CreateAsync(VentaDTO venta)
    {
        venta.Id = _nextId++;
        venta.Fecha = DateTime.Now;
        venta.Total = venta.Detalles.Sum(d => d.Subtotal);
        _ventas.Add(venta);
        return Task.FromResult(venta);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var venta = _ventas.FirstOrDefault(v => v.Id == id);
        if (venta != null)
        {
            _ventas.Remove(venta);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}