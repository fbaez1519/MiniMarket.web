using FluentValidation;
using MiniMarket.web.DTOs.Pago.Requests;
using MiniMarket.web.Enums;

namespace MiniMarket.web.Validators
{
    public class ProcesarPagoValidator : AbstractValidator<ProcesarPagoRequestDTO>
    {
        public ProcesarPagoValidator()
        {
            RuleFor(x => x.VentaId)
                .GreaterThan(0).WithMessage("ID de venta inválido");

            RuleFor(x => x.MontoTotal)
                .GreaterThan(0).WithMessage("El monto debe ser mayor a 0")
                .LessThanOrEqualTo(999999.99m).WithMessage("Monto excede el límite permitido");

            RuleFor(x => x.MetodoPago)
                .IsInEnum().WithMessage("Método de pago no válido");

            RuleFor(x => x.Moneda)
                .IsInEnum().WithMessage("Moneda no válida");

            // Validaciones condicionales según método de pago
            When(x => x.MetodoPago == MetodoPagoEnum.Efectivo, () =>
            {
                RuleFor(x => x.DatosEfectivo)
                    .NotNull().WithMessage("Datos de pago en efectivo requeridos")
                    .Must(d => d?.MontoRecibido >= d?.MontoTotal)
                    .WithMessage("El monto recibido debe ser mayor o igual al total");
            });

            When(x => x.MetodoPago == MetodoPagoEnum.TarjetaDebito || 
                      x.MetodoPago == MetodoPagoEnum.TarjetaCredito, () =>
            {
                RuleFor(x => x.DatosTarjeta)
                    .NotNull().WithMessage("Datos de tarjeta requeridos")
                    .ChildRules(t =>
                    {
                        t.RuleFor(d => d.NumeroTarjeta)
                            .Length(16, 19).WithMessage("Número de tarjeta inválido");
                        t.RuleFor(d => d.CVV)
                            .Length(3, 4).WithMessage("CVV inválido");
                        t.RuleFor(d => d.NombreTitular)
                            .NotEmpty().WithMessage("Nombre del titular requerido");
                    });
            });

            When(x => x.MetodoPago == MetodoPagoEnum.Yape || 
                      x.MetodoPago == MetodoPagoEnum.Plin, () =>
            {
                RuleFor(x => x.DatosDigital)
                    .NotNull().WithMessage("Datos de pago digital requeridos")
                    .ChildRules(d =>
                    {
                        d.RuleFor(dig => dig.NumeroTelefono)
                            .NotEmpty().WithMessage("Número de teléfono requerido")
                            .Length(9, 15).WithMessage("Número de teléfono inválido");
                    });
            });
        }
    }
}