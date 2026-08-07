using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMarket.web.DTOs;
using MiniMarket.web.Services;

namespace MiniMarket.web.Controllers;

/// <summary>
/// Controlador de API para la gestión de ventas
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VentasController : ControllerBase
{
    private readonly IVentaService _service;

    public VentasController(IVentaService service)
    {
        _service = service;
    }

    // ============================================================
    // MÉTODOS DE CONSULTA
    // ============================================================

    /// <summary>
    /// Obtiene todas las ventas
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var ventas = await _service.GetAllAsync();
        return Ok(ventas);
    }

    /// <summary>
    /// Obtiene una venta por su ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var venta = await _service.GetByIdAsync(id);
        if (venta == null)
            return NotFound(new { mensaje = $"Venta con ID {id} no encontrada" });

        return Ok(venta);
    }

    /// <summary>
    /// Obtiene ventas por número de factura
    /// </summary>
    [HttpGet("factura/{numeroFactura}")]
    public async Task<IActionResult> GetByNumeroFactura(string numeroFactura)
    {
        var venta = await _service.GetByNumeroFacturaAsync(numeroFactura);
        if (venta == null)
            return NotFound(new { mensaje = $"Venta con factura {numeroFactura} no encontrada" });

        return Ok(venta);
    }

    /// <summary>
    /// Obtiene ventas por rango de fechas
    /// </summary>
    [HttpGet("fecha")]
    public async Task<IActionResult> GetByFecha([FromQuery] DateTime inicio, [FromQuery] DateTime fin)
    {
        var ventas = await _service.GetByFechaAsync(inicio, fin);
        return Ok(ventas);
    }

    /// <summary>
    /// Obtiene ventas de un cliente específico
    /// </summary>
    [HttpGet("cliente/{clienteId}")]
    public async Task<IActionResult> GetByCliente(int clienteId)
    {
        var ventas = await _service.GetByClienteAsync(clienteId);
        return Ok(ventas);
    }

    /// <summary>
    /// Obtiene ventas de un usuario específico
    /// </summary>
    [HttpGet("usuario/{usuarioId}")]
    public async Task<IActionResult> GetByUsuario(int usuarioId)
    {
        var ventas = await _service.GetByUsuarioAsync(usuarioId);
        return Ok(ventas);
    }

    // ============================================================
    // MÉTODOS DE CREACIÓN Y ACTUALIZACIÓN
    // ============================================================

    /// <summary>
    /// Crea una nueva venta
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VentaCreateDTO ventaDto)
    {
        try
        {
            var nueva = await _service.CreateAsync(ventaDto);
            return CreatedAtAction(nameof(GetById), new { id = nueva.Id }, nueva);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    /// <summary>
    /// Anula una venta
    /// </summary>
    [HttpPatch("{id}/anular")]
    public async Task<IActionResult> Anular(int id, [FromBody] VentaAnularDTO anularDto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(anularDto.MotivoAnulacion))
                return BadRequest(new { mensaje = "Debe especificar un motivo de anulación" });

            var anulada = await _service.AnularAsync(id, anularDto.MotivoAnulacion);
            return Ok(new { 
                mensaje = "Venta anulada exitosamente", 
                venta = anulada 
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    /// <summary>
    /// Actualiza una venta
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] VentaUpdateDTO ventaDto)
    {
        try
        {
            var actualizada = await _service.UpdateAsync(ventaDto);
            return Ok(actualizada);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    // ============================================================
    // MÉTODOS DE ELIMINACIÓN
    // ============================================================

    /// <summary>
    /// Elimina una venta (borrado físico)
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _service.DeleteAsync(id);
        if (!eliminado)
            return NotFound(new { mensaje = $"Venta con ID {id} no encontrada" });

        return NoContent();
    }
}