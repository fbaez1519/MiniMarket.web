using MiniMarket.web.DTOs;

namespace MiniMarket.web.Services
{
    /// <summary>
    /// Interfaz para el servicio de categorías
    /// </summary>
    public interface ICategoriaService
    {
        /// <summary>
        /// Obtiene todas las categorías
        /// </summary>
        Task<IEnumerable<CategoriaDTO>> GetAllAsync();

        /// <summary>
        /// Obtiene una categoría por su ID
        /// </summary>
        Task<CategoriaDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Obtiene una categoría por su código
        /// </summary>
        Task<CategoriaDTO?> GetByCodigoAsync(string codigo);

        /// <summary>
        /// Obtiene todas las categorías principales (sin padre)
        /// </summary>
        Task<IEnumerable<CategoriaDTO>> GetPrincipalesAsync();

        /// <summary>
        /// Obtiene las subcategorías de una categoría padre
        /// </summary>
        Task<IEnumerable<CategoriaDTO>> GetSubCategoriasAsync(int categoriaPadreId);

        /// <summary>
        /// Crea una nueva categoría
        /// </summary>
        Task<CategoriaDTO> CreateAsync(CategoriaCreateDTO categoriaDto);

        /// <summary>
        /// Actualiza una categoría existente
        /// </summary>
        Task<CategoriaDTO> UpdateAsync(CategoriaUpdateDTO categoriaDto);

        /// <summary>
        /// Elimina una categoría (borrado físico)
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Desactiva una categoría (borrado lógico)
        /// </summary>
        Task<bool> DesactivarAsync(int id);

        /// <summary>
        /// Activa una categoría
        /// </summary>
        Task<bool> ActivarAsync(int id);
    }
}