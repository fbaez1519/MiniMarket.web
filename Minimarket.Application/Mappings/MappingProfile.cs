using AutoMapper;
using Minimarket.Domain.Entities;
using MiniMarket.web.DTOs;

namespace MiniMarket.web.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ============================================================
        // PRODUCTO
        // ============================================================
        CreateMap<Producto, ProductoDTO>().ReverseMap();
        CreateMap<Producto, ProductoCreateDTO>().ReverseMap();
        CreateMap<Producto, ProductoUpdateDTO>().ReverseMap();
        CreateMap<ProductoCreateDTO, Producto>();
        CreateMap<ProductoUpdateDTO, Producto>();

        // ============================================================
        // USUARIO
        // ============================================================
        CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        CreateMap<Usuario, UsuarioCreateDTO>().ReverseMap();
        CreateMap<Usuario, UsuarioUpdateDTO>().ReverseMap();
        CreateMap<UsuarioCreateDTO, Usuario>();
        CreateMap<UsuarioUpdateDTO, Usuario>();

        // ============================================================
        // VENTA - CORREGIDO
        // ============================================================
        CreateMap<Venta, VentaDTO>()
            .ForMember(dest => dest.ClienteNombre, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.NombreCompleto : null))
            .ForMember(dest => dest.UsuarioNombre, opt => opt.MapFrom(src => src.Usuario != null ? src.Usuario.NombreCompleto : null))
            .ForMember(dest => dest.CantidadProductos, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<Venta, VentaCreateDTO>().ReverseMap();
        CreateMap<Venta, VentaUpdateDTO>().ReverseMap();
        CreateMap<Venta, VentaAnularDTO>().ReverseMap();
        CreateMap<VentaCreateDTO, Venta>();
        CreateMap<VentaUpdateDTO, Venta>();

        // ============================================================
        // DETALLE VENTA - CORREGIDO
        // ============================================================
        CreateMap<DetalleVenta, DetalleVentaDTO>()
            .ForMember(dest => dest.ProductoNombre, opt => opt.MapFrom(src => src.Producto != null ? src.Producto.Nombre : null))
            .ReverseMap();

        CreateMap<DetalleVenta, DetalleVentaCreateDTO>().ReverseMap();
        CreateMap<DetalleVentaCreateDTO, DetalleVenta>();

        // ============================================================
        // CLIENTE
        // ============================================================
        CreateMap<Cliente, ClienteDTO>().ReverseMap();
        CreateMap<Cliente, ClienteCreateDTO>().ReverseMap();
        CreateMap<Cliente, ClienteUpdateDTO>().ReverseMap();
        CreateMap<ClienteCreateDTO, Cliente>();
        CreateMap<ClienteUpdateDTO, Cliente>();

        // ============================================================
        // CATEGORÍA
        // ============================================================
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        CreateMap<Categoria, CategoriaCreateDTO>().ReverseMap();
        CreateMap<Categoria, CategoriaUpdateDTO>().ReverseMap();
        CreateMap<CategoriaCreateDTO, Categoria>();
        CreateMap<CategoriaUpdateDTO, Categoria>();
    }
}