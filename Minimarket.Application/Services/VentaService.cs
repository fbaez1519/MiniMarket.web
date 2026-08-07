using AutoMapper;
using MiniMarket.web.DTOs;
using Minimarket.Domain.Entities;

namespace MiniMarket.web.Services;

public class VentaService : IVentaService
{
    private static List<Venta> _ventas = new List<Venta>();
    private static List<DetalleVenta> _detalles = new List<DetalleVenta>();
    private static int _nextId = 1;
    private static int _nextDetalleId = 1;
    private readonly IMapper _mapper;
    private readonly IProductoService _productoService;  

    public VentaService(IMapper mapper, IProductoService productoService)  
    {
        _mapper = mapper;
        _productoService = productoService;  

        // Datos de ejemplo
        if (!_ventas.Any())
        {
            // Crear una venta de ejemplo
            var venta = new Venta
            {
                Id = _nextId++,
                NumeroFactura = "FAC-001",
                Serie = "001",
                FechaVenta = DateTime.UtcNow.AddDays(-2),
                ClienteId = 1,
                UsuarioId = 1,
                Subtotal = 0,
                Impuesto = 0,
                Descuento = 0,
                Total = 0,
                TipoPago = "Efectivo",
                Estado = "Completada",
                TipoComprobante = "Factura",
                Anulada = false,
                Moneda = "PEN",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var detalle1 = new DetalleVenta
            {
                Id = _nextDetalleId++,
                VentaId = venta.Id,
                ProductoId = 1,
                Cantidad = 2,
                PrecioUnitario = 2.50m,
                Descuento = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            detalle1.CalcularSubtotal();

            var detalle2 = new DetalleVenta
            {
                Id = _nextDetalleId++,
                VentaId = venta.Id,
                ProductoId = 2,
                Cantidad = 3,
                PrecioUnitario = 1.20m,
                Descuento = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            detalle2.CalcularSubtotal();

            venta.Detalles.Add(detalle1);
            venta.Detalles.Add(detalle2);
            venta.CalcularTotalesConDetalles(venta.Detalles);

            _ventas.Add(venta);
            _detalles.Add(detalle1);
            _detalles.Add(detalle2);
        }
    }

    // ============================================================
    // MÉTODOS DE CONSULTA
    // ============================================================

    public async Task<IEnumerable<VentaDTO>> GetAllAsync()
    {
        var ventas = _ventas.OrderByDescending(v => v.FechaVenta);
        return await Task.FromResult(_mapper.Map<IEnumerable<VentaDTO>>(ventas));
    }

    public async Task<VentaDTO?> GetByIdAsync(int id)
    {
        var venta = _ventas.FirstOrDefault(v => v.Id == id);
        if (venta == null)
            return null;

        var dto = _mapper.Map<VentaDTO>(venta);
        dto.CantidadProductos = venta.Detalles?.Sum(d => d.Cantidad) ?? 0;
        
        return await Task.FromResult(dto);
    }

    public async Task<IEnumerable<VentaDTO>> GetByFechaAsync(DateTime fechaInicio, DateTime fechaFin)
    {
        var ventas = _ventas.Where(v => v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFin)
                            .OrderByDescending(v => v.FechaVenta);
        return await Task.FromResult(_mapper.Map<IEnumerable<VentaDTO>>(ventas));
    }

    public async Task<IEnumerable<VentaDTO>> GetByClienteAsync(int clienteId)
    {
        var ventas = _ventas.Where(v => v.ClienteId == clienteId)
                            .OrderByDescending(v => v.FechaVenta);
        return await Task.FromResult(_mapper.Map<IEnumerable<VentaDTO>>(ventas));
    }

    public async Task<IEnumerable<VentaDTO>> GetByUsuarioAsync(int usuarioId)
    {
        var ventas = _ventas.Where(v => v.UsuarioId == usuarioId)
                            .OrderByDescending(v => v.FechaVenta);
        return await Task.FromResult(_mapper.Map<IEnumerable<VentaDTO>>(ventas));
    }

    public async Task<VentaDTO?> GetByNumeroFacturaAsync(string numeroFactura)
    {
        var venta = _ventas.FirstOrDefault(v => v.NumeroFactura == numeroFactura);
        return await Task.FromResult(_mapper.Map<VentaDTO>(venta));
    }

    // ============================================================
    // MÉTODOS DE CREACIÓN Y ACTUALIZACIÓN
    // ============================================================

    public async Task<VentaDTO> CreateAsync(VentaCreateDTO ventaDto)
    {
        // Validar que tenga al menos un detalle
        if (ventaDto.Detalles == null || !ventaDto.Detalles.Any())
            throw new Exception("La venta debe tener al menos un producto");

        // Crear la venta
        var venta = new Venta
        {
            Id = _nextId++,
            NumeroFactura = $"FAC-{DateTime.Now:yyyyMMdd}-{_nextId:D4}",
            Serie = "001",
            FechaVenta = DateTime.UtcNow,
            ClienteId = ventaDto.ClienteId,
            UsuarioId = 1, // En producción, obtener del usuario autenticado
            TipoPago = ventaDto.TipoPago,
            Estado = "Completada",
            Comentarios = ventaDto.Comentarios,
            TipoComprobante = ventaDto.TipoComprobante,
            Anulada = false,
            Moneda = ventaDto.Moneda,
            TipoCambio = ventaDto.TipoCambio,
            Descuento = ventaDto.Descuento,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Crear los detalles
        foreach (var detalleDto in ventaDto.Detalles)
        {
            // ✅ CORREGIDO: Usar IProductoService en lugar de ProductoService._productos
            var producto = await _productoService.GetEntityByIdAsync(detalleDto.ProductoId);
            if (producto == null)
                throw new Exception($"Producto con ID {detalleDto.ProductoId} no encontrado");

            // Verificar stock
            if (producto.Stock < detalleDto.Cantidad)
                throw new Exception($"Stock insuficiente para el producto {producto.Nombre}. Disponible: {producto.Stock}");

            // Reducir stock
            producto.ReducirStock(detalleDto.Cantidad);

            var detalle = new DetalleVenta
            {
                Id = _nextDetalleId++,
                VentaId = venta.Id,
                ProductoId = detalleDto.ProductoId,
                Cantidad = detalleDto.Cantidad,
                PrecioUnitario = detalleDto.PrecioUnitario,
                Descuento = detalleDto.Descuento,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            detalle.CalcularSubtotal();
            venta.Detalles.Add(detalle);
            _detalles.Add(detalle);
        }

        // Calcular totales
        venta.CalcularTotalesConDetalles(venta.Detalles);

        _ventas.Add(venta);

        var dto = _mapper.Map<VentaDTO>(venta);
        dto.CantidadProductos = venta.Detalles.Sum(d => d.Cantidad);
        return await Task.FromResult(dto);
    }

    public async Task<VentaDTO> AnularAsync(int id, string motivo)
    {
        var venta = _ventas.FirstOrDefault(v => v.Id == id);
        if (venta == null)
            throw new Exception($"Venta con ID {id} no encontrada");

        if (venta.Anulada)
            throw new Exception("La venta ya está anulada");

        // Validar que se pueda anular (dentro de 24 horas)
        if (!venta.PuedeSerAnulada())
            throw new Exception("La venta no puede ser anulada. Han pasado más de 24 horas.");

        // ✅ CORREGIDO: Usar IProductoService en lugar de ProductoService._productos
        foreach (var detalle in venta.Detalles)
        {
            var producto = await _productoService.GetEntityByIdAsync(detalle.ProductoId);
            if (producto != null)
            {
                producto.AgregarStock(detalle.Cantidad);
            }
        }

        venta.Anular(motivo);
        
        return await Task.FromResult(_mapper.Map<VentaDTO>(venta));
    }

    public async Task<VentaDTO> UpdateAsync(VentaUpdateDTO ventaDto)
    {
        var venta = _ventas.FirstOrDefault(v => v.Id == ventaDto.Id);
        if (venta == null)
            throw new Exception($"Venta con ID {ventaDto.Id} no encontrada");

        if (venta.Anulada)
            throw new Exception("No se puede modificar una venta anulada");

        venta.Estado = ventaDto.Estado;
        venta.Comentarios = ventaDto.Comentarios;
        venta.Descuento = ventaDto.Descuento;
        venta.UpdatedAt = DateTime.UtcNow;
        
        // Recalcular totales con el nuevo descuento
        venta.CalcularTotalesConDetalles(venta.Detalles);

        return await Task.FromResult(_mapper.Map<VentaDTO>(venta));
    }

    // ============================================================
    // MÉTODOS DE ELIMINACIÓN
    // ============================================================

    public async Task<bool> DeleteAsync(int id)
    {
        var venta = _ventas.FirstOrDefault(v => v.Id == id);
        if (venta == null)
            return await Task.FromResult(false);

        // Eliminar detalles
        var detalles = _detalles.Where(d => d.VentaId == id).ToList();
        foreach (var detalle in detalles)
        {
            _detalles.Remove(detalle);
        }

        _ventas.Remove(venta);
        return await Task.FromResult(true);
    }
}