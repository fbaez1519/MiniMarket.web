using System;
using System.Text.RegularExpressions;
using Minimarket.Application.DTOs;

namespace Minimarket.Application.Validators
{
    public class FiltroCompraValidator
    {
        public ValidationResult Validate(FiltroCompraDto filtro)
        {
            var result = new ValidationResult();

            if (filtro == null)
            {
                result.AddError("General", "Los filtros son requeridos");
                return result;
            }

            if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
            {
                if (filtro.FechaInicio.Value > filtro.FechaFin.Value)
                {
                    result.AddError("FechaInicio", "La fecha de inicio no puede ser mayor a la fecha fin");
                }

                if (filtro.FechaInicio.Value > DateTime.UtcNow)
                {
                    result.AddError("FechaInicio", "La fecha de inicio no puede ser futura");
                }

                if (filtro.FechaFin.Value > DateTime.UtcNow)
                {
                    result.AddError("FechaFin", "La fecha fin no puede ser futura");
                }

                var dias = (filtro.FechaFin.Value - filtro.FechaInicio.Value).Days;
                if (dias > 365)
                {
                    result.AddError("General", "El rango de fechas no puede exceder 365 días");
                }
            }

            if (filtro.ProveedorId.HasValue && filtro.ProveedorId.Value <= 0)
            {
                result.AddError("ProveedorId", "El proveedor seleccionado no es válido");
            }

            if (filtro.UsuarioId.HasValue && filtro.UsuarioId.Value <= 0)
            {
                result.AddError("UsuarioId", "El usuario seleccionado no es válido");
            }

            if (filtro.PageNumber < 1)
            {
                result.AddError("PageNumber", "El número de página debe ser mayor a 0");
            }

            if (filtro.PageSize < 1 || filtro.PageSize > 100)
            {
                result.AddError("PageSize", "El tamaño de página debe estar entre 1 y 100");
            }

            if (!string.IsNullOrEmpty(filtro.NumeroCompra))
            {
                if (filtro.NumeroCompra.Length < 5 || filtro.NumeroCompra.Length > 20)
                {
                    result.AddError("NumeroCompra", "El número de compra debe tener entre 5 y 20 caracteres");
                }

                if (!Regex.IsMatch(filtro.NumeroCompra, @"^C-\d{4}-\d{3}$"))
                {
                    result.AddError("NumeroCompra", "Formato inválido. Use: C-YYYY-XXX (ej: C-2024-001)");
                }
            }

            return result;
        }
    }
}
