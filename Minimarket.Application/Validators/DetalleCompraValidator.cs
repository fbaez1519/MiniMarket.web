using Minimarket.Application.DTOs;
using Minimarket.Application.Common;

namespace Minimarket.Application.Validators
{
    public class DetalleCompraValidator
    {
        public ValidationResult Validate(DetalleCompraRequestDto detalle)
        {
            var result = new ValidationResult();

            if (detalle == null)
            {
                result.AddError("General", "El detalle de compra es requerido");
                return result;
            }

            if (detalle.ProductoId <= 0)
            {
                result.AddError("ProductoId", "Debe seleccionar un producto válido");
            }

            if (detalle.Cantidad <= 0)
            {
                result.AddError("Cantidad", "La cantidad debe ser mayor a 0");
            }
            else if (detalle.Cantidad > 99999)
            {
                result.AddError("Cantidad", "La cantidad no puede exceder 99,999 unidades");
            }

            if (detalle.PrecioUnitario <= 0)
            {
                result.AddError("PrecioUnitario", "El precio unitario debe ser mayor a 0");
            }
            else if (detalle.PrecioUnitario > 999999.99m)
            {
                result.AddError("PrecioUnitario", "El precio unitario no puede exceder $999,999.99");
            }

            if (detalle.Cantidad > 0 && detalle.PrecioUnitario > 0)
            {
                var subtotal = detalle.Cantidad * detalle.PrecioUnitario;
                if (subtotal > 99999999.99m)
                {
                    result.AddError("General", $"El subtotal ({subtotal:C}) excede el límite permitido");
                }
            }

            return result;
        }
    }
}