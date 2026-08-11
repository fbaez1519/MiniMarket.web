using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MiniMarket.web.DTOs.Pago.Requests;
using MiniMarket.web.Services;
using FluentValidation;

[ApiController]
[Route("api/[controller]")]
public class PagosController : ControllerBase
{
    private readonly IPagoService _pagoService;
    private readonly IValidator<ProcesarPagoRequestDTO> _validator;

    public PagosController(IPagoService pagoService, IValidator<ProcesarPagoRequestDTO> validator)
    {
        _pagoService = pagoService;
        _validator = validator;
    }

    /// <summary>
    /// Procesa un pago para una venta
    /// </summary>
    [HttpPost("procesar")]
    public async Task<IActionResult> ProcesarPago([FromBody] ProcesarPagoRequestDTO request)
    {
        // Validar request
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new
            {
                Exitoso = false,
                Errores = validationResult.Errors.Select(e => new
                {
                    Campo = e.PropertyName,
                    Mensaje = e.ErrorMessage
                })
            });
        }

        try
        {
            var response = await _pagoService.ProcesarPagoAsync(request);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Exitoso = false, Mensaje = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Exitoso = false, Mensaje = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Verifica el estado de un pago
    /// </summary>
    [HttpGet("{pagoId}/estado")]
    public async Task<IActionResult> VerificarEstado(int pagoId)
    {
        var response = await _pagoService.VerificarEstadoPagoAsync(pagoId);
        return Ok(response);
    }

    /// <summary>
    /// Reembolsa un pago
    /// </summary>
    [HttpPost("{pagoId}/reembolsar")]
    public async Task<IActionResult> ReembolsarPago(int pagoId, [FromBody] string? motivo = null)
    {
        try
        {
            var response = await _pagoService.ReembolsarPagoAsync(pagoId, motivo);
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
    public async Task<IActionResult> CancelarPago(int pagoId, [FromBody] string? motivo = null)
    {
        var result = await _pagoService.CancelarPagoAsync(pagoId, motivo);
        return Ok(new { Exitoso = result });
    }

    /// <summary>
    /// Obtiene métodos de pago disponibles
    /// </summary>
    [HttpGet("metodos")]
    public async Task<IActionResult> ObtenerMetodos([FromQuery] decimal monto, [FromQuery] MonedaEnum moneda)
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
        var comprobante = await _pagoService.GenerarComprobantePagoAsync(pagoId);
        return Ok(new { Comprobante = comprobante });
    }
}