using MiniMarket.web.DTOs;

namespace MiniMarket.web.Services
{
    /// <summary>
    /// Interfaz para el servicio de clientes
    /// </summary>
    public interface IClienteService
    {
        /// <summary>
        /// Obtiene todos los clientes
        /// </summary>
        Task<IEnumerable<ClienteDTO>> GetAllAsync();

        /// <summary>
        /// Obtiene un cliente por su ID
        /// </summary>
        Task<ClienteDTO> GetByIdAsync(int id);

        /// <summary>
        /// Obtiene un cliente por su número de documento
        /// </summary>
        Task<ClienteDTO> GetByDocumentoAsync(string documento);

        /// <summary>
        /// Obtiene un cliente por su email
        /// </summary>
        Task<ClienteDTO> GetByEmailAsync(string email);

        /// <summary>
        /// Crea un nuevo cliente
        /// </summary>
        Task<ClienteDTO> CreateAsync(ClienteCreateDTO clienteDto);

        /// <summary>
        /// Actualiza un cliente existente
        /// </summary>
        Task<ClienteDTO> UpdateAsync(ClienteUpdateDTO clienteDto);

        /// <summary>
        /// Elimina un cliente (borrado físico)
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Desactiva un cliente (borrado lógico)
        /// </summary>
        Task<bool> DesactivarAsync(int id);

        /// <summary>
        /// Activa un cliente
        /// </summary>
        Task<bool> ActivarAsync(int id);
    }
}