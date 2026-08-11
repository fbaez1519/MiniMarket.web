using System.ComponentModel.DataAnnotations;

namespace MiniMarket.web.Enums
{
    public enum TipoPagoEnum
    {
        [Display(Name = "Efectivo")]
        Efectivo = 1,
        
        [Display(Name = "Tarjeta Débito/Crédito")]
        Tarjeta = 2,
        
        [Display(Name = "Yape")]
        Yape = 3,
        
        [Display(Name = "Plin")]
        Plin = 4,
        
        
    }
}