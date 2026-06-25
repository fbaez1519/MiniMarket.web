namespace MiniMarket.web.DTOs;

public class UsuarioDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Email { get; set; }
    public string Rol { get; set; }
    // NO incluir Password
}