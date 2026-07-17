using Microsoft.AspNetCore.Mvc;
using MiniMarket.web.DTOs;
using MiniMarket.web.Helpers;
using MiniMarket.web.Services;
using Minimarket.Domain.Entities;

namespace MiniMarket.web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly IConfiguration _configuration;

    public AuthController(IUsuarioService usuarioService, IConfiguration configuration)
    {
        _usuarioService = usuarioService;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO login)
    {
        // Buscar por Email o Username
        var usuario = await _usuarioService.GetByEmailAsync(login.Email);
        if (usuario == null)
        {
            usuario = await _usuarioService.GetByUsernameAsync(login.Email);
        }

        if (usuario == null)
            return Unauthorized(new { message = "Credenciales inválidas" });

        // Verificar contraseña (en producción usar hash)
        if (usuario.PasswordHash != login.Password)
            return Unauthorized(new { message = "Credenciales inválidas" });

        // Actualizar último acceso
        usuario.ActualizarUltimoAcceso();

        var token = JwtHelper.GenerateToken(usuario, _configuration["Jwt:Key"]!);

        return Ok(new LoginResponseDTO
        {
            Token = token,
            Usuario = usuario.NombreCompleto,
            Rol = usuario.Rol
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO register)
    {
        // Validar que las contraseñas coincidan
        if (register.Password != register.ConfirmPassword)
            return BadRequest(new { message = "Las contraseñas no coinciden" });

        // Verificar si el usuario ya existe por Email
        var existingUser = await _usuarioService.GetByEmailAsync(register.Email);
        if (existingUser != null)
            return BadRequest(new { message = "El correo electrónico ya está registrado" });

        // Verificar si el usuario ya existe por Username
        existingUser = await _usuarioService.GetByUsernameAsync(register.Username);
        if (existingUser != null)
            return BadRequest(new { message = "El nombre de usuario ya está registrado" });

        // Crear el nuevo usuario
        var usuario = new Usuario
        {
            NombreCompleto = register.NombreCompleto,
            Email = register.Email,
            Username = register.Username ?? register.Email.Split('@')[0],
            PasswordHash = register.Password, // En producción usar hash
            Rol = "Vendedor",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var result = await _usuarioService.RegisterAsync(usuario);

        if (!result)
            return BadRequest(new { message = "Error al registrar el usuario" });

        return Ok(new { message = "Usuario registrado exitosamente" });
    }
}