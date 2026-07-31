using AutoMapper;
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
    private readonly IMapper _mapper;

    public UsuarioService(IMapper mapper)
    {
        _mapper = mapper;
    }

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
    // MÉTODOS CRUD PARA ADMINISTRADORES
    // ============================================

    public async Task<Usuario> CreateAsync(UsuarioCreateDTO usuarioDto)
    {
        // Verificar email único
        if (_usuarios.Any(u => u.Email.ToLower() == usuarioDto.Email.ToLower()))
            throw new Exception($"El email {usuarioDto.Email} ya está registrado");

        // Verificar username único
        if (_usuarios.Any(u => u.Username.ToLower() == usuarioDto.Username.ToLower()))
            throw new Exception($"El username {usuarioDto.Username} ya está registrado");

        var usuario = new Usuario
        {
            Id = _nextId++,
            NombreCompleto = usuarioDto.NombreCompleto,
            Email = usuarioDto.Email,
            Username = usuarioDto.Username,
            PasswordHash = usuarioDto.Password,
            Rol = usuarioDto.Rol ?? "Vendedor",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _usuarios.Add(usuario);
        return usuario;
    }

    public async Task<Usuario> UpdateAsync(UsuarioUpdateDTO usuarioDto)
    {
        var usuario = _usuarios.FirstOrDefault(u => u.Id == usuarioDto.Id);
        if (usuario == null)
            throw new Exception($"Usuario con ID {usuarioDto.Id} no encontrado");

        // Verificar email único (excepto el mismo)
        if (_usuarios.Any(u => u.Email.ToLower() == usuarioDto.Email.ToLower() && u.Id != usuarioDto.Id))
            throw new Exception($"El email {usuarioDto.Email} ya está registrado por otro usuario");

        // Verificar username único (excepto el mismo)
        if (_usuarios.Any(u => u.Username.ToLower() == usuarioDto.Username.ToLower() && u.Id != usuarioDto.Id))
            throw new Exception($"El username {usuarioDto.Username} ya está registrado por otro usuario");

        usuario.NombreCompleto = usuarioDto.NombreCompleto;
        usuario.Email = usuarioDto.Email;
        usuario.Username = usuarioDto.Username;
        usuario.Rol = usuarioDto.Rol;
        usuario.UpdatedAt = DateTime.UtcNow;

        return usuario;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
        if (usuario == null)
            return false;

        // No permitir eliminar al administrador principal
        if (usuario.Username == "admin" && usuario.Rol == "Administrador")
            throw new Exception("No se puede eliminar al administrador principal");

        _usuarios.Remove(usuario);
        return true;
    }

    public async Task<bool> DesactivarAsync(int id)
    {
        var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
        if (usuario == null)
            return false;

        usuario.EstaActivo = false;
        usuario.UpdatedAt = DateTime.UtcNow;
        return true;
    }

    public async Task<bool> ActivarAsync(int id)
    {
        var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
        if (usuario == null)
            return false;

        usuario.EstaActivo = true;
        usuario.UpdatedAt = DateTime.UtcNow;
        return true;
    }

    public async Task<bool> CambiarPasswordAsync(int id, string nuevaPassword)
    {
        var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
        if (usuario == null)
            throw new Exception("Usuario no encontrado");

        if (string.IsNullOrEmpty(nuevaPassword) || nuevaPassword.Length < 6)
            throw new Exception("La nueva contraseña debe tener al menos 6 caracteres");

        usuario.PasswordHash = nuevaPassword;
        usuario.UpdatedAt = DateTime.UtcNow;
        return true;
    }
}