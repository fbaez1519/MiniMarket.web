using System;
using System.Collections.Generic;

namespace Minimarket.Domain.Entities
{
    /// <summary>
    /// Cliente del minimarket
    /// </summary>
    public class Cliente : BaseEntity
    {
        /// <summary>
        /// Número de documento del cliente (DNI, RUC, etc.) - Único
        /// </summary>
        public string Documento { get; set; } = string.Empty;

        /// <summary>
        /// Nombre completo del cliente
        /// </summary>
        public string NombreCompleto { get; set; } = string.Empty;

        /// <summary>
        /// Teléfono de contacto
        /// </summary>
        public string Telefono { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Dirección de domicilio
        /// </summary>
        public string Direccion { get; set; } = string.Empty;

        /// <summary>
        /// Tipo de cliente: Regular, VIP, Mayorista
        /// </summary>
        public string TipoCliente { get; set; } = "Regular";

        /// <summary>
        /// Fecha de registro del cliente
        /// </summary>
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Descuento especial para este cliente (porcentaje)
        /// </summary>
        public decimal? DescuentoEspecial { get; set; }

        /// <summary>
        /// Límite de crédito del cliente
        /// </summary>
        public decimal? LimiteCredito { get; set; }

        /// <summary>
        /// Saldo actual del cliente (deuda)
        /// </summary>
        public decimal Saldo { get; set; } = 0;

        /// <summary>
        /// Género: M, F
        /// </summary>
        public string? Genero { get; set; }

        /// <summary>
        /// Fecha de nacimiento
        /// </summary>
        public DateTime? FechaNacimiento { get; set; }

        // 🔗 RELACIONES
        public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();

        /// <summary>
        /// Verifica si el cliente tiene crédito disponible para un monto específico
        /// </summary>
        public bool TieneCreditoDisponible(decimal monto)
        {
            if (!LimiteCredito.HasValue) return true;
            return (Saldo + monto) <= LimiteCredito.Value;
        }

        /// <summary>
        /// Actualiza el saldo del cliente
        /// </summary>
        public void ActualizarSaldo(decimal monto)
        {
            Saldo += monto;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Aplica el descuento especial si existe
        /// </summary>
        public decimal AplicarDescuento(decimal monto)
        {
            if (!DescuentoEspecial.HasValue) return monto;
            return monto - (monto * DescuentoEspecial.Value / 100);
        }

        /// <summary>
        /// Verifica si el cliente es VIP
        /// </summary>
        public bool EsVIP() => TipoCliente == "VIP";

        /// <summary>
        /// Verifica si el cliente es Mayorista
        /// </summary>
        public bool EsMayorista() => TipoCliente == "Mayorista";
    }
}