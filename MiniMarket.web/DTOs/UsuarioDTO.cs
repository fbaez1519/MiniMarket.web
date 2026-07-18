namespace MiniMarket.web.DTOs;

public class UsuarioDTO
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
}

/// <summary>
/// DTO para CREAR un nuevo usuario (desde el panel de admin)
/// </summary>
public class UsuarioCreateDTO
{
    public string NombreCompleto { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Rol { get; set; } = "Vendedor";
}

/// <summary>
/// DTO para ACTUALIZAR un usuario (desde el panel de admin)
/// </summary>
public class UsuarioUpdateDTO
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Rol { get; set; } = "Vendedor";
    public bool EstaActivo { get; set; } = true;
}

/// <summary>
/// DTO para cambiar la contraseña de un usuario
/// </summary>
public class CambiarPasswordDTO
{
    public string NuevaPassword { get; set; } = string.Empty;
}