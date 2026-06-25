using System.Collections.Generic;

namespace Minimarket.Domain.Entities
{
    public class Proveedor : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Contacto { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string? Ruc { get; set; }
        public string? PaginaWeb { get; set; }
        public string? Observaciones { get; set; }
        public int? Calificacion { get; set; }
        public int? TiempoEntregaDias { get; set; }

        // ═══════════════════════════════════════════════════════
        // 🔗 PROPIEDAD DE NAVEGACIÓN - AGREGADA
        // ═══════════════════════════════════════════════════════
        public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
    }
}