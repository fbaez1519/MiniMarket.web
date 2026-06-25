using Microsoft.EntityFrameworkCore.Storage;
using Minimarket.Domain.Entities;
using Minimarket.Infrastructure.Data;
using System;
using System.Threading.Tasks;

namespace Minimarket.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;  // ← LÍNEA 29: Agregar ? (CORREGIDO)
        private bool _disposed;

        public IRepository<Usuario> Usuarios { get; private set; }
        public IRepository<Cliente> Clientes { get; private set; }
        public IRepository<Producto> Productos { get; private set; }
        public IRepository<Categoria> Categorias { get; private set; }
        public IRepository<Venta> Ventas { get; private set; }
        public IRepository<DetalleVenta> DetalleVentas { get; private set; }
        public IRepository<Proveedor> Proveedores { get; private set; }
        public IRepository<Compra> Compras { get; private set; }
        public IRepository<DetalleCompra> DetalleCompras { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            Usuarios = new Repository<Usuario>(_context);
            Clientes = new Repository<Cliente>(_context);
            Productos = new Repository<Producto>(_context);
            Categorias = new Repository<Categoria>(_context);
            Ventas = new Repository<Venta>(_context);
            DetalleVentas = new Repository<DetalleVenta>(_context);
            Proveedores = new Repository<Proveedor>(_context);
            Compras = new Repository<Compra>(_context);
            DetalleCompras = new Repository<DetalleCompra>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)  // ← LÍNEA 57: Validar null (CORREGIDO)
            {
                await _transaction.CommitAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)  // ← LÍNEA 62: Validar null (CORREGIDO)
            {
                await _transaction.RollbackAsync();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _transaction?.Dispose();
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}