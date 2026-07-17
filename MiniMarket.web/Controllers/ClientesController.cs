using Microsoft.AspNetCore.Mvc;
using MiniMarket.web.DTOs;
using MiniMarket.web.Services;

namespace MiniMarket.web.Controllers
{
    /// <summary>
    /// Controlador de API para la gestión de clientes
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClientesController(IClienteService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene todos los clientes
        /// </summary>
        /// <returns>Lista de clientes</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _service.GetAllAsync();
            return Ok(clientes);
        }

        /// <summary>
        /// Obtiene un cliente por su ID
        /// </summary>
        /// <param name="id">ID del cliente</param>
        /// <returns>Cliente encontrado</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _service.GetByIdAsync(id);
            if (cliente == null)
                return NotFound(new { mensaje = $"Cliente con ID {id} no encontrado" });

            return Ok(cliente);
        }

        /// <summary>
        /// Obtiene un cliente por su número de documento
        /// </summary>
        /// <param name="documento">Número de documento</param>
        /// <returns>Cliente encontrado</returns>
        [HttpGet("documento/{documento}")]
        public async Task<IActionResult> GetByDocumento(string documento)
        {
            var cliente = await _service.GetByDocumentoAsync(documento);
            if (cliente == null)
                return NotFound(new { mensaje = $"Cliente con documento {documento} no encontrado" });

            return Ok(cliente);
        }

        /// <summary>
        /// Crea un nuevo cliente
        /// </summary>
        /// <param name="clienteDto">Datos del cliente a crear</param>
        /// <returns>Cliente creado</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteCreateDTO clienteDto)
        {
            try
            {
                var nuevo = await _service.CreateAsync(clienteDto);
                return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza un cliente existente
        /// </summary>
        /// <param name="clienteDto">Datos actualizados del cliente</param>
        /// <returns>Cliente actualizado</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ClienteUpdateDTO clienteDto)
        {
            try
            {
                var actualizado = await _service.UpdateAsync(clienteDto);
                return Ok(actualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un cliente (borrado físico)
        /// </summary>
        /// <param name="id">ID del cliente a eliminar</param>
        /// <returns>Sin contenido</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _service.DeleteAsync(id);
            if (!eliminado)
                return NotFound(new { mensaje = $"Cliente con ID {id} no encontrado" });

            return NoContent();
        }

        /// <summary>
        /// Desactiva un cliente (borrado lógico)
        /// </summary>
        /// <param name="id">ID del cliente a desactivar</param>
        /// <returns>Mensaje de confirmación</returns>
        [HttpPatch("{id}/desactivar")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var desactivado = await _service.DesactivarAsync(id);
            if (!desactivado)
                return NotFound(new { mensaje = $"Cliente con ID {id} no encontrado" });

            return Ok(new { mensaje = "Cliente desactivado exitosamente" });
        }

        /// <summary>
        /// Activa un cliente
        /// </summary>
        /// <param name="id">ID del cliente a activar</param>
        /// <returns>Mensaje de confirmación</returns>
        [HttpPatch("{id}/activar")]
        public async Task<IActionResult> Activar(int id)
        {
            var activado = await _service.ActivarAsync(id);
            if (!activado)
                return NotFound(new { mensaje = $"Cliente con ID {id} no encontrado" });

            return Ok(new { mensaje = "Cliente activado exitosamente" });
        }
    }
}