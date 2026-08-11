using FluentValidation;
using MiniMarket.web.DTOs.Pago.Requests;

namespace MiniMarket.web.Validators
{
    public class ProcesarPagoValidator : AbstractValidator<ProcesarPagoRequestDTO>
    {
        public ProcesarPagoValidator()
        {
            // Validación de VentaId
            RuleFor(x => x.VentaId)
                .GreaterThan(0).WithMessage("ID de venta inválido");

            // Validación de Monto
            RuleFor(x => x.Monto)
                .GreaterThan(0).WithMessage("El monto debe ser mayor a 0")
                .LessThanOrEqualTo(999999.99m).WithMessage("Monto excede el límite permitido");

            // Validación de Método de Pago
            RuleFor(x => x.MetodoPago)
                .NotEmpty().WithMessage("El método de pago es obligatorio")
                .Must(metodo => new[] { "Efectivo", "Tarjeta", "Transferencia", "Credito", "Yape", "Plin" }.Contains(metodo))
                .WithMessage("Método de pago no válido. Opciones: Efectivo, Tarjeta, Transferencia, Credito, Yape, Plin");

            // Validación de Moneda
            RuleFor(x => x.Moneda)
                .NotEmpty().WithMessage("La moneda es obligatoria")
                .Must(moneda => new[] { "PEN", "USD", "EUR" }.Contains(moneda))
                .WithMessage("Moneda no válida. Opciones: PEN, USD, EUR");

            // Validaciones condicionales según método de pago
            When(x => x.MetodoPago == "Efectivo", () =>
            {
                RuleFor(x => x.MontoPagado)
                    .NotNull().WithMessage("El monto recibido es requerido para pago en efectivo")
                    .GreaterThanOrEqualTo(x => x.Monto).WithMessage("El monto recibido debe ser mayor o igual al total");
            });

            When(x => x.MetodoPago == "Tarjeta", () =>
            {
                RuleFor(x => x.Tarjeta)
                    .NotNull().WithMessage("Datos de tarjeta requeridos");

                When(x => x.Tarjeta != null, () =>
                {
                    RuleFor(x => x.Tarjeta!.NumeroTarjeta)
                        .NotEmpty().WithMessage("Número de tarjeta requerido")
                        .Length(16, 19).WithMessage("Número de tarjeta inválido (debe tener entre 16 y 19 dígitos)");

                    RuleFor(x => x.Tarjeta!.NombreTitular)
                        .NotEmpty().WithMessage("Nombre del titular requerido");

                    RuleFor(x => x.Tarjeta!.FechaExpiracion)
                        .NotEmpty().WithMessage("Fecha de expiración requerida")
                        .Matches(@"^(0[1-9]|1[0-2])\/([0-9]{2})$").WithMessage("Formato de fecha inválido (MM/AA)");

                    RuleFor(x => x.Tarjeta!.Cvv)
                        .NotEmpty().WithMessage("CVV requerido")
                        .Length(3, 4).WithMessage("CVV inválido (debe tener 3 o 4 dígitos)");
                });
            });

            When(x => x.MetodoPago == "Yape" || x.MetodoPago == "Plin", () =>
            {
                RuleFor(x => x.Referencia)
                    .NotEmpty().WithMessage("Número de teléfono o referencia requerido para pago digital")
                    .Length(9, 15).WithMessage("Número de teléfono inválido (debe tener entre 9 y 15 dígitos)");
            });

            // Validación de Observaciones (opcional)
            RuleFor(x => x.Observaciones)
                .MaximumLength(500).WithMessage("Las observaciones no pueden tener más de 500 caracteres");
        }
    }
}