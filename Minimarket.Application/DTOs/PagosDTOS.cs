using System;
using System.Collections.Generic;

namespace MiniMarket.web.DTOs.Pago.Responses
{
    public class ProcesarPagoResponseDTO
    {
        public int Id { get; set; }
        public int VentaId { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public decimal MontoTotal { get; set; }
        public string Moneda { get; set; } = "PEN";
        public string Estado { get; set; } = "Pendiente";
        public string? NumeroTransaccion { get; set; }
        public string? CodigoAutorizacion { get; set; }
        public DateTime FechaPago { get; set; }
        public string? ComprobanteUrl { get; set; }
        public string? Mensaje { get; set; }
        public bool Exitoso { get; set; }
        public decimal? Vuelto { get; set; }
        public List<PagoDetalleDTO>? Detalles { get; set; }
    }

    public class PagoDetalleDTO
    {
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class EstadoPagoResponseDTO
    {
        public int Id { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? Mensaje { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public bool PuedeReintentar { get; set; }
        public int IntentosFallidos { get; set; }
        public int MaximoReintentos { get; set; } = 3;
    }

    public class PagoMetodoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Icono { get; set; }
        public bool Disponible { get; set; }
    }
}