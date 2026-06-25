using FluentValidation;
using MiniMarket.web.DTOs;

namespace MiniMarket.web.Validators;

public class VentaValidator : AbstractValidator<VentaDTO>
{
    public VentaValidator()
    {
        RuleFor(v => v.UsuarioId)
            .GreaterThan(0).WithMessage("El UsuarioId es obligatorio");

        RuleFor(v => v.Detalles)
            .NotEmpty().WithMessage("La venta debe tener al menos un detalle");

        RuleForEach(v => v.Detalles).SetValidator(new VentaDetalleValidator());
    }
}

public class VentaDetalleValidator : AbstractValidator<VentaDetalleDTO>
{
    public VentaDetalleValidator()
    {
        RuleFor(d => d.ProductoId)
            .GreaterThan(0).WithMessage("El ProductoId es obligatorio");

        RuleFor(d => d.Cantidad)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0");

        RuleFor(d => d.PrecioUnitario)
            .GreaterThan(0).WithMessage("El precio unitario debe ser mayor a 0");
    }
}