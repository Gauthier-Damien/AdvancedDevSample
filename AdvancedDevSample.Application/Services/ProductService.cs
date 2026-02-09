using AdvancedDevSample.Application.DTOs.Products;
using AdvancedDevSample.Application.Exceptions;
using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Interfaces.Products;

namespace AdvancedDevSample.Application.Services;

/// <summary>
/// Orchestration des cas d'usage liés au catalogue produit.
/// Délègue les règles métier à la couche Domain.
/// </summary>
public class ProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<ProductResponse> GetAllProducts()
    {
        var products = _repo.GetAll();
        return products.Where(p => p.IsActive).Select(MapToResponse);
    }

    public ProductResponse GetProductById(Guid productId)
    {
        var product = GetProduct(productId);
        return MapToResponse(product);
    }

    public ProductResponse CreateProduct(CreateProductRequest request)
    {
        var product = new Product(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.Price,
            request.VATRate,
            true
        );

        _repo.Save(product);
        return MapToResponse(product);
    }

    /// <summary>
    /// Modifie le prix d'un produit.
    /// La validation métier (prix > 0) est effectuée par le Domain.
    /// </summary>
    public ProductResponse ChangeProductPrice(Guid productId, decimal newPrice)
    {
        var product = GetProduct(productId);
        product.UpdatePrice(newPrice);
        _repo.Save(product);
        return MapToResponse(product);
    }

    /// <summary>
    /// Applique une réduction en pourcentage.
    /// Le prix résultant doit rester strictement positif (invariant vérifié par le Domain).
    /// </summary>
    public ProductResponse ApplyDiscount(Guid productId, decimal discountPercentage)
    {
        var product = GetProduct(productId);
        product.ApplyDiscount(discountPercentage);
        _repo.Save(product);
        return MapToResponse(product);
    }

    public ProductResponse ToggleProductStatus(Guid productId, bool isActive)
    {
        var product = GetProduct(productId);
        product.ChangeIsActive(isActive);
        _repo.Save(product);
        return MapToResponse(product);
    }

    /// <summary>
    /// Soft delete : désactive le produit au lieu de le supprimer de la base.
    /// </summary>
    public void DeleteProduct(Guid productId)
    {
        var product = GetProduct(productId);
        product.ChangeIsActive(false);
        _repo.Save(product);
    }

    private Product GetProduct(Guid productId)
    {
        return _repo.GetByID(productId)
            ?? throw new ApplicationServiceException("Produit non trouvé", System.Net.HttpStatusCode.NotFound);
    }

    private ProductResponse MapToResponse(Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            VATRate = product.VATRate,
            IsActive = product.IsActive
        };
    }
}

