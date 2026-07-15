using Minimarket.Domain.Entities;
using MiniMarket.web.DTOs;

namespace MiniMarket.web.Services;

public class UsuarioService : IUsuarioService
{
    private static List<Usuario> _usuarios = new List<Usuario>
    {
        new Usuario
        {
            Id = 1,
            NombreCompleto = "Admin",
            Email = "admin@minimarket.com",
            Username = "admin",
            PasswordHash = "admin123",
            Rol = "Administrador",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        },
        new Usuario
        {
            Id = 2,
            NombreCompleto = "Vendedor Demo",
            Email = "vendedor@minimarket.com",
            Username = "vendedor",
            PasswordHash = "123456",
            Rol = "Vendedor",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }
    };
    private static int _nextId = 3;

    // ============================================
    // MÉTODOS DE CONSULTA
    // ============================================

    public async Task<Usuario> GetByIdAsync(int id)
    {
        return await Task.FromResult(_usuarios.FirstOrDefault(u => u.Id == id));
    }

    public async Task<Usuario> GetByEmailAsync(string email)
    {
        return await Task.FromResult(_usuarios.FirstOrDefault(u => u.Email.ToLower() == email.ToLower()));
    }

    public async Task<Usuario> GetByUsernameAsync(string username)
    {
        return await Task.FromResult(_usuarios.FirstOrDefault(u => u.Username.ToLower() == username.ToLower()));
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await Task.FromResult(_usuarios.AsEnumerable());
    }

    // ============================================
    // MÉTODO DE REGISTRO
    // ============================================

    public async Task<bool> RegisterAsync(Usuario usuario)
    {
        try
        {
            // Verificar si el email ya existe
            if (_usuarios.Any(u => u.Email.ToLower() == usuario.Email.ToLower()))
                return await Task.FromResult(false);

            // Verificar si el username ya existe
            if (_usuarios.Any(u => u.Username.ToLower() == usuario.Username.ToLower()))
                return await Task.FromResult(false);

            usuario.Id = _nextId++;
            usuario.CreatedAt = DateTime.UtcNow;
            usuario.UpdatedAt = DateTime.UtcNow;
            _usuarios.Add(usuario);
            return await Task.FromResult(true);
        }
        catch
        {
            return await Task.FromResult(false);
        }
    }

    // ============================================
    // MÉTODOS DEL CRUD (Create, Update, Delete)
    // ============================================

    public async Task<Usuario> CreateAsync(UsuarioDTO usuarioDto)
    {
        try
        {
            // Verificar si el email ya existe
            var existeEmail = _usuarios.Any(u => u.Email.ToLower() == usuarioDto.Email.ToLower());
            if (existeEmail)
                throw new Exception("El email ya está registrado");

            // Verificar si el username ya existe
            var existeUsername = _usuarios.Any(u => u.Username.ToLower() == usuarioDto.Username.ToLower());
            if (existeUsername)
                throw new Exception("El nombre de usuario ya está registrado");

            var usuario = new Usuario
            {
                Id = _nextId++,
                NombreCompleto = usuarioDto.NombreCompleto,
                Email = usuarioDto.Email,
                Username = usuarioDto.Username,
                PasswordHash = usuarioDto.Password ?? "123456",
                Rol = usuarioDto.Rol ?? "Vendedor",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _usuarios.Add(usuario);
            return await Task.FromResult(usuario);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al crear usuario: {ex.Message}");
        }
    }

    public async Task<Usuario> UpdateAsync(int id, UsuarioDTO usuarioDto)
    {
        try
        {
            var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
                return null;

            // Verificar si el email ya existe (y no es el mismo usuario)
            var existeEmail = _usuarios.Any(u => u.Email.ToLower() == usuarioDto.Email.ToLower() && u.Id != id);
            if (existeEmail)
                throw new Exception("El email ya está registrado por otro usuario");

            // Verificar si el username ya existe (y no es el mismo usuario)
            var existeUsername = _usuarios.Any(u => u.Username.ToLower() == usuarioDto.Username.ToLower() && u.Id != id);
            if (existeUsername)
                throw new Exception("El nombre de usuario ya está registrado por otro usuario");

            // Actualizar propiedades
            usuario.NombreCompleto = usuarioDto.NombreCompleto ?? usuario.NombreCompleto;
            usuario.Email = usuarioDto.Email ?? usuario.Email;
            usuario.Username = usuarioDto.Username ?? usuario.Username;
            usuario.Rol = usuarioDto.Rol ?? usuario.Rol;
            usuario.UpdatedAt = DateTime.UtcNow;

            return await Task.FromResult(usuario);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al actualizar usuario: {ex.Message}");
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
                return await Task.FromResult(false);

            // No permitir eliminar al administrador principal
            if (usuario.Username == "admin" && usuario.Rol == "Administrador")
                return await Task.FromResult(false);

            _usuarios.Remove(usuario);
            return await Task.FromResult(true);
        }
        catch
        {
            return await Task.FromResult(false);
        }
    }
}