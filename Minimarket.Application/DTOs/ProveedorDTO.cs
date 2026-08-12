using System;

namespace MiniMarket.web.DTOs
// Proveedor.cs
public class Proveedor
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Ruc { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    public string Direccion { get; set; }
    public bool Activo { get; set; }
    public DateTime FechaRegistro { get; set; }
    public ICollection<Compra> Compras { get; set; }
}

// Compra.cs
public class Compra


{
    public int Id { get; set; }
    public string NumeroCompra { get; set; }        
    public int ProveedorId { get; set; }
    public Proveedor Proveedor { get; set; }
    public int UsuarioId { get; set; }              // Quién registró la compra
    public Usuario Usuario { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Total { get; set; }
    public EstadoCompra Estado { get; set; }        // Pendiente, Recibida, Cancelada
    public string Observaciones { get; set; }
    public ICollection<DetalleCompra> Detalles { get; set; }
}

// DetalleCompra.cs
public class DetalleCompra
{
    public int Id { get; set; }
    public int CompraId { get; set; }
    public Compra Compra { get; set; }
    public int ProductoId { get; set; }
    public Producto Producto { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal => Cantidad * PrecioUnitario;
}