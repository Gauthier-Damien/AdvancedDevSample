using AdvancedDevSample.Domain.Entities;

namespace AdvancedDevSample.Domain.Interfaces.Products
{
    /// <summary>
    /// Port (Interface) : Contrat de persistance des produits
    /// Défini dans le Domain, implémenté dans l'Infrastructure
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Récupère un produit par son identifiant
        /// </summary>
        Product? GetByID(Guid id);

        /// <summary>
        /// Récupère tous les produits
        /// </summary>
        IEnumerable<Product> GetAll();

        /// <summary>
        /// Sauvegarde ou met à jour un produit
        /// </summary>
        void Save(Product product);
    }
}
