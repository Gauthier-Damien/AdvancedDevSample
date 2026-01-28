using AdvancedDevSample.Domain.Interfaces.Products;

namespace AdvancedDevSample.Application.Services;

public class ProductService
{
    private readonly IProductRepository _repo;
    
    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }
    public void ChangeProductPrice(Guid productId, decimal newPrice)
    {
        var product = _repo.GetByID(productId) ?? throw new ApplicationException("Product not found");

        product.UpdatePrice(newPrice);
        
        _repo.Save(product);   
    }
}