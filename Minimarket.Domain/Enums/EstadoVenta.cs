using System.ComponentModel.DataAnnotations;

namespace MiniMarket.web.Enums
{
    public enum EstadoVenta
    {
        [Display(Name = "Pendiente")]
        Pendiente = 1,
        
        [Display(Name = "Completada")]
        Completada = 2,
        
        [Display(Name = "Anulada")]
        Anulada = 3,
        
        [Display(Name = "Reembolsada")]
        Reembolsada = 4
    }
}