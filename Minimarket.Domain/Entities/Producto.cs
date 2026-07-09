using System;
using System.Collections.Generic;

namespace Minimarket.Domain.Entities
{
    public class Producto : BaseEntity
    {
        // ═══════════════════════════════════════════════════════
        // 📌 PROPIEDADES NORMALES (ya las tienes)
        // ═══════════════════════════════════════════════════════
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; } = 5;
        public int? StockMaximo { get; set; }
        public string UnidadMedida { get; set; } = "Unidad";
        public string? ImagenUrl { get; set; }
        public DateTime FechaIngreso { get; set; } = DateTime.UtcNow;
        public DateTime? FechaVencimiento { get; set; }
        public bool RequiereRefrigeracion { get; set; } = false;
        public decimal? TemperaturaRecomendada { get; set; }
        public decimal? Peso { get; set; }
        public string? Marca { get; set; }
        public string? PaisOrigen { get; set; }
        public decimal Impuesto { get; set; } = 18;

        // ═══════════════════════════════════════════════════════
        // 🔗 PROPIEDADES DE NAVEGACIÓN (¡AGREGAR ESTAS!)
        // ═══════════════════════════════════════════════════════
        public virtual Categoria Categoria { get; set; } = null!;
        public virtual ICollection<DetalleVenta> DetalleVentas { get; set; } = new List<DetalleVenta>();
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

        // ═══════════════════════════════════════════════════════
        // 📌 MÉTODOS (ya los tienes)
        // ═══════════════════════════════════════════════════════
        public bool TieneBajoStock() => Stock <= StockMinimo;
        public bool EstaAgotado() => Stock <= 0;
        public bool TieneExcesoStock() => StockMaximo.HasValue && Stock > StockMaximo.Value;

        public void AgregarStock(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero");
            Stock += cantidad;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ReducirStock(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero");
            if (Stock < cantidad)
                throw new InvalidOperationException($"Stock insuficiente. Disponible: {Stock}, Solicitado: {cantidad}");
            Stock -= cantidad;
            UpdatedAt = DateTime.UtcNow;
        }

        public decimal CalcularMargen()
        {
            if (PrecioCompra == 0) return 0;
            return ((PrecioVenta - PrecioCompra) / PrecioCompra) * 100;
        }

        public decimal CalcularPrecioConImpuesto()
        {
            return PrecioVenta + (PrecioVenta * Impuesto / 100);
        }

        public decimal AplicarDescuento(decimal porcentaje)
        {
            if (porcentaje < 0 || porcentaje > 100)
                throw new ArgumentException("El porcentaje de descuento debe estar entre 0 y 100");
            return PrecioVenta - (PrecioVenta * porcentaje / 100);
        }

        public bool EstaProximoAVencer()
        {
            if (!FechaVencimiento.HasValue) return false;
            return (FechaVencimiento.Value - DateTime.UtcNow).TotalDays <= 30;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Codigo))
                throw new ArgumentException("El código del producto es obligatorio");
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentException("El nombre del producto es obligatorio");
            if (CategoriaId <= 0)
                throw new ArgumentException("Debe seleccionar una categoría");
            if (PrecioCompra < 0)
                throw new ArgumentException("El precio de compra no puede ser negativo");
            if (PrecioVenta <= 0)
                throw new ArgumentException("El precio de venta debe ser mayor a cero");
            if (PrecioVenta < PrecioCompra)
                throw new ArgumentException("El precio de venta debe ser mayor al precio de compra");
            if (Stock < 0)
                throw new ArgumentException("El stock no puede ser negativo");
            if (StockMinimo < 0)
                throw new ArgumentException("El stock mínimo no puede ser negativo");
        }
    }
}