using System;
using Minimarket.Domain.DTOs.Compra;
using Minimarket.Domain.Validators.Base;

namespace Minimarket.Domain.Validators.Compra
{
    public class FiltroCompraValidator : BaseValidator<FiltroCompraDto>
    {
        public override ValidationResult Validate(FiltroCompraDto filtro)
        {
            if (filtro == null)
            {
                AddError("General", "Los filtros son requeridos");
                return Fail();
            }

            // ============================================
            // VALIDACIÓN 1: Fechas
            // ============================================
            if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
            {
                if (filtro.FechaInicio.Value > filtro.FechaFin.Value)
                {
                    AddError(nameof(filtro.FechaInicio), 
                        "La fecha de inicio no puede ser mayor a la fecha fin", 
                        "FECHAS_INVALIDAS");
                }

                if (IsFutureDate(filtro.FechaInicio.Value))
                {
                    AddError(nameof(filtro.FechaInicio), 
                        "La fecha de inicio no puede ser futura", 
                        "FECHA_INICIO_FUTURA");
                }

                if (IsFutureDate(filtro.FechaFin.Value))
                {
                    AddError(nameof(filtro.FechaFin), 
                        "La fecha fin no puede ser futura", 
                        "FECHA_FIN_FUTURA");
                }

                // Rango máximo de búsqueda: 1 año
                var dias = (filtro.FechaFin.Value - filtro.FechaInicio.Value).Days;
                if (dias > 365)
                {
                    AddError("General", 
                        "El rango de fechas no puede exceder 365 días", 
                        "RANGO_FECHAS_EXCEDIDO");
                }
            }

            // ============================================
            // VALIDACIÓN 2: Proveedor
            // ============================================
            if (filtro.ProveedorId.HasValue && filtro.ProveedorId.Value <= 0)
            {
                AddError(nameof(filtro.ProveedorId), 
                    "El proveedor seleccionado no es válido", 
                    "PROVEEDOR_INVALIDO");
            }

            // ============================================
            // VALIDACIÓN 3: Usuario
            // ============================================
            if (filtro.UsuarioId.HasValue && filtro.UsuarioId.Value <= 0)
            {
                AddError(nameof(filtro.UsuarioId), 
                    "El usuario seleccionado no es válido", 
                    "USUARIO_INVALIDO");
            }

            // ============================================
            // VALIDACIÓN 4: Paginación
            // ============================================
            if (filtro.PageNumber < 1)
            {
                AddError(nameof(filtro.PageNumber), 
                    "El número de página debe ser mayor a 0", 
                    "PAGE_INVALIDO");
            }

            if (filtro.PageSize < 1 || filtro.PageSize > 100)
            {
                AddError(nameof(filtro.PageSize), 
                    "El tamaño de página debe estar entre 1 y 100", 
                    "PAGE_SIZE_INVALIDO");
            }

            // ============================================
            // VALIDACIÓN 5: Número de compra
            // ============================================
            if (!string.IsNullOrEmpty(filtro.NumeroCompra))
            {
                if (!IsValidLength(filtro.NumeroCompra, 5, 20))
                {
                    AddError(nameof(filtro.NumeroCompra), 
                        "El número de compra debe tener entre 5 y 20 caracteres", 
                        "NUMERO_COMPRA_INVALIDO");
                }

                // Validar formato: C-YYYY-XXX
                if (!System.Text.RegularExpressions.Regex.IsMatch(filtro.NumeroCompra, @"^C-\d{4}-\d{3}$"))
                {
                    AddError(nameof(filtro.NumeroCompra), 
                        "Formato inválido. Use: C-YYYY-XXX (ej: C-2024-001)", 
                        "FORMATO_INVALIDO");
                }
            }

            return HasErrors ? Fail() : Success();
        }
    }
}