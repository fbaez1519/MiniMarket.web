using Microsoft.AspNetCore.Mvc;
using MiniMarket.web.DTOs;
using MiniMarket.web.Services;

namespace MiniMarket.web.Controllers
{
    /// <summary>
    /// Controlador de API para la gestión de categorías
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriasController(ICategoriaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene todas las categorías
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categorias = await _service.GetAllAsync();
            return Ok(categorias);
        }

        /// <summary>
        /// Obtiene una categoría por su ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoria = await _service.GetByIdAsync(id);
            if (categoria == null)
                return NotFound(new { mensaje = $"Categoría con ID {id} no encontrada" });

            return Ok(categoria);
        }

        /// <summary>
        /// Obtiene una categoría por su código
        /// </summary>
        [HttpGet("codigo/{codigo}")]
        public async Task<IActionResult> GetByCodigo(string codigo)
        {
            var categoria = await _service.GetByCodigoAsync(codigo);
            if (categoria == null)
                return NotFound(new { mensaje = $"Categoría con código {codigo} no encontrada" });

            return Ok(categoria);
        }

        /// <summary>
        /// Obtiene todas las categorías principales (sin padre)
        /// </summary>
        [HttpGet("principales")]
        public async Task<IActionResult> GetPrincipales()
        {
            var categorias = await _service.GetPrincipalesAsync();
            return Ok(categorias);
        }

        /// <summary>
        /// Obtiene las subcategorías de una categoría padre
        /// </summary>
        [HttpGet("{id}/subcategorias")]
        public async Task<IActionResult> GetSubCategorias(int id)
        {
            var subCategorias = await _service.GetSubCategoriasAsync(id);
            return Ok(subCategorias);
        }

        /// <summary>
        /// Crea una nueva categoría
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoriaCreateDTO categoriaDto)
        {
            try
            {
                var nuevo = await _service.CreateAsync(categoriaDto);
                return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza una categoría existente
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoriaUpdateDTO categoriaDto)
        {
            try
            {
                var actualizado = await _service.UpdateAsync(categoriaDto);
                return Ok(actualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Elimina una categoría
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var eliminado = await _service.DeleteAsync(id);
                if (!eliminado)
                    return NotFound(new { mensaje = $"Categoría con ID {id} no encontrada" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Desactiva una categoría
        /// </summary>
        [HttpPatch("{id}/desactivar")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var desactivado = await _service.DesactivarAsync(id);
            if (!desactivado)
                return NotFound(new { mensaje = $"Categoría con ID {id} no encontrada" });

            return Ok(new { mensaje = "Categoría desactivada exitosamente" });
        }

        /// <summary>
        /// Activa una categoría
        /// </summary>
        [HttpPatch("{id}/activar")]
        public async Task<IActionResult> Activar(int id)
        {
            var activado = await _service.ActivarAsync(id);
            if (!activado)
                return NotFound(new { mensaje = $"Categoría con ID {id} no encontrada" });

            return Ok(new { mensaje = "Categoría activada exitosamente" });
        }
    }
}