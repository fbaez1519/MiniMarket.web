using AutoMapper;
using Minimarket.Domain.Entities;
using MiniMarket.web.DTOs;

namespace MiniMarket.web.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Producto
        CreateMap<Producto, ProductoDTO>().ReverseMap();
        CreateMap<Producto, ProductoCreateDTO>().ReverseMap();
        CreateMap<Producto, ProductoUpdateDTO>().ReverseMap();
        CreateMap<ProductoCreateDTO, Producto>();
        CreateMap<ProductoUpdateDTO, Producto>();

        // Usuario 
        CreateMap<Usuario, UsuarioDTO>().ReverseMap();

        // Venta
        CreateMap<Venta, VentaDTO>().ReverseMap();
        //CreateMap<VentaDetalle, VentaDetalleDTO>().ReverseMap();

        // Cliente
        CreateMap<Cliente, ClienteDTO>().ReverseMap();
        CreateMap<Cliente, ClienteCreateDTO>().ReverseMap();
        CreateMap<Cliente, ClienteUpdateDTO>().ReverseMap();
        CreateMap<ClienteCreateDTO, Cliente>();
        CreateMap<ClienteUpdateDTO, Cliente>();

        // ✅ Categoría 
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        CreateMap<Categoria, CategoriaCreateDTO>().ReverseMap();
        CreateMap<Categoria, CategoriaUpdateDTO>().ReverseMap();
        CreateMap<CategoriaCreateDTO, Categoria>();
        CreateMap<CategoriaUpdateDTO, Categoria>();
    }
}