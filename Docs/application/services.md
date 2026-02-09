# Application - Services
- [Exceptions →](exceptions.md)
- [DTOs →](dtos.md)
- [Retour à Application →](../architecture/application.md)

## Navigation

```
}
    }
        );
            () => service.CreateAsync(dto)
        await Assert.ThrowsAsync<InvalidPriceException>(
        // Act & Assert
        
        var dto = new CreateProductDto("Laptop", "HP", -100, 20, Guid.NewGuid());
        var service = new ProductService(mockRepo.Object);
        var mockRepo = new Mock<IProductRepository>();
        // Arrange
    {
    public async Task CreateAsync_WithNegativePrice_ShouldThrowException()
    [Fact]
    
    }
        mockRepo.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
        Assert.Equal("Laptop", result.Name);
        Assert.NotNull(result);
        // Assert
        
        var result = await service.CreateAsync(dto);
        // Act
        
        var dto = new CreateProductDto("Laptop", "HP", 1000, 20, Guid.NewGuid());
        var service = new ProductService(mockRepo.Object);
        var mockRepo = new Mock<IProductRepository>();
        // Arrange
    {
    public async Task CreateAsync_WithValidData_ShouldReturnProductDto()
    [Fact]
{
public class ProductServiceTests
```csharp

## Tests des services

```
}
    }
        throw;
        // Propager les exceptions métier
    {
    catch (DomainException)
    }
        return MapToDto(product);
        await _productRepository.UpdateAsync(product);
        
        product.UpdatePrice(newPrice);
        var newPrice = new Price(dto.Price);  // Peut lever InvalidPriceException
        // La validation est dans Price
    {
    try
    
        throw new ProductNotFoundException(id);
    if (product == null)
    // Not Found
    
    var product = await _productRepository.GetByIdAsync(id);
{
public async Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto dto)
```csharp

Les services propagent les exceptions du Domain :

## Gestion des erreurs

```
}
    return MapToDto(product);
    await _productRepository.AddAsync(product);
    
    );
        dto.SupplierId
        vat,
        price,
        dto.Description,
        dto.Name,
    var product = new Product(
    // Création de l'entité
    
    var vat = new VAT(dto.VatRate);
    var price = new Price(dto.Price);
    // Reconstruction des Value Objects
{
public async Task<ProductDto> CreateAsync(CreateProductDto dto)
```csharp

### DTO → Entity (écriture)

```
}
    );
        product.SupplierId
        product.IsActive,
        product.Vat.Rate,         // Extraction du taux
        product.Price.Value,     // Extraction de la valeur
        product.Description,
        product.Name,
        product.Id,
    return new ProductDto(
{
private static ProductDto MapToDto(Product product)
```csharp

### Entity → DTO (lecture)

## Mapping Entity ↔ DTO

```
    SVC -->|Utilise| VO[Value Object]
    SVC -->|Crée| DTO[DTO]
    
    SVC --> REPO[IRepository]
    SVC --> ENT[Entity Domain]
    CTRL[Controller] --> SVC[Service]
graph TB
```mermaid

## Pattern utilisé : Service Layer

5. **Application des règles métier** de haut niveau
4. **Coordination** de transactions complexes
3. **Transformation** Entity ↔ DTO
2. **Orchestration** des entités et repositories
1. **Validation métier** (en complément de la validation Domain)

Les services sont responsables de :

## Responsabilités

```
}
    public async Task<IEnumerable<OrderDto>> GetByUserIdAsync(Guid userId);
    // Méthodes spécifiques
    
    public async Task DeleteAsync(Guid id);
    public async Task<OrderDto> UpdateAsync(Guid id, UpdateOrderDto dto);
    public async Task<OrderDto> CreateAsync(CreateOrderDto dto);
    public async Task<OrderDto?> GetByIdAsync(Guid id);
    public async Task<IEnumerable<OrderDto>> GetAllAsync();
    
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
{
public class OrderService
```csharp

Gestion des commandes avec lignes de commande.

### OrderService

```
}
    public async Task DeleteAsync(Guid id);
    public async Task<UserDto> UpdateAsync(Guid id, UpdateUserDto dto);
    public async Task<UserDto> CreateAsync(CreateUserDto dto);
    public async Task<UserDto?> GetByIdAsync(Guid id);
    public async Task<IEnumerable<UserDto>> GetAllAsync();
    
    private readonly IUserRepository _userRepository;
{
public class UserService
```csharp

Gestion des utilisateurs.

### UserService

```
}
    public async Task DeleteAsync(Guid id);
    public async Task<SupplierDto> UpdateAsync(Guid id, UpdateSupplierDto dto);
    public async Task<SupplierDto> CreateAsync(CreateSupplierDto dto);
    public async Task<SupplierDto?> GetByIdAsync(Guid id);
    public async Task<IEnumerable<SupplierDto>> GetAllAsync();
    
    private readonly ISupplierRepository _supplierRepository;
{
public class SupplierService
```csharp

Gestion des fournisseurs.

### SupplierService

```
}
    }
        );
            product.SupplierId
            product.IsActive,
            product.Vat.Rate,
            product.Price.Value,
            product.Description,
            product.Name,
            product.Id,
        return new ProductDto(
    {
    private static ProductDto MapToDto(Product product)
    // Mapping Entity → DTO
    
    }
        await _productRepository.DeleteAsync(id);
        
            throw new ProductNotFoundException(id);
        if (product == null)
        var product = await _productRepository.GetByIdAsync(id);
    {
    public async Task DeleteAsync(Guid id)
    /// </summary>
    /// Supprime un produit
    /// <summary>
    
    }
        return MapToDto(product);
        await _productRepository.UpdateAsync(product);
        
        }
            product.UpdatePrice(newPrice);
            var newPrice = new Price(dto.Price.Value);
        {
        if (dto.Price.HasValue)
        // Mise à jour via méthodes métier
        
            throw new ProductNotFoundException(id);
        if (product == null)
        var product = await _productRepository.GetByIdAsync(id);
    {
    public async Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto dto)
    /// </summary>
    /// Met à jour un produit existant
    /// <summary>
    
    }
        return MapToDto(product);
        // Retourner le DTO
        
        await _productRepository.AddAsync(product);
        // Persister
        
        );
            dto.SupplierId
            vat,
            price,
            dto.Description,
            dto.Name,
        var product = new Product(
        // Créer l'entité Domain
        
        var vat = new VAT(dto.VatRate);
        var price = new Price(dto.Price);
        // Créer les Value Objects
    {
    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    /// </summary>
    /// Crée un nouveau produit
    /// <summary>
    
    }
        return product != null ? MapToDto(product) : null;
        var product = await _productRepository.GetByIdAsync(id);
    {
    public async Task<ProductDto?> GetByIdAsync(Guid id)
    /// </summary>
    /// Récupère un produit par son identifiant
    /// <summary>
    
    }
        return products.Select(MapToDto);
        var products = await _productRepository.GetAllAsync();
    {
    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    /// </summary>
    /// Récupère tous les produits du catalogue
    /// <summary>
    
    }
        _productRepository = productRepository;
    {
    public ProductService(IProductRepository productRepository)
    
    private readonly IProductRepository _productRepository;
{
public class ProductService
```csharp

Gestion complète du catalogue produits.

### ProductService

## Services disponibles

Les **services applicatifs** orchestrent la logique métier en coordonnant les entités du Domain et les repositories.

## Vue d'ensemble

