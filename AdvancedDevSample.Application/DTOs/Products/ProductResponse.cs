namespace AdvancedDevSample.Application.DTOs.Products
{
    /// <summary>
    /// DTO de réponse contenant les informations d'un produit
    /// </summary>
    public class ProductResponse
    {
        /// <summary>
        /// Identifiant unique du produit
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nom du produit
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Description du produit
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Prix du produit (strictement positif)
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Taux de TVA appliqué (en pourcentage)
        /// </summary>
        public decimal VATRate { get; set; }

        /// <summary>
        /// Statut du produit (actif/inactif)
        /// </summary>
        public bool IsActive { get; set; }
    }
}
