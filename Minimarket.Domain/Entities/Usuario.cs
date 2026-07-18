using System;
using System.Collections.Generic;

namespace Minimarket.Domain.Entities
{
    /// <summary>
    /// Representa un usuario del sistema con roles y permisos
    /// </summary>
    public class Usuario : BaseEntity
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Rol { get; set; } = "Vendedor";

        /// <summary>
        /// Indica si el usuario está activo
        /// </summary>
        public bool EstaActivo { get; set; } = true;  

        public DateTime? UltimoAcceso { get; set; }
        public bool DebeCambiarPassword { get; set; } = false;
        public int IntentosFallidos { get; set; } = 0;
        public DateTime? FechaBloqueo { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        // 🔗 RELACIONES
        public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();
        public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

        public bool EstaBloqueado()
        {
            return FechaBloqueo.HasValue && FechaBloqueo.Value > DateTime.UtcNow;
        }

        public void Bloquear(int minutos = 30)
        {
            FechaBloqueo = DateTime.UtcNow.AddMinutes(minutos);
        }

        public void Desbloquear()
        {
            FechaBloqueo = null;
            IntentosFallidos = 0;
        }

        public void RegistrarIntentoFallido()
        {
            IntentosFallidos++;
            if (IntentosFallidos >= 5)
            {
                Bloquear(30);
            }
        }

        public void ResetearIntentos()
        {
            IntentosFallidos = 0;
            FechaBloqueo = null;
        }

        public void ActualizarUltimoAcceso()
        {
            UltimoAcceso = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}