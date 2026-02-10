using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Interfaces.Products;
using System.Collections.Concurrent;

namespace AdvancedDevSample.Infrastructure.Repositories
{
    /// <summary>
    /// Adapter : implémente IProductRepository (Port du Domain) avec stockage InMemory.
    /// Production : remplacer par Entity Framework avec base de données réelle.
    /// </summary>
    public class EfProductRepository : IProductRepository
    {
        // Stockage InMemory - simulation de base de données (thread-safe)
        private static readonly ConcurrentDictionary<Guid, Product> _products = new();

        public Product? GetByID(Guid id)
        {
            _products.TryGetValue(id, out var product);
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            return _products.Values.ToList();
        }

        /// <summary>
        /// Upsert : ajoute ou met à jour le produit.
        /// </summary>
        public void Save(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _products[product.Id] = product;
        }
    }
}
