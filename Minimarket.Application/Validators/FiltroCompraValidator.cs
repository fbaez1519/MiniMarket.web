using Minimarket.Application.DTOs;
using Minimarket.Application.Common;

namespace Minimarket.Application.Validators
{
    public class FiltroCompraValidator
    {
        public ValidationResult Validate(FiltroCompraDto filtro)
        {
            var result = new ValidationResult();

            if (filtro == null)
            {
                result.AddError("General", "El filtro de compra es requerido");
                return result;
            }

            if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
            {
                if (filtro.FechaInicio > filtro.FechaFin)
                {
                    result.AddError("FechaInicio", "La fecha de inicio no puede ser mayor a la fecha final");
                }
            }

            // Agrega aquí más validaciones según necesites

            return result;
        }
    }
}