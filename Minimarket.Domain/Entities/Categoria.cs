using System.Collections.Generic;

namespace Minimarket.Domain.Entities
{
    /// <summary>
    /// Categoría de productos para organizar el inventario
    /// </summary>
    public class Categoria : BaseEntity
    {
        /// <summary>
        /// Nombre de la categoría (ej: Abarrotes, Bebidas, Lácteos)
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Descripción detallada de la categoría
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// Código de la categoría
        /// </summary>
        public string? Codigo { get; set; }

        /// <summary>
        /// Categoría padre ID (para jerarquías)
        /// </summary>
        public int? CategoriaPadreId { get; set; }

        /// <summary>
        /// Indica si es una categoría principal
        /// </summary>
        public bool EsPrincipal { get; set; } = false;

        /// <summary>
        /// Orden de visualización
        /// </summary>
        public int Orden { get; set; } = 0;

        /// <summary>
        /// Indica si la categoría está activa
        /// </summary>
        public bool EstaActivo { get; set; } = true;

        // 🔗 RELACIONES
        public virtual Categoria? CategoriaPadre { get; set; }
        public virtual ICollection<Categoria> SubCategorias { get; set; } = new List<Categoria>();
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

        /// <summary>
        /// Verifica si tiene subcategorías
        /// </summary>
        public bool TieneSubCategorias() => SubCategorias.Count > 0;

        /// <summary>
        /// Obtiene el nivel de profundidad de la categoría
        /// </summary>
        public int ObtenerNivel()
        {
            int nivel = 0;
            var actual = this;
            while (actual.CategoriaPadre != null)
            {
                nivel++;
                actual = actual.CategoriaPadre;
            }
            return nivel;
        }
    }
}