# 🔧 Correctifs Prioritaires - AdvancedDevSample

**Date**: 10 février 2026  
**Criticité**: HAUTE  

Ce document liste les corrections urgentes à appliquer immédiatement.

---

## ❌ BUGS CRITIQUES À CORRIGER

### 1. ExceptionHandlingMiddleware Non Enregistré

**Fichier**: `AdvancedDevSample.API/Program.cs`  
**Ligne**: Après `var app = builder.Build();`

**Problème**: Le middleware existe mais n'est jamais utilisé.

**Correction**:
```csharp
var app = builder.Build();

// Ajouter cette ligne AVANT tous les autres middlewares
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configuration du pipeline HTTP - ordre d'exécution des middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

**Vérification**:
```powershell
# Lancer l'API et tester une erreur
curl -X POST https://localhost:5181/api/product `
  -H "Content-Type: application/json" `
  -d '{"name":"Test","price":-10,"vatRate":20}'

# Vous devriez recevoir: {"error":"Erreur metier","detail":"Le prix doit être strictement positif."}
```

---

### 2. Opérateurs == et != Manquants dans Price

**Fichier**: `AdvancedDevSample.Domain/ValueObjects/Price.cs`  
**Ligne**: Après la méthode `GetHashCode()`

**Problème**: Le test `Equals_Should_Return_True_For_Same_Values` échoue.

**Correction**:
```csharp
public override int GetHashCode()
{
    return HashCode.Combine(AmountExcludingTax, VATRate);
}

// AJOUTER CES DEUX OPÉRATEURS
public static bool operator ==(Price? left, Price? right)
{
    if (left is null && right is null)
        return true;
    if (left is null || right is null)
        return false;
    return left.Equals(right);
}

public static bool operator !=(Price? left, Price? right)
{
    return !(left == right);
}

public override string ToString()
{
    return $"{AmountExcludingTax:C} HT (TVA {VATRate}%) = {AmountIncludingTax:C} TTC";
}
```

**Vérification**:
```powershell
dotnet test --filter "Equals_Should_Return_True_For_Same_Values"
# Doit passer au vert ✅
```

---

### 3. Validation des DTOs Manquante

**Fichiers**: Tous les DTOs dans `AdvancedDevSample.Application/DTOs/`

**Problème**: Pas de validation des entrées utilisateur.

#### 3.1 CreateProductRequest.cs
```csharp
using System.ComponentModel.DataAnnotations;

namespace AdvancedDevSample.Application.DTOs.Products
{
    public class CreateProductRequest
    {
        [Required(ErrorMessage = "Le nom du produit est obligatoire")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Le nom doit contenir entre 2 et 200 caractères")]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "La description ne peut pas dépasser 1000 caractères")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être strictement positif")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Le taux de TVA doit être entre 0 et 100%")]
        public decimal VATRate { get; set; }
    }
}
```

#### 3.2 CreateUserRequest.cs
```csharp
using System.ComponentModel.DataAnnotations;

namespace AdvancedDevSample.Application.DTOs.Users
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Le nom d'utilisateur est obligatoire")]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nom de famille est obligatoire")]
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(50)]
        public string Role { get; set; } = "User";
    }
}
```

#### 3.3 CreateOrderRequest.cs
```csharp
using System.ComponentModel.DataAnnotations;

namespace AdvancedDevSample.Application.DTOs.Orders
{
    public class CreateOrderRequest
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "L'adresse de livraison est obligatoire")]
        [StringLength(500, MinimumLength = 10)]
        public string DeliveryAddress { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;
    }
}
```

#### 3.4 CreateSupplierRequest.cs
```csharp
using System.ComponentModel.DataAnnotations;

namespace AdvancedDevSample.Application.DTOs.Suppliers
{
    public class CreateSupplierRequest
    {
        [Required(ErrorMessage = "Le nom du fournisseur est obligatoire")]
        [StringLength(200, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Le numéro de téléphone n'est pas valide")]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(500)]
        public string Address { get; set; } = string.Empty;
    }
}
```

**Vérification**:
```powershell
# Test avec des données invalides
curl -X POST https://localhost:5181/api/product `
  -H "Content-Type: application/json" `
  -d '{"name":"","price":0,"vatRate":150}'

# Devrait retourner une erreur de validation 400 Bad Request
```

---

### 4. Amélioration Validation Email (Domain)

**Fichiers**: `User.cs`, `Supplier.cs`

**Problème**: `email.Contains("@")` est trop basique.

**Fichier**: `AdvancedDevSample.Domain/Entities/User.cs`

```csharp
using AdvancedDevSample.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace AdvancedDevSample.Domain.Entities
{
    public class User
    {
        // ...existing code...

        public User(Guid id, string username, string email, string firstName, string lastName, string role = "User")
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new DomainException("Le nom d'utilisateur est obligatoire.");

            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("L'email est obligatoire.");

            // REMPLACER cette ligne:
            // if (!email.Contains("@"))
            //     throw new DomainException("L'email n'est pas valide.");
            
            // PAR celle-ci:
            if (!IsValidEmail(email))
                throw new DomainException("L'email n'est pas valide.");

            // ...existing code...
        }

        public void UpdateInfo(string username, string email, string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new DomainException("Le nom d'utilisateur est obligatoire.");

            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("L'email est obligatoire.");

            // MÊME CHANGEMENT ICI
            if (!IsValidEmail(email))
                throw new DomainException("L'email n'est pas valide.");

            // ...existing code...
        }

        // AJOUTER CETTE MÉTHODE
        private static bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        // ...existing code...
    }
}
```

**Même modification dans `Supplier.cs`**

---

### 5. Correction Random dans Order.cs

**Fichier**: `AdvancedDevSample.Domain/Entities/Order.cs`  
**Ligne**: Méthode `GenerateOrderNumber()`

**Problème**: `new Random()` à chaque appel peut générer les mêmes valeurs.

```csharp
namespace AdvancedDevSample.Domain.Entities
{
    public class Order
    {
        // ...existing code...

        // AJOUTER ce champ statique au début de la classe
        private static readonly Random _random = new();

        // MODIFIER cette méthode
        private static string GenerateOrderNumber()
        {
            var date = DateTime.UtcNow;
            var random = _random.Next(1000, 9999);
            var guid = Guid.NewGuid().ToString("N")[..6]; // 6 premiers caractères du GUID
            return $"ORD-{date:yyyyMMdd}-{random}-{guid}";
        }
    }
}
```

---

### 6. Thread-Safety des Repositories

**Fichiers**: Tous les `Ef*Repository.cs`

**Problème**: `Dictionary<>` n'est pas thread-safe.

**Exemple pour `EfProductRepository.cs`**:
```csharp
using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Interfaces.Products;
using System.Collections.Concurrent;

namespace AdvancedDevSample.Infrastructure.Repositories
{
    public class EfProductRepository : IProductRepository
    {
        // REMPLACER cette ligne:
        // private static readonly Dictionary<Guid, Product> _products = new();
        
        // PAR celle-ci:
        private static readonly ConcurrentDictionary<Guid, Product> _products = new();

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
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _products[product.Id] = product;
        }
    }
}
```

**Appliquer le même changement pour**:
- `EfOrderRepository.cs`
- `EfUserRepository.cs`
- `EfSupplierRepository.cs`

---

## ✅ VÉRIFICATION FINALE

### Checklist
```powershell
# 1. Compiler la solution
dotnet build

# 2. Exécuter tous les tests
dotnet test

# 3. Vérifier qu'il n'y a plus d'échecs
# ✅ Tous les tests doivent passer

# 4. Lancer l'API
cd AdvancedDevSample.API
dotnet run

# 5. Tester Swagger
# Ouvrir https://localhost:5181/swagger

# 6. Tester la gestion d'erreur
curl -X POST https://localhost:5181/api/product `
  -H "Content-Type: application/json" `
  -d '{"name":"Test","price":-10,"vatRate":20}'

# Résultat attendu: 
# {"error":"Erreur metier","detail":"Le prix doit être strictement positif."}
```

---

## 📊 Impact Estimé

| Correctif | Temps | Criticité | Impact |
|-----------|-------|-----------|--------|
| 1. Middleware | 2 min | 🔴 Critique | Sécurité +++ |
| 2. Opérateurs Price | 5 min | 🔴 Critique | Tests passent |
| 3. Validation DTOs | 20 min | 🔴 Critique | Sécurité +++ |
| 4. Email Validation | 10 min | 🟡 Important | Qualité ++ |
| 5. Random Order | 5 min | 🟡 Important | Fiabilité + |
| 6. Thread-Safety | 10 min | 🟡 Important | Stabilité ++ |
| **TOTAL** | **~1h** | | **Production-ready** |

---

## 🚀 Prochaines Étapes (Post-Correctifs)

Après avoir appliqué ces correctifs:

1. **Migration vers EF Core** avec SQL Server ou PostgreSQL
2. **Authentification JWT** pour sécuriser l'API
3. **Tests d'intégration** pour couvrir les scénarios end-to-end
4. **Pagination** pour les endpoints GET
5. **Docker** et docker-compose pour faciliter le déploiement

---

**Note**: Ces correctifs sont essentiels avant toute mise en production.
