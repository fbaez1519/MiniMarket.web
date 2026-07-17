using System;

namespace MiniMarket.web.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int CategoriaId { get; set; }
        public string? CategoriaNombre { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public int? StockMaximo { get; set; }
        public string UnidadMedida { get; set; } = "Unidad";
        public string? ImagenUrl { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public bool RequiereRefrigeracion { get; set; }
        public decimal? TemperaturaRecomendada { get; set; }
        public decimal? Peso { get; set; }
        public string? Marca { get; set; }
        public string? PaisOrigen { get; set; }
        public decimal Impuesto { get; set; }
        public bool EstaActivo { get; set; } = true;
    }

    public class ProductoCreateDTO
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int CategoriaId { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; } = 5;
        public int? StockMaximo { get; set; }
        public string UnidadMedida { get; set; } = "Unidad";
        public string? ImagenUrl { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public bool RequiereRefrigeracion { get; set; }
        public decimal? TemperaturaRecomendada { get; set; }
        public decimal? Peso { get; set; }
        public string? Marca { get; set; }
        public string? PaisOrigen { get; set; }
        public decimal Impuesto { get; set; } = 18;
    }

    public class ProductoUpdateDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int CategoriaId { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; } = 5;
        public int? StockMaximo { get; set; }
        public string UnidadMedida { get; set; } = "Unidad";
        public string? ImagenUrl { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public bool RequiereRefrigeracion { get; set; }
        public decimal? TemperaturaRecomendada { get; set; }
        public decimal? Peso { get; set; }
        public string? Marca { get; set; }
        public string? PaisOrigen { get; set; }
        public decimal Impuesto { get; set; } = 18;
        public bool EstaActivo { get; set; } = true;
    }
}