using System;
using System.Collections.Generic;

namespace Minimarket.Domain.Entities
{
    public class Compra : BaseEntity
    {
        // ═══════════════════════════════════════════════════════
        // 📌 PROPIEDADES NORMALES
        // ═══════════════════════════════════════════════════════
        public string NumeroFactura { get; set; } = string.Empty;
        public DateTime FechaCompra { get; set; } = DateTime.UtcNow;
        public int ProveedorId { get; set; }
        public int UsuarioId { get; set; }
        
        // 🆕 AGREGAR ESTAS DOS PROPIEDADES (CreatedAt y UpdatedAt YA VIENEN DE BaseEntity)
        public decimal Subtotal { get; set; }          // ← NUEVA
        public decimal Impuesto { get; set; }          // ← NUEVA
        public decimal Total { get; set; }
        
        public string Estado { get; set; } = "Pendiente";
        public string? Observaciones { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public string? GuiaRemision { get; set; }

        // ═══════════════════════════════════════════════════════
        // 🔗 PROPIEDADES DE NAVEGACIÓN
        // ═══════════════════════════════════════════════════════
        public virtual Proveedor Proveedor { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual ICollection<DetalleCompra> Detalles { get; set; } = new List<DetalleCompra>();

        // ═══════════════════════════════════════════════════════
        // 📌 MÉTODOS
        // ═══════════════════════════════════════════════════════
        public void Completar()
        {
            Estado = "Completada";
            FechaRecepcion = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancelar()
        {
            Estado = "Cancelada";
            UpdatedAt = DateTime.UtcNow;
        }

        public bool PuedeSerCancelada() => Estado == "Pendiente";
    }
}