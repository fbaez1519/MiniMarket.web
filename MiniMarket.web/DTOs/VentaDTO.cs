namespace MiniMarket.web.DTOs;

public class VentaDTO
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Total { get; set; }
    public List<VentaDetalleDTO> Detalles { get; set; }
}

public class VentaDetalleDTO
{
    public int ProductoId { get; set; }
    public string ProductoNombre { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}