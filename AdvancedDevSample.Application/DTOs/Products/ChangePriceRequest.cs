using System.ComponentModel.DataAnnotations;

namespace AdvancedDevSample.Application.DTOs.Products
{
    /// <summary>
    /// Requête de modification de prix d'un produit.
    /// </summary>
    public class ChangePriceRequest
    {
        /// <summary>
        /// Nouveau prix (strictement positif, invariant métier).
        /// </summary>
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
