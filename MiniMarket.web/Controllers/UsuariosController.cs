using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MiniMarket.web.DTOs;
using MiniMarket.web.Services;

namespace MiniMarket.web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _service;
    private readonly IValidator<UsuarioDTO> _validator;
    private readonly IMapper _mapper;

    public UsuariosController(IUsuarioService service, IValidator<UsuarioDTO> validator, IMapper mapper)
    {
        _service = service;
        _validator = validator;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtiene todos los usuarios
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _service.GetAllAsync();
        return Ok(usuarios);
    }

    /// <summary>
    /// Obtiene un usuario por su ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var usuario = await _service.GetByIdAsync(id);
        if (usuario == null)
            return NotFound(new { mensaje = $"Usuario con ID {id} no encontrado" });

        return Ok(usuario);
    }

    /// <summary>
    /// Crea un nuevo usuario
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UsuarioDTO usuario)
    {
        var result = await _validator.ValidateAsync(usuario);
        if (!result.IsValid)
            return BadRequest(result.Errors);

        var nuevo = await _service.CreateAsync(usuario);
        return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
    }

    /// <summary>
    /// Actualiza un usuario existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UsuarioDTO usuario)
    {
        if (id != usuario.Id)
            return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del objeto" });

        var result = await _validator.ValidateAsync(usuario);
        if (!result.IsValid)
            return BadRequest(result.Errors);

        var actualizado = await _service.UpdateAsync(id, usuario);
        if (actualizado == null)
            return NotFound(new { mensaje = $"Usuario con ID {id} no encontrado" });

        return Ok(actualizado);
    }

    /// <summary>
    /// Elimina un usuario
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _service.DeleteAsync(id);
        if (!eliminado)
            return NotFound(new { mensaje = $"Usuario con ID {id} no encontrado" });

        return NoContent();
    }
}