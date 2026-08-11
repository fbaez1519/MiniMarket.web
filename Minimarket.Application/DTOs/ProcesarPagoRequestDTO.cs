using System;

namespace MiniMarket.web.DTOs.Pago.Requests
{
    /// <summary>
    /// DTO para procesar un pago
    /// </summary>
    public class ProcesarPagoRequestDTO
    {
        /// <summary>
        /// ID de la venta asociada
        /// </summary>
        public int VentaId { get; set; }

        /// <summary>
        /// Monto total del pago
        /// </summary>
        public decimal Monto { get; set; }

        /// <summary>
        /// Método de pago: Efectivo, Tarjeta, Transferencia, Credito, Yape, Plin
        /// </summary>
        public string MetodoPago { get; set; } = "Efectivo";

        /// <summary>
        /// Moneda: PEN, USD, EUR
        /// </summary>
        public string Moneda { get; set; } = "PEN";

        /// <summary>
        /// Número de referencia (opcional)
        /// </summary>
        public string? Referencia { get; set; }

        /// <summary>
        /// Datos de la tarjeta (si el método es Tarjeta)
        /// </summary>
        public PagoTarjetaDTO? Tarjeta { get; set; }

        /// <summary>
        /// ID del cliente (opcional)
        /// </summary>
        public int? ClienteId { get; set; }

        /// <summary>
        /// Monto con el que paga el cliente (para efectivo)
        /// </summary>
        public decimal? MontoPagado { get; set; }

        /// <summary>
        /// Observaciones adicionales
        /// </summary>
        public string? Observaciones { get; set; }
    }

    /// <summary>
    /// DTO para datos de tarjeta de crédito/débito
    /// </summary>
    public class PagoTarjetaDTO
    {
        /// <summary>
        /// Número de la tarjeta (últimos 4 dígitos para seguridad)
        /// </summary>
        public string NumeroTarjeta { get; set; } = string.Empty;

        /// <summary>
        /// Nombre del titular
        /// </summary>
        public string NombreTitular { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de expiración (MM/AA)
        /// </summary>
        public string FechaExpiracion { get; set; } = string.Empty;

        /// <summary>
        /// Código de seguridad (CVV)
        /// </summary>
        public string Cvv { get; set; } = string.Empty;

        /// <summary>
        /// Tipo de tarjeta: Credito, Debito
        /// </summary>
        public string TipoTarjeta { get; set; } = "Credito";

        /// <summary>
        /// Número de cuotas (para crédito)
        /// </summary>
        public int? Cuotas { get; set; }
    }

    /// <summary>
    /// DTO para pago con token (tarjetas guardadas)
    /// </summary>
    public class PagoConTokenRequestDTO
    {
        /// <summary>
        /// ID del cliente
        /// </summary>
        public int ClienteId { get; set; }

        /// <summary>
        /// Token de la tarjeta guardada
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Monto a pagar
        /// </summary>
        public decimal Monto { get; set; }

        /// <summary>
        /// ID de la venta
        /// </summary>
        public int VentaId { get; set; }
    }
}