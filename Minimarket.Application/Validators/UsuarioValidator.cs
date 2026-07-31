using FluentValidation;
using MiniMarket.web.DTOs;

namespace MiniMarket.web.Validators;

public class UsuarioValidator : AbstractValidator<UsuarioDTO>
{
    public UsuarioValidator()
    {
        // Validación de NombreCompleto
        RuleFor(u => u.NombreCompleto)
            .NotEmpty().WithMessage("El nombre completo es obligatorio")
            .MaximumLength(50).WithMessage("El nombre completo no puede tener más de 50 caracteres")
            .MinimumLength(3).WithMessage("El nombre completo debe tener al menos 3 caracteres");

        // Validación de Email
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("El email es obligatorio")
            .EmailAddress().WithMessage("El email no es válido")
            .MaximumLength(100).WithMessage("El email no puede tener más de 100 caracteres");

        // Validación de Username
        RuleFor(u => u.Username)
            .NotEmpty().WithMessage("El nombre de usuario es obligatorio")
            .MinimumLength(3).WithMessage("El nombre de usuario debe tener al menos 3 caracteres")
            .MaximumLength(20).WithMessage("El nombre de usuario no puede tener más de 20 caracteres")
            .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("El nombre de usuario solo puede contener letras, números y guión bajo");

        // ⚠️ Validación de Password - LA HAGO OPCIONAL (porque el servicio asigna "123456" por defecto)
        RuleFor(u => u.Password)
            .MinimumLength(6).When(u => !string.IsNullOrEmpty(u.Password))
                .WithMessage("La contraseña debe tener al menos 6 caracteres")
            .Matches(@"[A-Z]").When(u => !string.IsNullOrEmpty(u.Password))
                .WithMessage("La contraseña debe tener al menos una letra mayúscula")
            .Matches(@"[a-z]").When(u => !string.IsNullOrEmpty(u.Password))
                .WithMessage("La contraseña debe tener al menos una letra minúscula")
            .Matches(@"[0-9]").When(u => !string.IsNullOrEmpty(u.Password))
                .WithMessage("La contraseña debe tener al menos un número");

        // Validación de Rol
        RuleFor(u => u.Rol)
            .NotEmpty().WithMessage("El rol es obligatorio")
            .Must(r => r == "Administrador" || r == "Vendedor" || r == "Cliente")
            .WithMessage("El rol debe ser Administrador, Vendedor o Cliente");
    }
}