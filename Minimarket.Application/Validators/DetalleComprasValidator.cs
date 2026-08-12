using Minimarket.Domain.DTOs.Compra;
using Minimarket.Domain.Validators.Base;

namespace Minimarket.Domain.Validators.Compra
{
    public class DetalleCompraValidator : BaseValidator<DetalleCompraRequestDto>
    {
        public override ValidationResult Validate(DetalleCompraRequestDto detalle)
        {
            if (detalle == null)
            {
                AddError("General", "El detalle de compra es requerido");
                return Fail();
            }

            // ============================================
            // VALIDACIÓN 1: Producto
            // ============================================
            if (detalle.ProductoId <= 0)
            {
                AddError(nameof(detalle.ProductoId), 
                    "Debe seleccionar un producto válido", 
                    "PRODUCTO_INVALIDO");
            }

            // ============================================
            // VALIDACIÓN 2: Cantidad
            // ============================================
            if (detalle.Cantidad <= 0)
            {
                AddError(nameof(detalle.Cantidad), 
                    "La cantidad debe ser mayor a 0", 
                    "CANTIDAD_INVALIDA");
            }
            else if (detalle.Cantidad > 99999)
            {
                AddError(nameof(detalle.Cantidad), 
                    "La cantidad no puede exceder 99,999 unidades", 
                    "CANTIDAD_EXCEDIDA");
            }

            // ============================================
            // VALIDACIÓN 3: Precio Unitario
            // ============================================
            if (detalle.PrecioUnitario <= 0)
            {
                AddError(nameof(detalle.PrecioUnitario), 
                    "El precio unitario debe ser mayor a 0", 
                    "PRECIO_INVALIDO");
            }
            else if (detalle.PrecioUnitario > 999999.99m)
            {
                AddError(nameof(detalle.PrecioUnitario), 
                    "El precio unitario no puede exceder $999,999.99", 
                    "PRECIO_EXCEDIDO");
            }

            // ============================================
            // VALIDACIÓN 4: Subtotal (validación de consistencia)
            // ============================================
            if (detalle.Cantidad > 0 && detalle.PrecioUnitario > 0)
            {
                var subtotal = detalle.Cantidad * detalle.PrecioUnitario;
                if (subtotal > 99999999.99m)
                {
                    AddError("General", 
                        $"El subtotal ({subtotal:C}) excede el límite permitido", 
                        "SUBTOTAL_EXCEDIDO");
                }
            }

            return HasErrors ? Fail() : Success();
        }
    }
}