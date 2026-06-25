using AutoMapper;
using MiniMarket.web.DTOs;

namespace MiniMarket.web.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Producto
        // CreateMap<Producto, ProductoDTO>().ReverseMap();
        
        // Usuario
        // CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        
        // Venta
        // CreateMap<Venta, VentaDTO>().ReverseMap();
        // CreateMap<VentaDetalle, VentaDetalleDTO>().ReverseMap();
    }
}