using Minimarket.Domain.Entities;
using MiniMarket.web.DTOs;

namespace MiniMarket.web.Services;

public interface IUsuarioService
{
    // ============================================
    // MÉTODOS DE CONSULTA
    // ============================================
    Task<Usuario> GetByIdAsync(int id);
    Task<Usuario> GetByEmailAsync(string email);
    Task<Usuario> GetByUsernameAsync(string username);
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<bool> RegisterAsync(Usuario usuario);

    // ============================================
    // MÉTODOS CRUD PARA ADMINISTRADORES
    // ============================================
    Task<Usuario> CreateAsync(UsuarioCreateDTO usuarioDto);
    Task<Usuario> UpdateAsync(UsuarioUpdateDTO usuarioDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DesactivarAsync(int id);
    Task<bool> ActivarAsync(int id);
    Task<bool> CambiarPasswordAsync(int id, string nuevaPassword);
}