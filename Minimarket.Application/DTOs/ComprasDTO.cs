using System;
using System.Collections.Generic;

namespace Minimarket.Application.DTOs
{
    public class CompraDTO
    {
        public int Id { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public DateTime FechaCompra { get; set; }
        public int ProveedorId { get; set; }
        public string? ProveedorNombre { get; set; }
        public int UsuarioId { get; set; }
        public string? UsuarioNombre { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public string? Observaciones { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public string? GuiaRemision { get; set; }
        public List<DetalleCompraDTO> Detalles { get; set; } = new List<DetalleCompraDTO>();
    }

    public class CompraCreateDTO
    {
        public int ProveedorId { get; set; }
        public string? Observaciones { get; set; }
        public string? GuiaRemision { get; set; }
        public List<DetalleCompraCreateDTO> Detalles { get; set; } = new List<DetalleCompraCreateDTO>();
    }

    public class CompraUpdateDTO
    {
        public int Id { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public string? Observaciones { get; set; }
    }

    public class DetalleCompraDTO
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string? ProductoNombre { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class DetalleCompraCreateDTO
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}