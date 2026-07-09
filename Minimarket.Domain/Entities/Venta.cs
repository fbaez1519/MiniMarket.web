using System;
using System.Collections.Generic;
using System.Linq;

namespace Minimarket.Domain.Entities
{
    public class Venta : BaseEntity
    {
        // ═══════════════════════════════════════════════════════
        // 📌 PROPIEDADES NORMALES (ya las tienes)
        // ═══════════════════════════════════════════════════════
        public string NumeroFactura { get; set; } = string.Empty;
        public string Serie { get; set; } = "001";
        public DateTime FechaVenta { get; set; } = DateTime.UtcNow;
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public string TipoPago { get; set; } = "Efectivo";
        public string Estado { get; set; } = "Completada";
        public string? Comentarios { get; set; }
        public string TipoComprobante { get; set; } = "Factura";
        public bool Anulada { get; set; } = false;
        public DateTime? FechaAnulacion { get; set; }
        public string? MotivoAnulacion { get; set; }
        public string Moneda { get; set; } = "PEN";
        public decimal? TipoCambio { get; set; }

        // ═══════════════════════════════════════════════════════
        // 🔗 PROPIEDADES DE NAVEGACIÓN (¡AGREGAR ESTAS!)
        // ═══════════════════════════════════════════════════════
        public virtual Cliente Cliente { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();

        // ═══════════════════════════════════════════════════════
        // 📌 MÉTODOS (ya los tienes)
        // ═══════════════════════════════════════════════════════
        public void CalcularTotales()
        {
            if (Subtotal < 0)
                throw new ArgumentException("El subtotal no puede ser negativo");

            Impuesto = Subtotal * 0.18m;
            Total = Subtotal + Impuesto - Descuento;

            if (Total < 0)
                throw new ArgumentException("El total no puede ser negativo");
        }

        public void CalcularTotalesConDetalles(ICollection<DetalleVenta> detalles)
        {
            if (detalles == null || !detalles.Any())
                throw new ArgumentException("La venta debe tener al menos un detalle");

            Subtotal = 0;
            foreach (var detalle in detalles)
            {
                detalle.CalcularSubtotal();
                Subtotal += detalle.Subtotal;
            }

            Impuesto = Subtotal * 0.18m;
            Total = Subtotal + Impuesto - Descuento;
        }

        public void AplicarDescuento(decimal porcentaje)
        {
            if (porcentaje < 0 || porcentaje > 100)
                throw new ArgumentException("El descuento debe estar entre 0 y 100");

            Descuento = (Subtotal * porcentaje) / 100;
            Total = Subtotal + Impuesto - Descuento;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Anular(string motivo)
        {
            if (Anulada)
                throw new InvalidOperationException("La venta ya fue anulada");

            if (string.IsNullOrWhiteSpace(motivo))
                throw new ArgumentException("Debe especificar un motivo de anulación");

            Anulada = true;
            FechaAnulacion = DateTime.UtcNow;
            MotivoAnulacion = motivo;
            Estado = "Cancelada";
            UpdatedAt = DateTime.UtcNow;
        }

        public bool PuedeSerAnulada()
        {
            return !Anulada && (DateTime.UtcNow - FechaVenta).TotalHours <= 24;
        }

        public bool EstaCompletada() => Estado == "Completada" && !Anulada;
        public bool EstaPendiente() => Estado == "Pendiente" && !Anulada;

        public void Completar()
        {
            if (Anulada)
                throw new InvalidOperationException("No se puede completar una venta anulada");
            Estado = "Completada";
            UpdatedAt = DateTime.UtcNow;
        }

        public void Validar()
        {
            if (ClienteId <= 0)
                throw new ArgumentException("Debe seleccionar un cliente");
            if (UsuarioId <= 0)
                throw new ArgumentException("Debe seleccionar un usuario");
            if (string.IsNullOrEmpty(TipoPago))
                throw new ArgumentException("Debe seleccionar un método de pago");
            if (Total < 0)
                throw new ArgumentException("El total no puede ser negativo");
            if (Subtotal < 0)
                throw new ArgumentException("El subtotal no puede ser negativo");
        }
    }
}