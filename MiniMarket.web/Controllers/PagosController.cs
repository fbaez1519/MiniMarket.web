using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MiniMarket.web.DTOs.Pago.Requests;
using MiniMarket.web.DTOs.Pago.Responses;
using MiniMarket.web.Services;

namespace MiniMarket.web.Controllers
{
    /// <summary>
    /// Controlador de API para la gestión de pagos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly IPagoService _pagoService;

        public PagosController(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }

        /// <summary>
        /// Procesa un pago para una venta
        /// </summary>
        [HttpPost("procesar")]
        public async Task<IActionResult> ProcesarPago([FromBody] ProcesarPagoRequestDTO request)
        {
            // Validación básica
            if (request == null)
                return BadRequest(new { Exitoso = false, Mensaje = "Los datos del pago son requeridos" });

            if (request.VentaId <= 0)
                return BadRequest(new { Exitoso = false, Mensaje = "ID de venta inválido" });

            if (request.Monto <= 0)
                return BadRequest(new { Exitoso = false, Mensaje = "El monto debe ser mayor a cero" });

            if (string.IsNullOrEmpty(request.MetodoPago))
                return BadRequest(new { Exitoso = false, Mensaje = "El método de pago es obligatorio" });

            try
            {
                var response = await _pagoService.ProcesarPagoAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exitoso = false, Mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Verifica el estado de un pago
        /// </summary>
        [HttpGet("{pagoId}/estado")]
        public async Task<IActionResult> VerificarEstado(int pagoId)
        {
            try
            {
                var response = await _pagoService.VerificarEstadoPagoAsync(pagoId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new { Exitoso = false, Mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Reembolsa un pago
        /// </summary>
        [HttpPost("{pagoId}/reembolsar")]
        public async Task<IActionResult> ReembolsarPago(int pagoId, [FromBody] ReembolsoRequestDTO request)
        {
            try
            {
                var response = await _pagoService.ReembolsarPagoAsync(pagoId, request?.Motivo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exitoso = false, Mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Cancela un pago
        /// </summary>
        [HttpPost("{pagoId}/cancelar")]
        public async Task<IActionResult> CancelarPago(int pagoId, [FromBody] CancelarPagoRequestDTO request)
        {
            try
            {
                var result = await _pagoService.CancelarPagoAsync(pagoId, request?.Motivo);
                return Ok(new { Exitoso = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exitoso = false, Mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene métodos de pago disponibles
        /// </summary>
        [HttpGet("metodos")]
        public async Task<IActionResult> ObtenerMetodos([FromQuery] decimal monto, [FromQuery] string moneda = "PEN")
        {
            var metodos = await _pagoService.ObtenerMetodosPagoDisponiblesAsync(monto, moneda);
            return Ok(metodos);
        }

        /// <summary>
        /// Genera comprobante de pago
        /// </summary>
        [HttpGet("{pagoId}/comprobante")]
        public async Task<IActionResult> GenerarComprobante(int pagoId)
        {
            try
            {
                var comprobante = await _pagoService.GenerarComprobantePagoAsync(pagoId);
                return Ok(new { Exitoso = true, Comprobante = comprobante });
            }
            catch (Exception ex)
            {
                return NotFound(new { Exitoso = false, Mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Valida una tarjeta
        /// </summary>
        [HttpPost("validar-tarjeta")]
        public async Task<IActionResult> ValidarTarjeta([FromBody] PagoTarjetaDTO tarjeta)
        {
            var result = await _pagoService.ValidarTarjetaAsync(tarjeta);
            return Ok(new { Exitoso = result, Mensaje = result ? "Tarjeta válida" : "Tarjeta inválida" });
        }
    }

    /// <summary>
    /// DTO para solicitar reembolso
    /// </summary>
    public class ReembolsoRequestDTO
    {
        public string? Motivo { get; set; }
    }

    /// <summary>
    /// DTO para solicitar cancelación
    /// </summary>
    public class CancelarPagoRequestDTO
    {
        public string? Motivo { get; set; }
    }
}