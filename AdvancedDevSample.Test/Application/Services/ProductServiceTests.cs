using AdvancedDevSample.Application.DTOs.Products;
using AdvancedDevSample.Application.Exceptions;
using AdvancedDevSample.Application.Services;
using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Exceptions;
using AdvancedDevSample.Domain.Interfaces.Products;

namespace AdvancedDevSample.Test.Application.Services;

/// <summary>
/// Tests unitaires pour ProductService avec un repository fake
/// </summary>
public class ProductServiceTests
{
    private readonly FakeProductRepository _fakeRepository;
    private readonly ProductService _service;

    public ProductServiceTests()
    {
        _fakeRepository = new FakeProductRepository();
        _service = new ProductService(_fakeRepository);
    }

    [Fact]
    public void GetAllProducts_Should_Return_Only_Active_Products()
    {

        var activeProduct = CreateTestProduct("Active Product", 100, true);
        var inactiveProduct = CreateTestProduct("Inactive Product", 50, false);
        
        _fakeRepository.Add(activeProduct);
        _fakeRepository.Add(inactiveProduct);
        

        var result = _service.GetAllProducts().ToList();
        

        Assert.Single(result);
        Assert.Equal("Active Product", result[0].Name);
    }

    [Fact]
    public void GetProductById_Should_Return_Product_When_Exists()
    {

        var product = CreateTestProduct("Test Product", 100, true);
        _fakeRepository.Add(product);
        

        var result = _service.GetProductById(product.Id);
        

        Assert.NotNull(result);
        Assert.Equal(product.Id, result.Id);
        Assert.Equal("Test Product", result.Name);
    }

    [Fact]
    public void GetProductById_Should_Throw_Exception_When_Not_Found()
    {

        var exception = Assert.Throws<ApplicationServiceException>((Action)(() => 
            _service.GetProductById(Guid.NewGuid())));
        
        Assert.Equal("Produit non trouvé", exception.Message);
    }

    [Fact]
    public void CreateProduct_Should_Create_And_Return_Product()
    {

        var request = new CreateProductRequest
        {
            Name = "New Product",
            Description = "Description",
            Price = 99.99m,
            VATRate = 20m
        };
        

        var result = _service.CreateProduct(request);
        

        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal("New Product", result.Name);
        Assert.Equal(99.99m, result.Price);
        Assert.True((bool)result.IsActive);
    }

    [Fact]
    public void ChangeProductPrice_Should_Update_Price()
    {

        var product = CreateTestProduct("Test Product", 100, true);
        _fakeRepository.Add(product);
        

        var result = _service.ChangeProductPrice(product.Id, 150);
        

        Assert.Equal(150, result.Price);
    }

    [Fact]
    public void ChangeProductPrice_Should_Throw_Exception_When_Product_Not_Found()
    {

        var exception = Assert.Throws<ApplicationServiceException>((Action)(() => 
            _service.ChangeProductPrice(Guid.NewGuid(), 100)));
        
        Assert.Equal("Produit non trouvé", exception.Message);
    }

    [Fact]
    public void ChangeProductPrice_Should_Throw_Exception_When_Product_Inactive()
    {

        var product = CreateTestProduct("Inactive Product", 100, false);
        _fakeRepository.Add(product);
        

        var exception = Assert.Throws<DomainException>((Action)(() => 
            _service.ChangeProductPrice(product.Id, 150)));
        
        Assert.Equal("Impossible de modifier un produit inactif.", exception.Message);
    }

    [Fact]
    public void ApplyDiscount_Should_Reduce_Price_By_Percentage()
    {

        var product = CreateTestProduct("Test Product", 100, true);
        _fakeRepository.Add(product);
        

        var result = _service.ApplyDiscount(product.Id, 25); // 25% de réduction
        

        Assert.Equal(75, result.Price); // 100 - 25% = 75
    }

    [Fact]
    public void ApplyDiscount_Should_Throw_Exception_For_Invalid_Percentage()
    {

        var product = CreateTestProduct("Test Product", 100, true);
        _fakeRepository.Add(product);
        
        //Act & Assert - Pourcentage négatif
        var exception1 = Assert.Throws<DomainException>((Action)(() => 
            _service.ApplyDiscount(product.Id, -10)));
        
        Assert.Equal("Le pourcentage de réduction doit être strictement positif.", exception1.Message);
        
        //Act & Assert - Pourcentage > 100
        var exception2 = Assert.Throws<DomainException>((Action)(() => 
            _service.ApplyDiscount(product.Id, 150)));
        
        Assert.Equal("Le pourcentage de réduction ne peut pas dépasser 100%.", exception2.Message);
    }

    [Fact]
    public void ToggleProductStatus_Should_Change_Status()
    {

        var product = CreateTestProduct("Test Product", 100, true);
        _fakeRepository.Add(product);
        

        var result = _service.ToggleProductStatus(product.Id, false);
        

        Assert.False((bool)result.IsActive);
    }

    [Fact]
    public void DeleteProduct_Should_Set_Product_Inactive()
    {

        var product = CreateTestProduct("Test Product", 100, true);
        _fakeRepository.Add(product);
        

        _service.DeleteProduct(product.Id);
        

        var savedProduct = _fakeRepository.GetByID(product.Id);
        Assert.NotNull(savedProduct);
        Assert.False((bool)savedProduct.IsActive);
    }

    [Fact]
    public void DeleteProduct_Should_Throw_Exception_When_Product_Not_Found()
    {

        var exception = Assert.Throws<ApplicationServiceException>((Action)(() => 
            _service.DeleteProduct(Guid.NewGuid())));
        
        Assert.Equal("Produit non trouvé", exception.Message);
    }

    private Product CreateTestProduct(string name, decimal price, bool isActive)
    {
        var product = new Product(
            Guid.NewGuid(),
            name,
            "Test Description",
            price,
            20m,
            isActive
        );
        return product;
    }
}

/// <summary>
/// Fake Repository pour les tests (InMemory)
/// </summary>
public class FakeProductRepository : IProductRepository
{
    private readonly Dictionary<Guid, Product> _products = new();

    public Product? GetByID(Guid id)
    {
        _products.TryGetValue(id, out var product);
        return product;
    }

    public IEnumerable<Product> GetAll()
    {
        return _products.Values.ToList();
    }

    public void Save(Product product)
    {
        _products[product.Id] = product;
    }

    public void Add(Product product)
    {
        _products[product.Id] = product;
    }

    public void Clear()
    {
        _products.Clear();
    }
}
