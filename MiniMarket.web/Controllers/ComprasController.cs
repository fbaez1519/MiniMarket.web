// ============================================================
// ComprasController - TEMPORALMENTE DESHABILITADO
// ============================================================
// Este controlador no compila porque depende de clases que aún
// no existen (o tienen otro nombre/namespace) en el proyecto:
//
//   - Minimarket.Application.DTOs.Compra.CrearCompraDto
//        -> Existe "CompraCreateDTO" en Minimarket.Application.DTOs (namespace y nombre distintos)
//   - Minimarket.Application.DTOs.Compra.FiltroCompraDto
//        -> No existe todavía, hay que crearlo
//   - Minimarket.Application.Interfaces.ICompraService
//        -> Falta confirmar/crear
//   - Minimarket.Application.Exceptions.EntidadNoEncontradaException
//        -> Falta confirmar/crear
//
// Cuando se implemente el módulo completo de Compras
// (servicio, interfaz, DTOs y excepción), este archivo se
// puede restaurar quitando el comentario de bloque de abajo.
// ============================================================

namespace MiniMarket.Web.Controllers
{
    // Controlador deshabilitado temporalmente - ver notas arriba.
}

/*
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Minimarket.Application.DTOs.Compra;
using Minimarket.Application.Exceptions;
using Minimarket.Application.Interfaces;

namespace MiniMarket.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ComprasController : ControllerBase
    {
        private readonly ICompraService _compraService;
        private readonly ILogger<ComprasController> _logger;

        public ComprasController(ICompraService compraService, ILogger<ComprasController> logger)
        {
            _compraService = compraService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCompra([FromBody] CrearCompraDto compraDto)
        {
            try
            {
                var compra = await _compraService.CrearCompraAsync(compraDto);
                return CreatedAtAction(nameof(GetCompraById), new { id = compra.Id }, compra);
            }
            catch (EntidadNoEncontradaException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear compra");
                return StatusCode(500, new { error = "Error interno al procesar la compra" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompraById(int id)
        {
            try
            {
                var compra = await _compraService.GetCompraByIdAsync(id);
                return Ok(compra);
            }
            catch (EntidadNoEncontradaException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener compra {id}");
                return StatusCode(500, new { error = "Error interno al obtener la compra" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCompras([FromQuery] FiltroCompraDto filtro)
        {
            try
            {
                var compras = await _compraService.GetComprasAsync(filtro);
                return Ok(new
                {
                    total = compras.Count,
                    data = compras
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historial de compras");
                return StatusCode(500, new { error = "Error interno al obtener el historial" });
            }
        }

        [HttpPut("{id}/anular")]
        public async Task<IActionResult> AnularCompra(int id)
        {
            try
            {
                var resultado = await _compraService.AnularCompraAsync(id);

                if (!resultado)
                    return NotFound(new { error = "Compra no encontrada" });

                return Ok(new { message = "Compra anulada exitosamente" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (EntidadNoEncontradaException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al anular compra {id}");
                return StatusCode(500, new { error = "Error interno al anular la compra" });
            }
        }

        [HttpGet("total-mes")]
        public async Task<IActionResult> GetTotalComprasDelMes()
        {
            try
            {
                var total = await _compraService.GetTotalComprasDelMesAsync();
                return Ok(new { total, mes = DateTime.Now.ToString("MMMM yyyy") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener total de compras del mes");
                return StatusCode(500, new { error = "Error interno al obtener el total" });
            }
        }
    }
}
*/