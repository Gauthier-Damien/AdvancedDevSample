using System.ComponentModel.DataAnnotations;

namespace AdvancedDevSample.Application.DTOs.Products
{
    /// <summary>
    /// Requête pour créer un nouveau produit
    /// </summary>
    public class CreateProductRequest
    {
        /// <summary>
        /// Nom du produit
        /// </summary>
        [Required(ErrorMessage = "Le nom du produit est obligatoire")]
        [StringLength(200, ErrorMessage = "Le nom ne peut pas dépasser 200 caractères")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Description du produit
        /// </summary>
        [StringLength(1000, ErrorMessage = "La description ne peut pas dépasser 1000 caractères")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Prix initial du produit (doit être strictement positif)
        /// </summary>
        [Required(ErrorMessage = "Le prix est obligatoire")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être strictement positif")]
        public decimal Price { get; set; }

        /// <summary>
        /// Taux de TVA (en pourcentage, par exemple 20 pour 20%)
        /// </summary>
        [Range(0, 100, ErrorMessage = "Le taux de TVA doit être entre 0 et 100%")]
        public decimal VATRate { get; set; } = 20; // TVA par défaut à 20%
    }
}
