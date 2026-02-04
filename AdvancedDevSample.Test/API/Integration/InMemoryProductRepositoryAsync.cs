using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Interfaces.Products;

namespace AdvancedDevSample.Test.API.Integration
{
    

    public class InMemoryProductRepositoryAsync : IProductRepositoryAsync
    {
        private readonly Dictionary<Guid, Product> _store = new();
        
        public Task<Product?> GetByIdAsync(Guid id)
        => Task.FromResult(_store.TryGetValue(id, out var product) ? product: null);
        
        public Task SaveAsync(Product product)
        {
            _store[product.Id] = product;
            return Task.CompletedTask;
        }

        public void Seed(Product product)
        => _store[product.Id] = product;
    }
}
