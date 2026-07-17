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

        RuleFor(p => p.PrecioVenta)
            .GreaterThan(0).WithMessage("El precio de venta debe ser mayor a 0");

        RuleFor(p => p.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo");

        RuleFor(p => p.CategoriaId)
            .GreaterThan(0).WithMessage("Debe seleccionar una categoría");

        RuleFor(p => p.Codigo)
            .NotEmpty().WithMessage("El código es obligatorio")
            .MaximumLength(20).WithMessage("El código no puede tener más de 20 caracteres");

        RuleFor(p => p.PrecioCompra)
            .GreaterThanOrEqualTo(0).WithMessage("El precio de compra no puede ser negativo");

        RuleFor(p => p.StockMinimo)
            .GreaterThanOrEqualTo(0).WithMessage("El stock mínimo no puede ser negativo");
    }
}