using System;

namespace Minimarket.Application.DTOs
{
    public class ProveedorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Contacto { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string? Ruc { get; set; }
        public string? PaginaWeb { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class ProveedorCreateDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Contacto { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string? Ruc { get; set; }
        public string? PaginaWeb { get; set; }
    }

    public class ProveedorUpdateDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Contacto { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string? Ruc { get; set; }
        public string? PaginaWeb { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
