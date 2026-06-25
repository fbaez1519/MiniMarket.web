using Microsoft.AspNetCore.Mvc;
using MiniMarket.web.DTOs;
using MiniMarket.web.Helpers;
using MiniMarket.web.Services;

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
        var usuario = await _usuarioService.GetByEmailAsync(login.Email);
        
        if (usuario == null)
            return Unauthorized("Credenciales inválidas");

        var token = JwtHelper.GenerateToken(usuario, _configuration["Jwt:Key"]!);

        return Ok(new LoginResponseDTO
        {
            Token = token,
            Usuario = usuario.Nombre,
            Rol = usuario.Rol
        });
    }
}