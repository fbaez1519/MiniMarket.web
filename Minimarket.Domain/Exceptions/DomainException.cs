using System;

namespace Minimarket.Domain.Exceptions
{
    /// <summary>
    /// Excepción base para errores del dominio
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
        public DomainException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Excepción para cuando no se encuentra una entidad
    /// </summary>
    public class EntityNotFoundException : DomainException
    {
        public EntityNotFoundException(string entityName, int id)
            : base($"Entidad {entityName} con ID {id} no encontrada") { }

        public EntityNotFoundException(string message) : base(message) { }
    }

    /// <summary>
    /// Excepción para errores de validación
    /// </summary>
    public class ValidationException : DomainException
    {
        public ValidationException(string message) : base(message) { }
    }

    /// <summary>
    /// Excepción para violaciones de reglas de negocio
    /// </summary>
    public class BusinessRuleException : DomainException
    {
        public BusinessRuleException(string message) : base(message) { }
    }
}