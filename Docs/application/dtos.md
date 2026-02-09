# Application - DTOs (Data Transfer Objects)

## Vue d'ensemble

Les **DTOs** sont des objets simples utilisés pour transférer des données entre les couches de l'application.

## Pourquoi des DTOs ?

1. **Isolation du Domain** : Les entités ne sont pas exposées directement
2. **Simplicité** : Structures plates, faciles à sérialiser
3. **Versioning** : Possibilité d'avoir plusieurs versions d'API
4. **Performance** : Sélection des champs nécessaires uniquement

## Structure

```
DTOs/
├── Common/            # DTOs partagés
├── Products/          # DTOs produits
│   ├── ProductDto.cs
│   ├── CreateProductDto.cs
│   └── UpdateProductDto.cs
├── Suppliers/         # DTOs fournisseurs
├── Users/             # DTOs utilisateurs
└── Orders/            # DTOs commandes
```

## DTOs Products

### ProductDto

DTO de retour pour un produit.

```csharp
/// <summary>
/// Représente un produit du catalogue
/// </summary>
public record ProductDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    decimal VatRate,
    bool IsActive,
    Guid SupplierId
);
```

### CreateProductDto

DTO pour créer un nouveau produit.

```csharp
/// <summary>
/// Données nécessaires pour créer un produit
/// </summary>
public record CreateProductDto(
    [Required] string Name,
    [StringLength(500)] string Description,
    [Range(0.01, double.MaxValue)] decimal Price,
    [Range(0, 100)] decimal VatRate,
    Guid SupplierId
);
```

### UpdateProductDto

DTO pour mettre à jour un produit.

```csharp
/// <summary>
/// Données pour mettre à jour un produit
/// </summary>
public record UpdateProductDto(
    string? Name,
    string? Description,
    decimal? Price,
    decimal? VatRate,
    bool? IsActive
);
```

## DTOs Suppliers

```csharp
public record SupplierDto(
    Guid Id,
    string Name,
    string Email,
    string Phone
);

public record CreateSupplierDto(
    [Required] string Name,
    [Required][EmailAddress] string Email,
    [Phone] string Phone
);

public record UpdateSupplierDto(
    string? Name,
    string? Email,
    string? Phone
);
```

## DTOs Users

```csharp
public record UserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email
);

public record CreateUserDto(
    [Required] string FirstName,
    [Required] string LastName,
    [Required][EmailAddress] string Email
);

public record UpdateUserDto(
    string? FirstName,
    string? LastName,
    string? Email
);
```

## DTOs Orders

### OrderDto

```csharp
public record OrderDto(
    Guid Id,
    Guid UserId,
    DateTime OrderDate,
    IEnumerable<OrderLineDto> OrderLines,
    decimal TotalAmount
);
```

### CreateOrderDto

```csharp
public record CreateOrderDto(
    Guid UserId,
    DateTime OrderDate,
    IEnumerable<CreateOrderLineDto> OrderLines
);
```

### OrderLineDto

```csharp
public record OrderLineDto(
    Guid Id,
    Guid ProductId,
    int Quantity,
    decimal UnitPrice
);

public record CreateOrderLineDto(
    Guid ProductId,
    [Range(1, int.MaxValue)] int Quantity,
    [Range(0.01, double.MaxValue)] decimal UnitPrice
);
```

## Records vs Classes

### Records (recommandé pour DTOs)

```csharp
// Immutable par défaut
public record ProductDto(Guid Id, string Name, decimal Price);

// Égalité par valeur automatique
var dto1 = new ProductDto(id, "Laptop", 1000);
var dto2 = new ProductDto(id, "Laptop", 1000);
Console.WriteLine(dto1 == dto2);  // True

// with expression pour copie modifiée
var dto3 = dto1 with { Price = 1200 };
```

### Classes (si nécessaire)

```csharp
public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

## Validation avec Data Annotations

```csharp
public record CreateProductDto(
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Le nom doit contenir entre 3 et 100 caractères")]
    string Name,
    
    [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
    string Description,
    
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être strictement positif")]
    decimal Price,
    
    [Required]
    [Range(0, 100, ErrorMessage = "Le taux de TVA doit être entre 0 et 100")]
    decimal VatRate,
    
    [Required]
    Guid SupplierId
);
```

## Mapping Entity ↔ DTO

### Manuellement (dans les services)

```csharp
// Entity → DTO
private static ProductDto MapToDto(Product product)
{
    return new ProductDto(
        product.Id,
        product.Name,
        product.Description,
        product.Price.Value,
        product.Vat.Rate,
        product.IsActive,
        product.SupplierId
    );
}

// DTO → Entity
private static Product MapToEntity(CreateProductDto dto)
{
    return new Product(
        dto.Name,
        dto.Description,
        new Price(dto.Price),
        new VAT(dto.VatRate),
        dto.SupplierId
    );
}
```

### Avec AutoMapper (optionnel)

```csharp
// Configuration
CreateMap<Product, ProductDto>()
    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Value))
    .ForMember(dest => dest.VatRate, opt => opt.MapFrom(src => src.Vat.Rate));

// Utilisation
var dto = _mapper.Map<ProductDto>(product);
```

## DTOs vs Entities

| DTO | Entity |
|-----|--------|
| Transfert de données | Logique métier |
| Structure plate | Structure riche |
| Pas de comportement | Comportements métier |
| Sérialisable JSON | Pas nécessairement sérialisable |
| Validation de forme | Validation métier |

## Pattern CQRS (optionnel)

Séparation des DTOs de lecture et d'écriture :

```csharp
// Commandes (écriture)
public record CreateProductCommand(string Name, decimal Price);
public record UpdateProductCommand(Guid Id, decimal Price);

// Queries (lecture)
public record ProductQuery(Guid Id);
public record ProductListQuery(int PageSize, int PageNumber);

// Résultats
public record ProductResult(Guid Id, string Name, decimal Price);
```

## Best Practices

### ✅ À faire

- Utiliser des **records** pour l'immuabilité
- Ajouter des **attributs de validation**
- Documenter avec des **commentaires XML**
- Créer des DTOs **spécifiques** par opération (Create, Update, etc.)

### ❌ À éviter

- Exposer directement les entités Domain
- DTOs avec logique métier
- DTOs mutables si non nécessaire
- Réutiliser le même DTO pour toutes les opérations

## Navigation

- [Retour à Application →](../architecture/application.md)
- [Services →](services.md)
- [Validation (API) →](../api/introduction.md#validation)
