using System.ComponentModel.DataAnnotations;

namespace AdvancedDevSample.Application.DTOs.Orders
{
    /// <summary>
    /// Requête de mise à jour des montants d'une commande.
    /// </summary>
    public class UpdateOrderTotalsRequest
    {
        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmountExcludingTax { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmountIncludingTax { get; set; }
    }
}
