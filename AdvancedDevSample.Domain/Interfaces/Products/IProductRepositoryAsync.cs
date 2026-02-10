using AdvancedDevSample.Domain.Entities;

namespace AdvancedDevSample.Domain.Interfaces.Products;

/// <summary>
/// Port (Interface) : Contrat de persistance asynchrone des produits
/// Utilisé pour les tests d'intégration et les opérations async
/// </summary>
public interface IProductRepositoryAsync
{
    Task<Product?> GetByIdAsync(Guid id);
    Task SaveAsync(Product product);
}