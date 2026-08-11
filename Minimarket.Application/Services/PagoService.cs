using MiniMarket.web.DTOs.Pago.Requests;
using MiniMarket.web.DTOs.Pago.Responses;

namespace MiniMarket.web.Services
{
    public class PagoService : IPagoService
    {
        private static List<ProcesarPagoResponseDTO> _pagos = new List<ProcesarPagoResponseDTO>();
        private static int _nextId = 1;

        public async Task<ProcesarPagoResponseDTO> ProcesarPagoAsync(ProcesarPagoRequestDTO request)
        {
            if (request.Monto <= 0)
                throw new Exception("El monto debe ser mayor a cero");

            var response = new ProcesarPagoResponseDTO
            {
                Id = _nextId++,
                VentaId = request.VentaId,
                MetodoPago = request.MetodoPago,
                MontoTotal = request.Monto,
                Moneda = request.Moneda,
                Estado = "Completado",
                NumeroTransaccion = $"TRX-{DateTime.Now:yyyyMMdd}-{new Random().Next(1000, 9999)}",
                FechaPago = DateTime.UtcNow,
                Exitoso = true,
                Mensaje = "Pago procesado exitosamente"
            };

            _pagos.Add(response);
            return await Task.FromResult(response);
        }

        public async Task<EstadoPagoResponseDTO> VerificarEstadoPagoAsync(int pagoId)
        {
            var pago = _pagos.FirstOrDefault(p => p.Id == pagoId);
            if (pago == null)
                throw new Exception($"Pago con ID {pagoId} no encontrado");

            return await Task.FromResult(new EstadoPagoResponseDTO
            {
                Id = pago.Id,
                Estado = pago.Estado,
                FechaActualizacion = DateTime.UtcNow,
                PuedeReintentar = pago.Estado == "Fallido",
                IntentosFallidos = 0
            });
        }

        public async Task<ProcesarPagoResponseDTO> ReembolsarPagoAsync(int pagoId, string? motivo = null)
        {
            var pago = _pagos.FirstOrDefault(p => p.Id == pagoId);
            if (pago == null)
                throw new Exception($"Pago con ID {pagoId} no encontrado");

            if (pago.Estado != "Completado")
                throw new Exception("Solo se pueden reembolsar pagos completados");

            pago.Estado = "Reembolsado";
            pago.Mensaje = "Reembolsado: " + (motivo ?? "Sin motivo especificado");  // ✅ CORREGIDO

            return await Task.FromResult(pago);
        }

        public async Task<bool> CancelarPagoAsync(int pagoId, string? motivo = null)
        {
            var pago = _pagos.FirstOrDefault(p => p.Id == pagoId);
            if (pago == null)
                return false;

            if (pago.Estado == "Completado")
                throw new Exception("No se puede cancelar un pago ya completado");

            pago.Estado = "Anulado";
            pago.Mensaje = "Cancelado: " + (motivo ?? "Sin motivo especificado");  

            return true;
        }

        public async Task<List<PagoMetodoDTO>> ObtenerMetodosPagoDisponiblesAsync(decimal monto, string moneda)
        {
            return await Task.FromResult(new List<PagoMetodoDTO>
            {
                new PagoMetodoDTO { Id = 1, Nombre = "Efectivo", Icono = "fa-money-bill-wave", Disponible = true },
                new PagoMetodoDTO { Id = 2, Nombre = "Tarjeta", Icono = "fa-credit-card", Disponible = true },
                new PagoMetodoDTO { Id = 3, Nombre = "Transferencia", Icono = "fa-university", Disponible = true },
                new PagoMetodoDTO { Id = 4, Nombre = "Crédito", Icono = "fa-hand-holding-usd", Disponible = true },
                new PagoMetodoDTO { Id = 5, Nombre = "Yape", Icono = "fa-mobile-alt", Disponible = true },
                new PagoMetodoDTO { Id = 6, Nombre = "Plin", Icono = "fa-mobile-alt", Disponible = true }
            });
        }

        public async Task<string> GenerarComprobantePagoAsync(int pagoId)
        {
            var pago = _pagos.FirstOrDefault(p => p.Id == pagoId);
            if (pago == null)
                throw new Exception($"Pago con ID {pagoId} no encontrado");

            return await Task.FromResult($"/comprobantes/pago-{pagoId}-{DateTime.Now:yyyyMMdd}.pdf");
        }

        public async Task<bool> ValidarTarjetaAsync(PagoTarjetaDTO tarjeta)
        {
            if (string.IsNullOrWhiteSpace(tarjeta.NumeroTarjeta) || tarjeta.NumeroTarjeta.Length < 16)
                return false;

            if (string.IsNullOrWhiteSpace(tarjeta.NombreTitular))
                return false;

            if (string.IsNullOrWhiteSpace(tarjeta.FechaExpiracion))
                return false;

            if (string.IsNullOrWhiteSpace(tarjeta.Cvv) || tarjeta.Cvv.Length < 3)
                return false;

            return true;
        }

        public async Task<ProcesarPagoResponseDTO> ProcesarPagoConTokenAsync(int clienteId, string token, decimal monto)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new Exception("El token de pago es obligatorio");

            if (monto <= 0)
                throw new Exception("El monto debe ser mayor a cero");

            return await Task.FromResult(new ProcesarPagoResponseDTO
            {
                Id = _nextId++,
                VentaId = 0,
                MetodoPago = "Tarjeta",
                MontoTotal = monto,
                Moneda = "PEN",
                Estado = "Completado",
                NumeroTransaccion = $"TOK-{DateTime.Now:yyyyMMdd}-{new Random().Next(1000, 9999)}",
                FechaPago = DateTime.UtcNow,
                Exitoso = true,
                Mensaje = "Pago con token procesado exitosamente"
            });
        }
    }
}