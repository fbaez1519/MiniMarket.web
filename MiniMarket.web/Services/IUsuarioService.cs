using MiniMarket.web.DTOs;

namespace MiniMarket.web.Services;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioDTO>> GetAllAsync();
    Task<UsuarioDTO?> GetByIdAsync(int id);
    Task<UsuarioDTO> CreateAsync(UsuarioDTO usuario);
    Task<UsuarioDTO?> UpdateAsync(int id, UsuarioDTO usuario);
    Task<bool> DeleteAsync(int id);
    Task<UsuarioDTO?> GetByEmailAsync(string email);
}