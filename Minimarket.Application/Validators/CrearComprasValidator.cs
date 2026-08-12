using System;
using System.Collections.Generic;
using System.Linq;
using Minimarket.Domain.DTOs.Compra;
using Minimarket.Domain.Validators.Base;

namespace Minimarket.Domain.Validators.Compra
{
    public class CrearCompraValidator : BaseValidator<CrearCompraDto>
    {
        private readonly DetalleCompraValidator _detalleValidator;

        public CrearCompraValidator()
        {
            _detalleValidator = new DetalleCompraValidator();
        }

        public override ValidationResult Validate(CrearCompraDto compra)
        {
            if (compra == null)
            {
                AddError("General", "Los datos de la compra son requeridos");
                return Fail();
            }

            // ============================================
            // VALIDACIÓN 1: Proveedor
            // ============================================
            if (compra.ProveedorId <= 0)
            {
                AddError(nameof(compra.ProveedorId), 
                    "Debe seleccionar un proveedor válido", 
                    "PROVEEDOR_REQUERIDO");
            }

            // ============================================
            // VALIDACIÓN 2: Usuario
            // ============================================
            if (compra.UsuarioId <= 0)
            {
                AddError(nameof(compra.UsuarioId), 
                    "Usuario no válido", 
                    "USUARIO_INVALIDO");
            }

            // ============================================
            // VALIDACIÓN 3: Observaciones
            // ============================================
            if (!string.IsNullOrEmpty(compra.Observaciones))
            {
                if (!IsValidLength(compra.Observaciones, 0, 500))
                {
                    AddError(nameof(compra.Observaciones), 
                        "Las observaciones no pueden exceder los 500 caracteres", 
                        "OBSERVACIONES_LARGAS");
                }

                if (compra.Observaciones.Any(c => char.IsControl(c) && c != '\n' && c != '\r'))
                {
                    AddError(nameof(compra.Observaciones), 
                        "Las observaciones contienen caracteres no válidos", 
                        "OBSERVACIONES_INVALIDAS");
                }
            }

            // ============================================
            // VALIDACIÓN 4: Detalles (Productos)
            // ============================================
            if (compra.Detalles == null || !compra.Detalles.Any())
            {
                AddError(nameof(compra.Detalles), 
                    "Debe agregar al menos un producto a la compra", 
                    "SIN_PRODUCTOS");
            }
            else
            {
                // Validar cada detalle
                for (int i = 0; i < compra.Detalles.Count; i++)
                {
                    var detalle = compra.Detalles[i];
                    var detalleResult = _detalleValidator.Validate(detalle);

                    if (!detalleResult.IsValid)
                    {
                        foreach (var error in detalleResult.Errors)
                        {
                            AddError($"Detalles[{i}].{error.PropertyName}", 
                                error.Message, 
                                error.Code);
                        }
                    }
                }

                // Validar productos duplicados
                var productosDuplicados = compra.Detalles
                    .GroupBy(d => d.ProductoId)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                if (productosDuplicados.Any())
                {
                    AddError(nameof(compra.Detalles), 
                        $"Productos duplicados encontrados: {string.Join(", ", productosDuplicados)}. " +
                        "Combine las cantidades en un solo registro.", 
                        "PRODUCTOS_DUPLICADOS");
                }

                // Validar total de la compra
                var total = compra.Detalles.Sum(d => d.Cantidad * d.PrecioUnitario);
                if (total > 99999999.99m)
                {
                    AddError("General", 
                        $"El total de la compra ({total:C}) excede el límite permitido de $99,999,999.99", 
                        "TOTAL_EXCEDIDO");
                }

                // Validar cantidad total de productos
                var cantidadTotal = compra.Detalles.Sum(d => d.Cantidad);
                if (cantidadTotal > 999999)
                {
                    AddError("General", 
                        $"La cantidad total de productos ({cantidadTotal}) excede el límite permitido", 
                        "CANTIDAD_TOTAL_EXCEDIDA");
                }
            }

            return HasErrors ? Fail() : Success();
        }
    }
}