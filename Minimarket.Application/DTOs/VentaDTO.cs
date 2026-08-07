using System;
using System.Collections.Generic;

namespace MiniMarket.web.DTOs
{
    /// <summary>
    /// DTO para mostrar/transferir datos de la venta
    /// </summary>
    public class VentaDTO
    {
        public int Id { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public string Serie { get; set; } = "001";
        public DateTime FechaVenta { get; set; }
        public int ClienteId { get; set; }
        public string? ClienteNombre { get; set; }
        public int UsuarioId { get; set; }
        public string? UsuarioNombre { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public string TipoPago { get; set; } = "Efectivo";
        public string Estado { get; set; } = "Completada";
        public string? Comentarios { get; set; }
        public string TipoComprobante { get; set; } = "Factura";
        public bool Anulada { get; set; }
        public DateTime? FechaAnulacion { get; set; }
        public string? MotivoAnulacion { get; set; }
        public string Moneda { get; set; } = "PEN";
        public decimal? TipoCambio { get; set; }
        public List<DetalleVentaDTO> Detalles { get; set; } = new List<DetalleVentaDTO>();
        public int CantidadProductos { get; set; }
    }

    /// <summary>
    /// DTO para CREAR una nueva venta
    /// </summary>
    public class VentaCreateDTO
    {
        public int ClienteId { get; set; }
        public string TipoPago { get; set; } = "Efectivo";
        public string? Comentarios { get; set; }
        public string TipoComprobante { get; set; } = "Factura";
        public string Moneda { get; set; } = "PEN";
        public decimal? TipoCambio { get; set; }
        public decimal Descuento { get; set; } = 0;
        public List<DetalleVentaCreateDTO> Detalles { get; set; } = new List<DetalleVentaCreateDTO>();
    }

    /// <summary>
    /// DTO para ACTUALIZAR una venta
    /// </summary>
    public class VentaUpdateDTO
    {
        public int Id { get; set; }
        public string Estado { get; set; } = "Completada";
        public string? Comentarios { get; set; }
        public decimal Descuento { get; set; } = 0;
    }

    /// <summary>
    /// DTO para ANULAR una venta
    /// </summary>
    public class VentaAnularDTO
    {
        public string MotivoAnulacion { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO para detalles de una venta
    /// </summary>
    public class DetalleVentaDTO
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string? ProductoNombre { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal Subtotal { get; set; }
    }

    /// <summary>
    /// DTO para CREAR un detalle de venta
    /// </summary>
    public class DetalleVentaCreateDTO
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; } = 0;
    }
}