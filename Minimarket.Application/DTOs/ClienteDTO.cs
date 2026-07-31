using System;

namespace MiniMarket.web.DTOs
{
    /// <summary>
    /// DTO para mostrar/transferir datos del cliente
    /// </summary>
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Documento { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string TipoCliente { get; set; } = "Regular";
        public DateTime FechaRegistro { get; set; }
        public decimal? DescuentoEspecial { get; set; }
        public decimal? LimiteCredito { get; set; }
        public decimal Saldo { get; set; }
        public string? Genero { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public bool EstaActivo { get; set; } = true;
    }

    /// <summary>
    /// DTO para CREAR un nuevo cliente (POST)
    /// </summary>
    public class ClienteCreateDTO
    {
        public string Documento { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string TipoCliente { get; set; } = "Regular";
        public decimal? DescuentoEspecial { get; set; }
        public decimal? LimiteCredito { get; set; }
        public string? Genero { get; set; }
        public DateTime? FechaNacimiento { get; set; }
    }

    /// <summary>
    /// DTO para ACTUALIZAR un cliente (PUT)
    /// </summary>
    public class ClienteUpdateDTO
    {
        public int Id { get; set; }
        public string Documento { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string TipoCliente { get; set; } = "Regular";
        public decimal? DescuentoEspecial { get; set; }
        public decimal? LimiteCredito { get; set; }
        public string? Genero { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public bool EstaActivo { get; set; } = true;
    }
}