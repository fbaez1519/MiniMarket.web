using System;

namespace Minimarket.Application.DTOs
{
    public class FiltroCompraDto
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? ProveedorId { get; set; }
        public string? Estado { get; set; }
    }
}