
using System;
using System.Collections.Generic;
using MiniMarket.web.Enums;

namespace MiniMarket.web.DTOs.Pago.Responses
{
    public class ProcesarPagoResponseDTO
    {
        public int Id { get; set; }
        public int VentaId { get; set; }
        public MetodoPagoEnum MetodoPago { get; set; }
        public string MetodoPagoNombre => MetodoPago.GetDisplayName();
        public decimal MontoTotal { get; set; }
        public MonedaEnum Moneda { get; set; }
        public string MonedaSimbolo => Moneda == MonedaEnum.PEN ? "S/" : "$";
        public string MontoFormateado => $"{MonedaSimbolo} {MontoTotal:N2}";
        public EstadoPagoEnum Estado { get; set; }
        public string EstadoNombre => Estado.GetDisplayName();
        public string? NumeroTransaccion { get; set; }
        public string? CodigoAutorizacion { get; set; }
        public DateTime FechaPago { get; set; }
        public string? ComprobanteUrl { get; set; }
        public string? Mensaje { get; set; }
        public bool Exitoso { get; set; }
        public string? Ticket { get; set; }
        public decimal? Vuelto { get; set; }
        public string? CodigoQR { get; set; }
        public List<PagoMetodoDTO> MetodosDisponibles { get; set; } = new();
        public PagoDetalleResponseDTO? DetallesPago { get; set; }
    }

    public class PagoDetalleResponseDTO
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class PagoMetodoDTO
    {
        public MetodoPagoEnum Metodo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Disponible { get; set; }
        public string? Icono { get; set; }
    }

    public class EstadoPagoResponseDTO
    {
        public int Id { get; set; }
        public EstadoPagoEnum Estado { get; set; }
        public string EstadoNombre { get; set; } = string.Empty;
        public string? Mensaje { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public bool PuedeReintentar { get; set; }
        public int IntentosFallidos { get; set; }
        public int MaximoReintentos { get; set; } = 3;
    }
}