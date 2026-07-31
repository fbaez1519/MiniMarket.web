using AutoMapper;
using MiniMarket.web.DTOs;
using Minimarket.Domain.Entities;

namespace MiniMarket.web.Services;

public class ProductoService : IProductoService
{
    private static List<Producto> _productos = new List<Producto>();
    private static int _nextId = 1;
    private readonly IMapper _mapper;

    public ProductoService(IMapper mapper)
    {
        _mapper = mapper;

        // Datos de ejemplo
        if (!_productos.Any())
        {
            _productos.AddRange(new List<Producto>
            {
                new Producto
                {
                    Id = _nextId++,
                    Codigo = "PROD-001",
                    Nombre = "Leche Entera",
                    Descripcion = "Leche entera pasteurizada",
                    CategoriaId = 1,
                    PrecioCompra = 1.80m,
                    PrecioVenta = 2.50m,
                    Stock = 10,
                    StockMinimo = 5,
                    UnidadMedida = "Unidad",
                    Marca = "Rica",
                    Impuesto = 18,
                    FechaIngreso = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    EstaActivo = true
                },
                new Producto
                {
                    Id = _nextId++,
                    Codigo = "PROD-002",
                    Nombre = "Pan Integral",
                    Descripcion = "Pan integral artesanal",
                    CategoriaId = 2,
                    PrecioCompra = 0.80m,
                    PrecioVenta = 1.20m,
                    Stock = 15,
                    StockMinimo = 3,
                    UnidadMedida = "Unidad",
                    Marca = "Bimbo",
                    Impuesto = 18,
                    FechaIngreso = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    EstaActivo = true
                },
                new Producto
                {
                    Id = _nextId++,
                    Codigo = "PROD-003",
                    Nombre = "Huevos",
                    Descripcion = "Huevos de gallina",
                    CategoriaId = 1,
                    PrecioCompra = 2.20m,
                    PrecioVenta = 3.00m,
                    Stock = 20,
                    StockMinimo = 5,
                    UnidadMedida = "Unidad",
                    Marca = "La Campiña",
                    Impuesto = 18,
                    FechaIngreso = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    EstaActivo = true
                }
            });
        }
    }

    public async Task<IEnumerable<ProductoDTO>> GetAllAsync()
    {
        var productos = _productos.OrderByDescending(p => p.Id);
        return await Task.FromResult(_mapper.Map<IEnumerable<ProductoDTO>>(productos));
    }

    public async Task<ProductoDTO?> GetByIdAsync(int id)
    {
        var producto = _productos.FirstOrDefault(p => p.Id == id);
        return await Task.FromResult(_mapper.Map<ProductoDTO>(producto));
    }

    public async Task<ProductoDTO?> GetByCodigoAsync(string codigo)
    {
        var producto = _productos.FirstOrDefault(p => p.Codigo == codigo);
        return await Task.FromResult(_mapper.Map<ProductoDTO>(producto));
    }

    public async Task<ProductoDTO> CreateAsync(ProductoCreateDTO productoDto)
    {
        // Validar código único
        if (_productos.Any(p => p.Codigo == productoDto.Codigo))
            throw new Exception($"Ya existe un producto con el código {productoDto.Codigo}");

        var producto = _mapper.Map<Producto>(productoDto);
        producto.Id = _nextId++;
        producto.FechaIngreso = DateTime.UtcNow;
        producto.CreatedAt = DateTime.UtcNow;
        producto.UpdatedAt = DateTime.UtcNow;
        producto.EstaActivo = true;

        _productos.Add(producto);

        return await Task.FromResult(_mapper.Map<ProductoDTO>(producto));
    }

    public async Task<ProductoDTO> UpdateAsync(ProductoUpdateDTO productoDto)
    {
        var producto = _productos.FirstOrDefault(p => p.Id == productoDto.Id);
        if (producto == null)
            throw new Exception($"Producto con ID {productoDto.Id} no encontrado");

        // Validar código único (excepto el mismo producto)
        if (_productos.Any(p => p.Codigo == productoDto.Codigo && p.Id != productoDto.Id))
            throw new Exception($"Ya existe otro producto con el código {productoDto.Codigo}");

        // Actualizar propiedades
        producto.Codigo = productoDto.Codigo;
        producto.Nombre = productoDto.Nombre;
        producto.Descripcion = productoDto.Descripcion;
        producto.CategoriaId = productoDto.CategoriaId;
        producto.PrecioCompra = productoDto.PrecioCompra;
        producto.PrecioVenta = productoDto.PrecioVenta;
        producto.Stock = productoDto.Stock;
        producto.StockMinimo = productoDto.StockMinimo;
        producto.StockMaximo = productoDto.StockMaximo;
        producto.UnidadMedida = productoDto.UnidadMedida;
        producto.ImagenUrl = productoDto.ImagenUrl;
        producto.FechaVencimiento = productoDto.FechaVencimiento;
        producto.RequiereRefrigeracion = productoDto.RequiereRefrigeracion;
        producto.TemperaturaRecomendada = productoDto.TemperaturaRecomendada;
        producto.Peso = productoDto.Peso;
        producto.Marca = productoDto.Marca;
        producto.PaisOrigen = productoDto.PaisOrigen;
        producto.Impuesto = productoDto.Impuesto;
        producto.EstaActivo = productoDto.EstaActivo;
        producto.UpdatedAt = DateTime.UtcNow;

        return await Task.FromResult(_mapper.Map<ProductoDTO>(producto));
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var producto = _productos.FirstOrDefault(p => p.Id == id);
        if (producto == null)
            return await Task.FromResult(false);

        _productos.Remove(producto);
        return await Task.FromResult(true);
    }

    public async Task<bool> DesactivarAsync(int id)
    {
        var producto = _productos.FirstOrDefault(p => p.Id == id);
        if (producto == null)
            return await Task.FromResult(false);

        producto.EstaActivo = false;
        producto.UpdatedAt = DateTime.UtcNow;
        return await Task.FromResult(true);
    }

    public async Task<bool> ActivarAsync(int id)
    {
        var producto = _productos.FirstOrDefault(p => p.Id == id);
        if (producto == null)
            return await Task.FromResult(false);

        producto.EstaActivo = true;
        producto.UpdatedAt = DateTime.UtcNow;
        return await Task.FromResult(true);
    }
}