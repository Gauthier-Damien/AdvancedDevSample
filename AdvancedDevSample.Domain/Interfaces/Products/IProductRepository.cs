using AdvancedDevSample.Domain.Entities;

namespace AdvancedDevSample.Domain.Interfaces.Products
{
    
public interface IProductRepository 
{
    Product GetByID(Guid id); 
    void Save(Product product);
}

}
