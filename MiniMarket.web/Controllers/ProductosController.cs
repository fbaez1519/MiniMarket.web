using Microsoft.AspNetCore.Mvc;
using MiniMarket.web.DTOs;
using MiniMarket.web.Services;

namespace MiniMarket.web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _service;

    public ProductosController(IProductoService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtiene todos los productos
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var productos = await _service.GetAllAsync();
        return Ok(productos);
    }

    /// <summary>
    /// Obtiene un producto por su ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var producto = await _service.GetByIdAsync(id);
        if (producto == null)
            return NotFound(new { mensaje = $"Producto con ID {id} no encontrado" });

        return Ok(producto);
    }

    /// <summary>
    /// Obtiene un producto por su código
    /// </summary>
    [HttpGet("codigo/{codigo}")]
    public async Task<IActionResult> GetByCodigo(string codigo)
    {
        var producto = await _service.GetByCodigoAsync(codigo);
        if (producto == null)
            return NotFound(new { mensaje = $"Producto con código {codigo} no encontrado" });

        return Ok(producto);
    }

    /// <summary>
    /// Crea un nuevo producto
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductoCreateDTO productoDto)
    {
        try
        {
            var nuevo = await _service.CreateAsync(productoDto);
            return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    /// <summary>
    /// Actualiza un producto existente
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProductoUpdateDTO productoDto)
    {
        try
        {
            var actualizado = await _service.UpdateAsync(productoDto);
            return Ok(actualizado);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    /// <summary>
    /// Elimina un producto
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _service.DeleteAsync(id);
        if (!eliminado)
            return NotFound(new { mensaje = $"Producto con ID {id} no encontrado" });

        return NoContent();
    }

    /// <summary>
    /// Desactiva un producto
    /// </summary>
    [HttpPatch("{id}/desactivar")]
    public async Task<IActionResult> Desactivar(int id)
    {
        var desactivado = await _service.DesactivarAsync(id);
        if (!desactivado)
            return NotFound(new { mensaje = $"Producto con ID {id} no encontrado" });

        return Ok(new { mensaje = "Producto desactivado exitosamente" });
    }

    /// <summary>
    /// Activa un producto
    /// </summary>
    [HttpPatch("{id}/activar")]
    public async Task<IActionResult> Activar(int id)
    {
        var activado = await _service.ActivarAsync(id);
        if (!activado)
            return NotFound(new { mensaje = $"Producto con ID {id} no encontrado" });

        return Ok(new { mensaje = "Producto activado exitosamente" });
    }
}