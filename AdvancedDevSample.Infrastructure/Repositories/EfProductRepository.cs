using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Interfaces.Products;
using AdvancedDevSample.Infrastructure.Entities;

namespace AdvancedDevSample.Infrastructure.Repositories
{
    
    public class EfProductRepository : IProductRepository
    {
        public Product GetByID(Guid id)
        { 
            ProductEntity product = new() { Id = id, Price = 10, IsActive = true };
            var domainProduct = new Product(product.Id, product.Price, product.IsActive);
            
           return domainProduct;
        }

        public void Save(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
