using System.Collections.Generic;
using System.Linq;

namespace Minimarket.Application.Common
{
    /// <summary>
    /// Representa el resultado de una validación, conteniendo errores agrupados por propiedad.
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Diccionario que almacena los errores agrupados por clave (nombre del campo o "General").
        /// </summary>
        public Dictionary<string, List<string>> Errors { get; } = new();

        /// <summary>
        /// Indica si la validación es exitosa (no hay errores).
        /// </summary>
        public bool IsValid => Errors.Count == 0;

        /// <summary>
        /// Agrega un mensaje de error asociado a una clave específica.
        /// </summary>
        /// <param name="key">Clave del error (ej: "ProductoId", "Cantidad", "General")</param>
        /// <param name="message">Mensaje de error descriptivo</param>
        public void AddError(string key, string message)
        {
            if (!Errors.ContainsKey(key))
                Errors[key] = new List<string>();

            Errors[key].Add(message);
        }

        /// <summary>
        /// Obtiene todos los mensajes de error como una lista plana.
        /// </summary>
        public List<string> GetAllErrors()
        {
            return Errors.SelectMany(x => x.Value).ToList();
        }

        /// <summary>
        /// Devuelve una representación en cadena de todos los errores concatenados.
        /// </summary>
        public override string ToString()
        {
            return string.Join("; ", Errors.SelectMany(x => x.Value));
        }
    }
}