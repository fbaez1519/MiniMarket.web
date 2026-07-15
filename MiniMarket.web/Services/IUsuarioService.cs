using Minimarket.Domain.Entities;
using MiniMarket.web.DTOs;

namespace MiniMarket.web.Services;

public interface IUsuarioService
{
    Task<Usuario> GetByIdAsync(int id);
    Task<Usuario> GetByEmailAsync(string email);
    Task<Usuario> GetByUsernameAsync(string username);
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<bool> RegisterAsync(Usuario usuario);

    // Métodos adicionales para el CRUD
    Task<Usuario> CreateAsync(UsuarioDTO usuarioDto);  // ← Usa UsuarioDTO
    Task<Usuario> UpdateAsync(int id, UsuarioDTO usuarioDto);  // ← Usa UsuarioDTO
    Task<bool> DeleteAsync(int id);
}