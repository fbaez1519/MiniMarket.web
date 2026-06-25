using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MiniMarket.web.DTOs;
using MiniMarket.web.Services;

namespace MiniMarket.web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _service;
    private readonly IValidator<ProductoDTO> _validator;

    public ProductosController(IProductoService service, IValidator<ProductoDTO> validator)
    {
        _service = service;
        _validator = validator;
    }

    // GET: api/productos
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var productos = await _service.GetAllAsync();
        return Ok(productos);
    }

    // GET: api/productos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var producto = await _service.GetByIdAsync(id);
        if (producto == null)
            return NotFound();

        return Ok(producto);
    }

    // POST: api/productos
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductoDTO producto)
    {
        var result = await _validator.ValidateAsync(producto);
        if (!result.IsValid)
            return BadRequest(result.Errors);

        var nuevo = await _service.CreateAsync(producto);
        return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
    }

    // PUT: api/productos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductoDTO producto)
    {
        var result = await _validator.ValidateAsync(producto);
        if (!result.IsValid)
            return BadRequest(result.Errors);

        var actualizado = await _service.UpdateAsync(id, producto);
        if (actualizado == null)
            return NotFound();

        return Ok(actualizado);
    }

    // DELETE: api/productos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _service.DeleteAsync(id);
        if (!eliminado)
            return NotFound();

        return NoContent();
    }
}