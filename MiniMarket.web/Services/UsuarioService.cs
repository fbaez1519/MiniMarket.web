using MiniMarket.web.DTOs;

namespace MiniMarket.web.Services;

public class UsuarioService : IUsuarioService
{
    // Base de datos en memoria (temporal)
    private static List<UsuarioDTO> _usuarios = new()
    {
        new UsuarioDTO { Id = 1, Nombre = "Admin", Email = "admin@minimarket.com", Rol = "Administrador" },
        new UsuarioDTO { Id = 2, Nombre = "Juan Pérez", Email = "juan@minimarket.com", Rol = "Vendedor" },
        new UsuarioDTO { Id = 3, Nombre = "María Gómez", Email = "maria@minimarket.com", Rol = "Cliente" }
    };
    private static int _nextId = 4;

    public Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        => Task.FromResult(_usuarios.AsEnumerable());

    public Task<UsuarioDTO?> GetByIdAsync(int id)
        => Task.FromResult(_usuarios.FirstOrDefault(u => u.Id == id));

    public Task<UsuarioDTO> CreateAsync(UsuarioDTO usuario)
    {
        usuario.Id = _nextId++;
        _usuarios.Add(usuario);
        return Task.FromResult(usuario);
    }

    public Task<UsuarioDTO?> UpdateAsync(int id, UsuarioDTO usuario)
    {
        var existing = _usuarios.FirstOrDefault(u => u.Id == id);
        if (existing != null)
        {
            existing.Nombre = usuario.Nombre;
            existing.Email = usuario.Email;
            existing.Rol = usuario.Rol;
        }
        return Task.FromResult(existing);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
        if (usuario != null)
        {
            _usuarios.Remove(usuario);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<UsuarioDTO?> GetByEmailAsync(string email)
        => Task.FromResult(_usuarios.FirstOrDefault(u => u.Email == email));
}