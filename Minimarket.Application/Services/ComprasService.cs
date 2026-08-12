using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Minimarket.Domain.Entities;
using Minimarket.Infrastructure.Data;
using Minimarket.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minimarket.Application.Services
{
    public class ComprasService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ComprasService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CompraDTO> CreateAsync(CompraCreateDTO compraDto)
        {
            var compra = new Compra
            {
                NumeroFactura = GenerarNumeroFactura(),
                FechaCompra = DateTime.UtcNow,
                ProveedorId = compraDto.ProveedorId,
                UsuarioId = 1,
                Estado = "Pendiente",
                Observaciones = compraDto.Observaciones,
                GuiaRemision = compraDto.GuiaRemision,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            foreach (var detalleDto in compraDto.Detalles)
            {
                var producto = await _context.Productos.FindAsync(detalleDto.ProductoId);
                if (producto == null)
                    throw new Exception($"Producto con ID {detalleDto.ProductoId} no encontrado");

                var detalle = new DetalleCompra
                {
                    ProductoId = detalleDto.ProductoId,
                    Cantidad = detalleDto.Cantidad,
                    PrecioUnitario = detalleDto.PrecioUnitario,
                    Subtotal = detalleDto.Cantidad * detalleDto.PrecioUnitario,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                compra.Detalles.Add(detalle);
            }

            compra.Subtotal = compra.Detalles.Sum(d => d.Subtotal);
            compra.Impuesto = compra.Subtotal * 0.18m;
            compra.Total = compra.Subtotal + compra.Impuesto;

            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();

            return _mapper.Map<CompraDTO>(compra);
        }

        public async Task<CompraDTO> GetByIdAsync(int id)
        {
            var compra = await _context.Compras
                .Include(c => c.Proveedor)
                .Include(c => c.Usuario)
                .Include(c => c.Detalles)
                .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(c => c.Id == id);

            return compra == null ? null : _mapper.Map<CompraDTO>(compra);
        }

        private string GenerarNumeroFactura()
        {
            var fecha = DateTime.Now;
            var consecutivo = _context.Compras.Count() + 1;
            return $"COM-{fecha:yyyyMMdd}-{consecutivo:D4}";
        }
    }
}
