using Minimarket.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Minimarket.Infrastructure.Repositories
{
    /// <summary>
    /// Unit of Work pattern para manejar transacciones
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Usuario> Usuarios { get; }
        IRepository<Cliente> Clientes { get; }
        IRepository<Producto> Productos { get; }
        IRepository<Categoria> Categorias { get; }
        IRepository<Venta> Ventas { get; }
        IRepository<DetalleVenta> DetalleVentas { get; }
        IRepository<Proveedor> Proveedores { get; }
        IRepository<Compra> Compras { get; }
        IRepository<DetalleCompra> DetalleCompras { get; }

        Task<int> CompleteAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}