using MiniMarket.web.DTOs.Pago.Requests;
using MiniMarket.web.DTOs.Pago.Responses;

namespace MiniMarket.web.Services
{
    public interface IPagoService
    {
        /// <summary>
        /// Procesa un pago
        /// </summary>
        Task<ProcesarPagoResponseDTO> ProcesarPagoAsync(ProcesarPagoRequestDTO request);

        /// <summary>
        /// Verifica el estado de un pago
        /// </summary>
        Task<EstadoPagoResponseDTO> VerificarEstadoPagoAsync(int pagoId);

        /// <summary>
        /// Reembolsa un pago
        /// </summary>
        Task<ProcesarPagoResponseDTO> ReembolsarPagoAsync(int pagoId, string? motivo = null);

        /// <summary>
        /// Cancela un pago
        /// </summary>
        Task<bool> CancelarPagoAsync(int pagoId, string? motivo = null);

        /// <summary>
        /// Obtiene los métodos de pago disponibles
        /// </summary>
        Task<List<PagoMetodoDTO>> ObtenerMetodosPagoDisponiblesAsync(decimal monto, string moneda);

        /// <summary>
        /// Genera el comprobante de pago
        /// </summary>
        Task<string> GenerarComprobantePagoAsync(int pagoId);

        /// <summary>
        /// Valida una tarjeta
        /// </summary>
        Task<bool> ValidarTarjetaAsync(PagoTarjetaDTO tarjeta);

        /// <summary>
        /// Procesa un pago con token
        /// </summary>
        Task<ProcesarPagoResponseDTO> ProcesarPagoConTokenAsync(int clienteId, string token, decimal monto);
    }
}