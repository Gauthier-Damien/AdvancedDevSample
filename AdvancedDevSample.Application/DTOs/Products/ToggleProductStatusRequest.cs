using System.ComponentModel.DataAnnotations;
namespace AdvancedDevSample.Application.DTOs.Products
{
    /// <summary>
    /// Requête pour activer ou désactiver un produit
    /// </summary>
    public class ToggleProductStatusRequest
    {
        /// <summary>
        /// Nouveau statut du produit (true = actif, false = inactif)
        /// </summary>
        [Required(ErrorMessage = "Le statut du produit est obligatoire")]
        public bool IsActive { get; set; }
    }
}
