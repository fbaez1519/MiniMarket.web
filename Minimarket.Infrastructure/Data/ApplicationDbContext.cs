using Microsoft.EntityFrameworkCore;
using Minimarket.Domain.Entities;

namespace Minimarket.Infrastructure.Data
{
    /// <summary>
    /// Contexto principal de Entity Framework Core para el minimarket
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 📋 DbSets para cada entidad
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DetalleCompra> DetalleCompras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.NombreCompleto).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(u => u.Rol).IsRequired().HasMaxLength(20);

                // Ignorar propiedades que no están en la BD
                entity.Ignore(u => u.EstaBloqueado());
            });

            // Configurar Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasIndex(c => c.Documento).IsUnique();
                entity.Property(c => c.Documento).IsRequired().HasMaxLength(20);
                entity.Property(c => c.NombreCompleto).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Telefono).HasMaxLength(15);
                entity.Property(c => c.Email).HasMaxLength(100);
                entity.Property(c => c.Direccion).HasMaxLength(200);
                entity.Property(c => c.TipoCliente).HasMaxLength(20);
            });

            // Configurar Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.Property(c => c.Nombre).IsRequired().HasMaxLength(50);
                entity.Property(c => c.Descripcion).HasMaxLength(200);
                entity.Property(c => c.Codigo).HasMaxLength(20);

                // Auto-relación para subcategorías
                entity.HasOne(c => c.CategoriaPadre)
                      .WithMany(c => c.SubCategorias)
                      .HasForeignKey(c => c.CategoriaPadreId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configurar Producto
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasIndex(p => p.Codigo).IsUnique();
                entity.Property(p => p.Codigo).IsRequired().HasMaxLength(20);
                entity.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Descripcion).HasMaxLength(200);
                entity.Property(p => p.UnidadMedida).HasMaxLength(20);
                entity.Property(p => p.Marca).HasMaxLength(50);
                entity.Property(p => p.ImagenUrl).HasMaxLength(500);
                entity.Property(p => p.PaisOrigen).HasMaxLength(50);

                // Relación con Categoria
                entity.HasOne(p => p.Categoria)
                      .WithMany(c => c.Productos)
                      .HasForeignKey(p => p.CategoriaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configurar Venta
            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasIndex(v => v.NumeroFactura).IsUnique();
                entity.Property(v => v.NumeroFactura).IsRequired().HasMaxLength(20);
                entity.Property(v => v.Serie).HasMaxLength(10);
                entity.Property(v => v.TipoPago).HasMaxLength(20);
                entity.Property(v => v.Estado).HasMaxLength(20);
                entity.Property(v => v.TipoComprobante).HasMaxLength(20);
                entity.Property(v => v.Moneda).HasMaxLength(3);

                // Relación con Cliente
                entity.HasOne(v => v.Cliente)
                      .WithMany(c => c.Ventas)
                      .HasForeignKey(v => v.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con Usuario
                entity.HasOne(v => v.Usuario)
                      .WithMany(u => u.Ventas)
                      .HasForeignKey(v => v.UsuarioId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configurar DetalleVenta
            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.HasKey(d => d.Id);

                // Relación con Venta
                entity.HasOne(d => d.Venta)
                      .WithMany(v => v.Detalles)
                      .HasForeignKey(d => d.VentaId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relación con Producto
                entity.HasOne(d => d.Producto)
                      .WithMany(p => p.DetalleVentas)
                      .HasForeignKey(d => d.ProductoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configurar Proveedor
            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Contacto).HasMaxLength(100);
                entity.Property(p => p.Telefono).HasMaxLength(15);
                entity.Property(p => p.Email).HasMaxLength(100);
                entity.Property(p => p.Direccion).HasMaxLength(200);
                entity.Property(p => p.Ruc).HasMaxLength(20);
                entity.Property(p => p.PaginaWeb).HasMaxLength(100);
            });

            // Configurar Compra
            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasIndex(c => c.NumeroFactura).IsUnique();
                entity.Property(c => c.NumeroFactura).IsRequired().HasMaxLength(20);
                entity.Property(c => c.Estado).HasMaxLength(20);

                // Relación con Proveedor
                entity.HasOne(c => c.Proveedor)
                      .WithMany(p => p.Compras)
                      .HasForeignKey(c => c.ProveedorId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con Usuario
                entity.HasOne(c => c.Usuario)
                      .WithMany(u => u.Compras)
                      .HasForeignKey(c => c.UsuarioId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configurar DetalleCompra
            modelBuilder.Entity<DetalleCompra>(entity =>
            {
                entity.HasKey(d => d.Id);

                // Relación con Compra
                entity.HasOne(d => d.Compra)
                      .WithMany(c => c.Detalles)
                      .HasForeignKey(d => d.CompraId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relación con Producto
                entity.HasOne(d => d.Producto)
                      .WithMany(p => p.DetalleCompras)
                      .HasForeignKey(d => d.ProductoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 📊 Datos semilla (Seed Data) - Opcional
            SeedData(modelBuilder);
        }

        /// <summary>
        /// Agrega datos iniciales a la base de datos
        /// </summary>
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Usuario administrador por defecto
            // Contraseña: Admin123 (en producción usar hash real)
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    NombreCompleto = "Administrador",
                    Email = "admin@minimarket.com",
                    Username = "admin",
                    PasswordHash = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", // "admin"
                    Rol = "Administrador",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Categorías iniciales
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nombre = "Abarrotes", Descripcion = "Productos envasados y no perecederos", EsPrincipal = true, IsActive = true, CreatedAt = DateTime.UtcNow },
                new Categoria { Id = 2, Nombre = "Bebidas", Descripcion = "Bebidas frías y calientes", EsPrincipal = true, IsActive = true, CreatedAt = DateTime.UtcNow },
                new Categoria { Id = 3, Nombre = "Lácteos", Descripcion = "Productos lácteos y derivados", EsPrincipal = true, IsActive = true, CreatedAt = DateTime.UtcNow },
                new Categoria { Id = 4, Nombre = "Panadería", Descripcion = "Pan y productos de repostería", EsPrincipal = true, IsActive = true, CreatedAt = DateTime.UtcNow },
                new Categoria { Id = 5, Nombre = "Limpieza", Descripcion = "Productos de limpieza y hogar", EsPrincipal = true, IsActive = true, CreatedAt = DateTime.UtcNow }
            );

            // Cliente por defecto
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente
                {
                    Id = 1,
                    Documento = "99999999",
                    NombreCompleto = "Consumidor Final",
                    TipoCliente = "Regular",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    FechaRegistro = DateTime.UtcNow
                }
            );
        }
    }
}