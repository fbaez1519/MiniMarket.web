using FluentValidation;
using MiniMarket.web.DTOs;

namespace MiniMarket.web.Validators;

public class ProductoValidator : AbstractValidator<ProductoDTO>
{
    public ProductoValidator()
    {
        RuleFor(p => p.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MaximumLength(100).WithMessage("El nombre no puede tener más de 100 caracteres");

        RuleFor(p => p.Precio)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a 0");

        RuleFor(p => p.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo");

        RuleFor(p => p.Categoria)
            .NotEmpty().WithMessage("La categoría es obligatoria");
    }
}