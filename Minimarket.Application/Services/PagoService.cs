using System.Threading.Tasks;
using MiniMarket.web.DTOs.Pago.Requests;
using MiniMarket.web.DTOs.Pago.Responses;
using MiniMarket.web.Enums;

namespace MiniMarket.web.Services
{
    public interface IPagoService
    {
        Task<ProcesarPagoResponseDTO> ProcesarPagoAsync(ProcesarPagoRequestDTO request);
        Task<EstadoPagoResponseDTO> VerificarEstadoPagoAsync(int pagoId);
        Task<ProcesarPagoResponseDTO> ReembolsarPagoAsync(int pagoId, string? motivo = null);
        Task<bool> CancelarPagoAsync(int pagoId, string? motivo = null);
        Task<PagoMetodoDTO[]> ObtenerMetodosPagoDisponiblesAsync(decimal monto, MonedaEnum moneda);
        Task<string> GenerarComprobantePagoAsync(int pagoId);
        Task<bool> ValidarTarjetaAsync(PagoTarjetaDTO tarjeta);
        Task<ProcesarPagoResponseDTO> ProcesarPagoConTokenAsync(int clienteId, string token, decimal monto);
    }
}