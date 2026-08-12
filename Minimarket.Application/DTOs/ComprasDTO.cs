using System;

namespace MiniMarket.web.DTOs

// ComprasDTO.cs
public class CompraDto
{
    public int Id { get; set; }
    public string NumeroCompra { get; set; }
    public DateTime Fecha { get; set; }
    public int ProveedorId { get; set; }
    public string ProveedorNombre { get; set; }
    public decimal Total { get; set; }
    public EstadoCompra Estado { get; set; }
    public List<DetalleCompraDto> Detalles { get; set; }
}

// DetalleCompraDto.cs
public class DetalleCompraDto
{
    public int ProductoId { get; set; }
    public string ProductoNombre { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}

// CrearCompraDto.cs
public class CrearCompraDto
{
    public int ProveedorId { get; set; }
    public int UsuarioId { get; set; }
    public List<DetalleCompraRequestDto> Detalles { get; set; }
    public string Observaciones { get; set; }
}

public class DetalleCompraRequestDto
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}
