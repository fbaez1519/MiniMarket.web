using System;

namespace Minimarket.Domain.Entities
{
    public class DetalleVenta : BaseEntity  // ✅ AHORA HEREDA DE BaseEntity
    {
        public int Id { get; set; }
        public int VentaId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal Subtotal { get; set; }

        // ═══════════════════════════════════════════════════════
        // 🔗 PROPIEDADES DE NAVEGACIÓN
        // ═══════════════════════════════════════════════════════
        public virtual Venta Venta { get; set; } = null!;
        public virtual Producto Producto { get; set; } = null!;

        /// <summary>
        /// Calcula el subtotal del detalle
        /// </summary>
        public void CalcularSubtotal()
        {
            Subtotal = (Cantidad * PrecioUnitario) - Descuento;
        }

        /// <summary>
        /// Valida los datos del detalle
        /// </summary>
        public void Validar()
        {
            if (Cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero");
            if (PrecioUnitario < 0)
                throw new ArgumentException("El precio no puede ser negativo");
            if (Descuento < 0)
                throw new ArgumentException("El descuento no puede ser negativo");
            if (Descuento > (Cantidad * PrecioUnitario))
                throw new ArgumentException("El descuento no puede ser mayor al subtotal");
        }
    }
}