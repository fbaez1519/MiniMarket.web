namespace MiniMarket.web.DTOs;

public class UsuarioDTO
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; }  // ← Cambiar de "Nombre" a "NombreCompleto"
    public string Email { get; set; }
    public string Username { get; set; }        // ← Agregar Username
    public string Password { get; set; }        // ← Agregar Password (opcional)
    public string Rol { get; set; }
}