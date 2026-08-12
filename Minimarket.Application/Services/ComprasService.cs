using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Minimarket.Domain.DTOs.Compra;
using Minimarket.Domain.Entities;
using Minimarket.Domain.Enums;
using Minimarket.Domain.Exceptions;
using Minimarket.Domain.Interfaces;

namespace Minimarket.Infrastructure.Services
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _compraRepository;
        private readonly IProductoRepository _productoRepository;

        public CompraService(ICompraRepository compraRepository, IProductoRepository productoRepository)
        {
            _compraRepository = compraRepository;
            _productoRepository = productoRepository;
        }

        public async Task<CompraDto> CrearCompraAsync(CrearCompraDto compraDto)
        {
            var compra = new Compra
            {
                ProveedorId = compraDto.ProveedorId,
                UsuarioId = compraDto.UsuarioId,
                Fecha = DateTime.Now,
                Estado = EstadoCompra.Recibida,
                Observaciones = compraDto.Observaciones,
                Detalles = new List<DetalleCompra>()
            };

            foreach (var detalleDto in compraDto.Detalles)
            {
                var producto = await _productoRepository.GetProductoByIdAsync(detalleDto.ProductoId);
                if (producto == null)
                    throw new EntidadNoEncontradaException("Producto", detalleDto.ProductoId);

                var detalle = new DetalleCompra
                {
                    ProductoId = detalleDto.ProductoId,
                    Cantidad = detalleDto.Cantidad,
                    PrecioUnitario = detalleDto.PrecioUnitario
                };

                compra.Detalles.Add(detalle);
            }

            compra.CalcularTotal();

            var compraCreada = await _compraRepository.CrearCompraAsync(compra);
            return MapToCompraDto(compraCreada);
        }

        public async Task<CompraDto> GetCompraByIdAsync(int id)
        {
            var compra = await _compraRepository.GetCompraByIdAsync(id);
            if (compra == null)
                throw new EntidadNoEncontradaException("Compra", id);

            return MapToCompraDto(compra);
        }

        public async Task<List<CompraDto>> GetComprasAsync(FiltroCompraDto filtro)
        {
            var compras = await _compraRepository.GetComprasAsync(filtro);
            return compras.Select(MapToCompraDto).ToList();
        }

        public async Task<bool> AnularCompraAsync(int id)
        {
            var compra = await _compraRepository.GetCompraByIdAsync(id);
            if (compra == null)
                throw new EntidadNoEncontradaException("Compra", id);

            if (compra.Estado == EstadoCompra.Cancelada)
                throw new InvalidOperationException("La compra ya está anulada");

            return await _compraRepository.AnularCompraAsync(id);
        }

        public async Task<decimal> GetTotalComprasDelMesAsync()
        {
            return await _compraRepository.GetTotalComprasDelMesAsync();
        }

        private CompraDto MapToCompraDto(Compra compra)
        {
            return new CompraDto
            {
                Id = compra.Id,
                NumeroCompra = compra.NumeroCompra,
                Fecha = compra.Fecha,
                ProveedorId = compra.ProveedorId,
                ProveedorNombre = compra.Proveedor?.Nombre ?? "Proveedor No Especificado",
                ProveedorRuc = compra.Proveedor?.Ruc,
                Total = compra.Total,
                Estado = compra.Estado,
                Observaciones = compra.Observaciones,
                Detalles = compra.Detalles?.Select(d => new DetalleCompraDto
                {
                    ProductoId = d.ProductoId,
                    ProductoNombre = d.Producto?.Nombre ?? "Producto Eliminado",
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList() ?? new List<DetalleCompraDto>()
            };
        }
    }
}