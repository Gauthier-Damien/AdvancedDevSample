# Bonnes Pratiques de Développement

## Conventions de code

### Nommage

- **Classes** : PascalCase (`ProductService`, `OrderController`)
- **Méthodes** : PascalCase (`GetAllAsync`, `CreateProduct`)
- **Variables/Paramètres** : camelCase (`productId`, `orderDate`)
- **Champs privés** : _camelCase (`_productRepository`, `_logger`)
- **Constantes** : UPPER_CASE (`MAX_RETRY_COUNT`)

### Organisation des fichiers

```
Projet/
├── Entities/           # Entités métier
├── ValueObjects/       # Value Objects
├── Interfaces/         # Interfaces (IRepository, etc.)
├── Services/           # Services applicatifs
├── DTOs/              # Data Transfer Objects
└── Exceptions/        # Exceptions personnalisées
```

## Règles de développement

### 1. Respect de l'architecture Clean Code

❌ **À éviter** :
```csharp
// Dans Domain - INTERDIT
using Microsoft.EntityFrameworkCore;
```

✅ **À faire** :
```csharp
// Domain dépend uniquement de lui-même
namespace AdvancedDevSample.Domain.Entities;
```

### 2. Utilisation des DTOs

❌ **À éviter** :
```csharp
[HttpPost]
public async Task<Product> Create(Product product) // Exposer l'entité
```

✅ **À faire** :
```csharp
[HttpPost]
public async Task<ProductDto> Create(CreateProductDto dto) // Utiliser des DTOs
```

### 3. Gestion des erreurs

❌ **À éviter** :
```csharp
throw new Exception("Erreur");
```

✅ **À faire** :
```csharp
throw new ProductNotFoundException(productId);
```

### 4. Validation métier

❌ **À éviter** :
```csharp
// Validation dans le controller
if (price <= 0)
    return BadRequest();
```

✅ **À faire** :
```csharp
// Validation dans l'entité/Value Object
public Price(decimal value)
{
    if (value <= 0)
        throw new InvalidPriceException("Le prix doit être strictement positif");
    Value = value;
}
```

## Tests

### Structure des tests

```csharp
// Arrange - Act - Assert pattern
[Fact]
public async Task GetAllAsync_ShouldReturnAllProducts()
{
    // Arrange
    var products = CreateTestProducts();
    _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(products);
    
    // Act
    var result = await _service.GetAllAsync();
    
    // Assert
    Assert.Equal(products.Count, result.Count());
}
```

### Nommage des tests

Pattern : `MethodName_Scenario_ExpectedResult`

Exemples :
- `CreateProduct_WithValidData_ShouldReturnProductDto`
- `CreateProduct_WithNegativePrice_ShouldThrowException`
- `GetProduct_WithInvalidId_ShouldReturnNull`

## Commentaires XML

### Pour les classes

```csharp
/// <summary>
/// Service de gestion des produits.
/// Fournit les opérations CRUD pour les entités Product.
/// </summary>
public class ProductService
{
}
```

### Pour les méthodes

```csharp
/// <summary>
/// Récupère tous les produits actifs du catalogue.
/// </summary>
/// <returns>Liste de tous les produits sous forme de DTOs</returns>
/// <exception cref="ApplicationServiceException">En cas d'erreur lors de la récupération</exception>
public async Task<IEnumerable<ProductDto>> GetAllAsync()
{
}
```

### Pour les paramètres

```csharp
/// <summary>
/// Crée un nouveau produit dans le catalogue.
/// </summary>
/// <param name="dto">Données du produit à créer</param>
/// <returns>Le produit créé avec son identifiant généré</returns>
public async Task<ProductDto> CreateAsync(CreateProductDto dto)
{
}
```

## Astuces de performance

### 1. Utiliser Async/Await

```csharp
// Toujours pour les opérations I/O
public async Task<Product> GetByIdAsync(Guid id)
{
    return await _repository.GetByIdAsync(id);
}
```

### 2. Éviter les requêtes N+1

❌ **À éviter** :
```csharp
foreach (var order in orders)
{
    order.User = await _userRepository.GetByIdAsync(order.UserId);
}
```

✅ **À faire** :
```csharp
var orders = await _repository.GetAllWithUsersAsync();
```

### 3. Utiliser IEnumerable pour les lectures seules

```csharp
public async Task<IEnumerable<ProductDto>> GetAllAsync() // Pas List<>
```

## Sécurité

### 1. Validation des entrées

```csharp
// Toujours valider côté serveur
if (string.IsNullOrWhiteSpace(dto.Name))
    throw new ValidationException("Le nom est requis");
```

### 2. Ne pas exposer les détails techniques

❌ **À éviter** :
```csharp
catch (Exception ex)
{
    return StatusCode(500, ex.StackTrace); // Fuite d'informations
}
```

✅ **À faire** :
```csharp
catch (Exception ex)
{
    _logger.LogError(ex, "Erreur lors de la création");
    return StatusCode(500, "Une erreur est survenue");
}
```

## Git - Commits

### Format des messages

```
type(scope): description courte

Description détaillée si nécessaire

Fixes #issue_number
```

### Types

- `feat`: Nouvelle fonctionnalité
- `fix`: Correction de bug
- `docs`: Documentation
- `refactor`: Refactoring
- `test`: Ajout/modification de tests
- `chore`: Tâches diverses

### Exemples

```
feat(products): ajout de la validation du prix

Implémentation de la règle métier : le prix doit être strictement positif.
Ajout de tests unitaires correspondants.

Closes #42
```

## Checklist avant commit

- [ ] Le code compile sans erreur
- [ ] Tous les tests passent
- [ ] Les commentaires XML sont à jour
- [ ] Respect des conventions de nommage
- [ ] Pas de code commenté
- [ ] Pas de `TODO` laissés
- [ ] Architecture respectée (pas de dépendances inversées)

---

*Pour plus d'informations, consulter la [documentation architecture](../architecture/overview.md)*
