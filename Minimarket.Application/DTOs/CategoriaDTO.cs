using System;

namespace MiniMarket.web.DTOs
{
    /// <summary>
    /// DTO para mostrar/transferir datos de la categoría
    /// </summary>
    public class CategoriaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Codigo { get; set; }
        public int? CategoriaPadreId { get; set; }
        public string? CategoriaPadreNombre { get; set; }
        public bool EsPrincipal { get; set; }
        public int Orden { get; set; }
        public bool EstaActivo { get; set; } = true;
        public int CantidadProductos { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// DTO para CREAR una nueva categoría
    /// </summary>
    public class CategoriaCreateDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Codigo { get; set; }
        public int? CategoriaPadreId { get; set; }
        public bool EsPrincipal { get; set; } = false;
        public int Orden { get; set; } = 0;
    }

    /// <summary>
    /// DTO para ACTUALIZAR una categoría
    /// </summary>
    public class CategoriaUpdateDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Codigo { get; set; }
        public int? CategoriaPadreId { get; set; }
        public bool EsPrincipal { get; set; } = false;
        public int Orden { get; set; } = 0;
        public bool EstaActivo { get; set; } = true;
    }
}