using System.ComponentModel.DataAnnotations;
namespace AdvancedDevSample.Application.DTOs.Products
{
    /// <summary>
    /// Requête pour appliquer une promotion/réduction sur un produit
    /// </summary>
    public class ApplyDiscountRequest
    {
        /// <summary>
        /// Pourcentage de réduction à appliquer (0.01 à 100)
        /// </summary>
        [Required(ErrorMessage = "Le pourcentage de réduction est obligatoire")]
        [Range(0.01, 100, ErrorMessage = "La réduction doit être entre 0,01 et 100%")]
        public decimal DiscountPercentage { get; set; }
    }
}
