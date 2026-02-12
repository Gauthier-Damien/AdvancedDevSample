# Architecture du projet

## 🏗️ Vue d'ensemble

Le projet AdvancedDevSample implémente une **Clean Architecture** (Architecture Hexagonale) avec une séparation stricte des responsabilités en 4 couches principales.

## 📊 Diagramme des couches

```
┌─────────────────────────────────────────┐
│           API (Présentation)            │
│  - Controllers                          │
│  - Middlewares                          │
│  - Configuration                        │
└──────────────┬──────────────────────────┘
               │
┌──────────────▼──────────────────────────┐
│        Application (Services)           │
│  - Services métier                      │
│  - DTOs                                 │
│  - Orchestration                        │
└──────────────┬──────────────────────────┘
               │
┌──────────────▼──────────────────────────┐
│          Domain (Métier)                │
│  - Entités                              │
│  - Règles métier                        │
│  - Interfaces                           │
│  - Value Objects                        │
└──────────────┬──────────────────────────┘
               │
┌──────────────▼──────────────────────────┐
│      Infrastructure (Persistance)       │
│  - Repositories                         │
│  - Implémentation persistance           │
└─────────────────────────────────────────┘
```

## 🎯 Principes appliqués

### 1. Separation of Concerns
Chaque couche a une responsabilité unique et bien définie.

### 2. Dependency Inversion
- Les couches internes (Domain) ne dépendent jamais des couches externes
- Les dépendances pointent toujours vers l'intérieur
- Utilisation d'interfaces pour l'inversion de dépendances

### 3. Single Responsibility Principle
- Une classe = une responsabilité
- Entités métier séparées des DTOs
- Services séparés par domaine fonctionnel

## 📦 Description des couches

### API (AdvancedDevSample.API)

**Responsabilités :**
- Point d'entrée HTTP de l'application
- Gestion des requêtes et réponses
- Configuration de l'injection de dépendances
- Middlewares de gestion d'erreurs
- Documentation Swagger/OpenAPI

**Composants :**
- `Controllers/` : Endpoints REST
- `Middlewares/` : ExceptionHandlingMiddleware
- `Program.cs` : Configuration et pipeline

### Application (AdvancedDevSample.Application)

**Responsabilités :**
- Orchestration des cas d'usage
- Validation des données d'entrée
- Transformation Domain ↔ DTO
- Logique applicative (non métier)

**Composants :**
- `Services/` : ProductService, OrderService, UserService, etc.
- `DTOs/` : Objets de transfert de données
- `Exceptions/` : ApplicationServiceException

### Domain (AdvancedDevSample.Domain)

**Responsabilités :**
- Modèle métier
- Règles de gestion fondamentales
- Invariants métier
- Définition des contrats (interfaces)

**Composants :**
- `Entities/` : Product, Order, User, Supplier
- `Interfaces/` : Contrats des repositories
- `Exceptions/` : DomainException
- `ValueObjects/` : Objets-valeurs immuables

### Infrastructure (AdvancedDevSample.Infrastructure)

**Responsabilités :**
- Implémentation de la persistance
- Accès aux données
- Implémentation des interfaces du Domain

**Composants :**
- `Repositories/` : Implémentations des IRepository
- `Entities/` : Modèles de persistance (si différents du Domain)

## 🔄 Flux de données

### Requête HTTP typique

```
1. Controller (API)
   ↓ Reçoit la requête HTTP
   ↓ Valide les données d'entrée
   
2. Service (Application)
   ↓ Orchestre le cas d'usage
   ↓ Transforme DTO → Entité Domain
   
3. Entité (Domain)
   ↓ Applique les règles métier
   ↓ Valide les invariants
   
4. Repository (Infrastructure)
   ↓ Persiste les données
   
5. Service (Application)
   ↓ Transforme Entité → DTO
   
6. Controller (API)
   ↓ Retourne la réponse HTTP
```

## 🛡️ Avantages de cette architecture

### Testabilité
- Chaque couche peut être testée indépendamment
- Facilite les tests unitaires
- 137 tests unitaires dans le projet

### Maintenabilité
- Code organisé et prévisible
- Facile à naviguer et comprendre
- Modifications localisées

### Flexibilité
- Changement de base de données facile
- Remplacement de la couche API possible (CLI, gRPC, etc.)
- Évolution du Domain indépendante

### Qualité
- Couplage faible entre les couches
- Cohésion forte au sein de chaque couche
- Respect des principes SOLID

## 📝 Conventions de nommage

### Entités Domain
```csharp
public class Product { }
public class Order { }
```

### Services Application
```csharp
public class ProductService { }
public class OrderService { }
```

### DTOs
```csharp
public class CreateProductRequest { }
public class ProductResponse { }
```

### Repositories
```csharp
public interface IProductRepository { }
public class EfProductRepository : IProductRepository { }
```

## 🔗 Injection de dépendances

Configuration dans `Program.cs` :

```csharp
// Services Application
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();

// Repositories Infrastructure
builder.Services.AddScoped<IProductRepository, EfProductRepository>();
builder.Services.AddScoped<IOrderRepository, EfOrderRepository>();
```

## 🎓 Références

- [Clean Architecture - Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Domain-Driven Design - Eric Evans](https://www.domainlanguage.com/ddd/)
- [SOLID Principles](https://en.wikipedia.org/wiki/SOLID)
