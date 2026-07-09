using System;

namespace Minimarket.Domain.Entities
{
    /// <summary>
    /// Entidad base que todas las entidades del dominio deben heredar
    /// </summary>
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}