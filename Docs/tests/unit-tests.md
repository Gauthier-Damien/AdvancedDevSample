# Tests - Tests Unitaires

## Vue d'ensemble

Les **tests unitaires** vérifient le comportement des composants individuels en isolation.

## Framework : xUnit

Le projet utilise **xUnit** comme framework de tests.

```xml
<PackageReference Include="xunit" Version="2.4.2" />
<PackageReference Include="xunit.runner.visualstudio" Version="2.4.5" />
<PackageReference Include="Moq" Version="4.18.4" />
```

## Structure des tests

```
AdvancedDevSample.Test/
├── Domain/               # Tests du Domain
│   ├── ProductTests.cs
│   ├── PriceTests.cs
│   └── VATTests.cs
├── Application/          # Tests de l'Application
│   ├── ProductServiceTests.cs
│   ├── SupplierServiceTests.cs
│   └── UserServiceTests.cs
└── API/                 # Tests de l'API
    └── Integration/
```

## Tests du Domain

### Tests des Value Objects

```csharp
public class PriceTests
{
    [Fact]
    public void Constructor_WithPositiveValue_ShouldCreatePrice()
    {
        // Arrange
        decimal value = 100m;
        
        // Act
        var price = new Price(value);
        
        // Assert
        Assert.Equal(value, price.Value);
    }
    
    [Fact]
    public void Constructor_WithNegativeValue_ShouldThrowException()
    {
        // Arrange
        decimal value = -50m;
        
        // Act & Assert
        Assert.Throws<InvalidPriceException>(() => new Price(value));
    }
    
    [Fact]
    public void Constructor_WithZero_ShouldThrowException()
    {
        // Arrange
        decimal value = 0m;
        
        // Act & Assert
        Assert.Throws<InvalidPriceException>(() => new Price(value));
    }
}
```

### Tests des Entités

```csharp
public class ProductTests
{
    [Fact]
    public void UpdatePrice_WithValidPrice_ShouldUpdatePrice()
    {
        // Arrange
        var product = CreateTestProduct();
        var newPrice = new Price(200m);
        
        // Act
        product.UpdatePrice(newPrice);
        
        // Assert
        Assert.Equal(200m, product.Price.Value);
    }
    
    [Fact]
    public void Activate_ShouldSetIsActiveToTrue()
    {
        // Arrange
        var product = CreateTestProduct();
        product.Deactivate();
        
        // Act
        product.Activate();
        
        // Assert
        Assert.True(product.IsActive);
    }
    
    private static Product CreateTestProduct()
    {
        return new Product(
            "Test Product",
            "Description",
            new Price(100m),
            new VAT(20m),
            Guid.NewGuid()
        );
    }
}
```

## Tests de l'Application

### Tests des Services avec Moq

```csharp
public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _mockRepository;
    private readonly ProductService _service;
    
    public ProductServiceTests()
    {
        _mockRepository = new Mock<IProductRepository>();
        _service = new ProductService(_mockRepository.Object);
    }
    
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            CreateTestProduct("Product 1"),
            CreateTestProduct("Product 2")
        };
        
        _mockRepository.Setup(r => r.GetAllAsync())
            .ReturnsAsync(products);
        
        // Act
        var result = await _service.GetAllAsync();
        
        // Assert
        Assert.Equal(2, result.Count());
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }
    
    [Fact]
    public async Task CreateAsync_WithValidData_ShouldCreateProduct()
    {
        // Arrange
        var dto = new CreateProductDto(
            "Laptop",
            "High performance",
            1299.99m,
            20m,
            Guid.NewGuid()
        );
        
        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Product>()))
            .Returns(Task.CompletedTask);
        
        // Act
        var result = await _service.CreateAsync(dto);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal("Laptop", result.Name);
        Assert.Equal(1299.99m, result.Price);
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
    }
    
    [Fact]
    public async Task CreateAsync_WithNegativePrice_ShouldThrowException()
    {
        // Arrange
        var dto = new CreateProductDto(
            "Laptop",
            "Description",
            -100m,  // Prix négatif
            20m,
            Guid.NewGuid()
        );
        
        // Act & Assert
        await Assert.ThrowsAsync<InvalidPriceException>(
            () => _service.CreateAsync(dto)
        );
        
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never);
    }
    
    private static Product CreateTestProduct(string name)
    {
        return new Product(
            name,
            "Description",
            new Price(100m),
            new VAT(20m),
            Guid.NewGuid()
        );
    }
}
```

## Pattern AAA (Arrange-Act-Assert)

Tous les tests suivent le pattern **AAA** :

```csharp
[Fact]
public void MethodName_Scenario_ExpectedResult()
{
    // Arrange - Préparer les données et dépendances
    var product = CreateTestProduct();
    var newPrice = new Price(200m);
    
    // Act - Exécuter l'action à tester
    product.UpdatePrice(newPrice);
    
    // Assert - Vérifier le résultat
    Assert.Equal(200m, product.Price.Value);
}
```

## Moq - Mocking des dépendances

### Setup d'une méthode

```csharp
_mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
    .ReturnsAsync(testProduct);
```

### Verify qu'une méthode a été appelée

```csharp
_mockRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
_mockRepository.Verify(r => r.DeleteAsync(productId), Times.Never);
```

### Setup avec condition

```csharp
_mockRepository.Setup(r => r.GetByIdAsync(It.Is<Guid>(id => id == productId)))
    .ReturnsAsync(testProduct);
```

## Exécution des tests

### Tous les tests

```bash
dotnet test
```

### Tests d'un projet spécifique

```bash
dotnet test AdvancedDevSample.Test/AdvancedDevSample.Test.csproj
```

### Tests avec couverture

```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Tests avec filtre

```bash
dotnet test --filter "FullyQualifiedName~ProductServiceTests"
```

## Assertions xUnit

```csharp
// Égalité
Assert.Equal(expected, actual);
Assert.NotEqual(notExpected, actual);

// Booléens
Assert.True(condition);
Assert.False(condition);

// Nullité
Assert.Null(value);
Assert.NotNull(value);

// Collections
Assert.Empty(collection);
Assert.NotEmpty(collection);
Assert.Contains(item, collection);

// Exceptions
Assert.Throws<InvalidPriceException>(() => new Price(-50));
await Assert.ThrowsAsync<ProductNotFoundException>(() => service.GetByIdAsync(id));

// Types
Assert.IsType<ProductDto>(result);
Assert.IsAssignableFrom<IEnumerable<ProductDto>>(result);
```

## Best Practices

### ✅ À faire

- **Un test = un comportement**
- Nommage clair : `MethodName_Scenario_ExpectedResult`
- Pattern AAA systématique
- Tests indépendants (pas d'ordre d'exécution)
- Mock des dépendances externes

### ❌ À éviter

- Tests qui dépendent les uns des autres
- Tests avec logique complexe
- Tests qui accèdent à une vraie base de données
- Assertions multiples sans rapport

## Navigation

- [Retour aux Tests →](../index.md#tests)
- [Tests d'intégration →](integration-tests.md)
