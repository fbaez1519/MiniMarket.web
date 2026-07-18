using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMarket.web.DTOs;
using MiniMarket.web.Services;

namespace MiniMarket.web.Controllers;

/// <summary>
/// Controlador de API para la gestión de usuarios
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]  // ✅ CAMBIADO: Solo requiere autenticación, no verifica rol
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _service;

    public UsuariosController(IUsuarioService service)
    {
        _service = service;
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
    /// Obtiene un usuario por su email
    /// </summary>
    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var usuario = await _service.GetByEmailAsync(email);
        if (usuario == null)
            return NotFound(new { mensaje = $"Usuario con email {email} no encontrado" });

        return Ok(usuario);
    }

    /// <summary>
    /// Obtiene un usuario por su username
    /// </summary>
    [HttpGet("username/{username}")]
    public async Task<IActionResult> GetByUsername(string username)
    {
        var usuario = await _service.GetByUsernameAsync(username);
        if (usuario == null)
            return NotFound(new { mensaje = $"Usuario con username {username} no encontrado" });

        return Ok(usuario);
    }

    /// <summary>
    /// Crea un nuevo usuario
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UsuarioCreateDTO usuarioDto)
    {
        try
        {
            var nuevo = await _service.CreateAsync(usuarioDto);
            return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    /// <summary>
    /// Actualiza un usuario existente
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UsuarioUpdateDTO usuarioDto)
    {
        try
        {
            var actualizado = await _service.UpdateAsync(usuarioDto);
            return Ok(actualizado);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    /// <summary>
    /// Elimina un usuario
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var eliminado = await _service.DeleteAsync(id);
            if (!eliminado)
                return NotFound(new { mensaje = $"Usuario con ID {id} no encontrado" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    /// <summary>
    /// Desactiva un usuario
    /// </summary>
    [HttpPatch("{id}/desactivar")]
    public async Task<IActionResult> Desactivar(int id)
    {
        var desactivado = await _service.DesactivarAsync(id);
        if (!desactivado)
            return NotFound(new { mensaje = $"Usuario con ID {id} no encontrado" });

        return Ok(new { mensaje = "Usuario desactivado exitosamente" });
    }

    /// <summary>
    /// Activa un usuario
    /// </summary>
    [HttpPatch("{id}/activar")]
    public async Task<IActionResult> Activar(int id)
    {
        var activado = await _service.ActivarAsync(id);
        if (!activado)
            return NotFound(new { mensaje = $"Usuario con ID {id} no encontrado" });

        return Ok(new { mensaje = "Usuario activado exitosamente" });
    }

    /// <summary>
    /// Cambia la contraseña de un usuario
    /// </summary>
    [HttpPatch("{id}/cambiar-password")]
    public async Task<IActionResult> CambiarPassword(int id, [FromBody] CambiarPasswordDTO dto)
    {
        try
        {
            var result = await _service.CambiarPasswordAsync(id, dto.NuevaPassword);
            if (!result)
                return BadRequest(new { mensaje = "Error al cambiar la contraseña" });

            return Ok(new { mensaje = "Contraseña cambiada exitosamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}