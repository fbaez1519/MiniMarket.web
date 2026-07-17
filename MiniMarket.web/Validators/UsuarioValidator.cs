using FluentValidation;
using MiniMarket.web.DTOs;

namespace MiniMarket.web.Validators;

public class UsuarioValidator : AbstractValidator<UsuarioDTO>
{
    public UsuarioValidator()
    {
        RuleFor(u => u.NombreCompleto)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MaximumLength(50).WithMessage("El nombre no puede tener más de 50 caracteres");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("El email es obligatorio")
            .EmailAddress().WithMessage("El email no es válido");

        RuleFor(u => u.Rol)
            .NotEmpty().WithMessage("El rol es obligatorio")
            .Must(r => r == "Administrador" || r == "Vendedor" || r == "Cliente")
            .WithMessage("El rol debe ser Administrador, Vendedor o Cliente");
    }
}