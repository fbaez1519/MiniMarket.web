using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MiniMarket.web.DTOs;
using MiniMarket.web.Services;

namespace MiniMarket.web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly IVentaService _service;
    private readonly IValidator<VentaDTO> _validator;

    public VentasController(IVentaService service, IValidator<VentaDTO> validator)
    {
        _service = service;
        _validator = validator;
    }

    // GET: api/ventas
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var ventas = await _service.GetAllAsync();
        return Ok(ventas);
    }

    // GET: api/ventas/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var venta = await _service.GetByIdAsync(id);
        if (venta == null)
            return NotFound();

        return Ok(venta);
    }

    // POST: api/ventas
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VentaDTO venta)
    {
        var result = await _validator.ValidateAsync(venta);
        if (!result.IsValid)
            return BadRequest(result.Errors);

        var nuevo = await _service.CreateAsync(venta);
        return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
    }

    // DELETE: api/ventas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _service.DeleteAsync(id);
        if (!eliminado)
            return NotFound();

        return NoContent();
    }
}