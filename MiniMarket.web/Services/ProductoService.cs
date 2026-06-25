using MiniMarket.web.DTOs;

namespace MiniMarket.web.Services;

public class ProductoService : IProductoService
{
    // Base de datos en memoria (temporal)
    private static List<ProductoDTO> _productos = new()
    {
        new ProductoDTO { Id = 1, Nombre = "Leche", Precio = 2.50m, Stock = 10, Categoria = "Lácteos" },
        new ProductoDTO { Id = 2, Nombre = "Pan", Precio = 1.20m, Stock = 15, Categoria = "Panadería" },
        new ProductoDTO { Id = 3, Nombre = "Huevos", Precio = 3.00m, Stock = 20, Categoria = "Lácteos" },
        new ProductoDTO { Id = 4, Nombre = "Arroz", Precio = 1.80m, Stock = 8, Categoria = "Granos" },
        new ProductoDTO { Id = 5, Nombre = "Aceite", Precio = 4.50m, Stock = 5, Categoria = "Aceites" }
    };
    private static int _nextId = 6;

    // Obtener todos los productos
    public Task<IEnumerable<ProductoDTO>> GetAllAsync()
        => Task.FromResult(_productos.AsEnumerable());

    // Obtener un producto por ID
    public Task<ProductoDTO?> GetByIdAsync(int id)
        => Task.FromResult(_productos.FirstOrDefault(p => p.Id == id));

    // Crear un nuevo producto
    public Task<ProductoDTO> CreateAsync(ProductoDTO producto)
    {
        producto.Id = _nextId++;
        _productos.Add(producto);
        return Task.FromResult(producto);
    }

    // Actualizar un producto existente
    public Task<ProductoDTO?> UpdateAsync(int id, ProductoDTO producto)
    {
        var existing = _productos.FirstOrDefault(p => p.Id == id);
        if (existing != null)
        {
            existing.Nombre = producto.Nombre;
            existing.Precio = producto.Precio;
            existing.Stock = producto.Stock;
            existing.Categoria = producto.Categoria;
        }
        return Task.FromResult(existing);
    }

    // Eliminar un producto
    public Task<bool> DeleteAsync(int id)
    {
        var producto = _productos.FirstOrDefault(p => p.Id == id);
        if (producto != null)
        {
            _productos.Remove(producto);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}