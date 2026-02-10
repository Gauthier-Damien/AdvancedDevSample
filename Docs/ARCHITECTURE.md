# Architecture - AdvancedDevSample

> 🚧 **En cours de rédaction** - Cette documentation sera complétée avec des diagrammes UML

## Vue d'ensemble

Le projet **AdvancedDevSample** suit les principes de la **Clean Architecture** (Architecture Hexagonale), garantissant une séparation claire des responsabilités et une testabilité optimale.

## Structure en couches

```
┌─────────────────────────────────────────────┐
│           API (Présentation)                │
│   Controllers, Middlewares, Program.cs      │
└─────────────────┬───────────────────────────┘
                  │
┌─────────────────▼───────────────────────────┐
│       Application (Logique métier)          │
│    Services, DTOs, Exceptions métier        │
└─────────────────┬───────────────────────────┘
                  │
┌─────────────────▼───────────────────────────┐
│          Domain (Cœur métier)               │
│   Entities, ValueObjects, Interfaces        │
└─────────────────┬───────────────────────────┘
                  │
┌─────────────────▼───────────────────────────┐
│      Infrastructure (Persistance)           │
│    Repositories, Context, Entities EF       │
└─────────────────────────────────────────────┘
```

## Couche API (`AdvancedDevSample.API`)

### Responsabilités
- Point d'entrée de l'application
- Gestion des requêtes HTTP
- Configuration des middlewares
- Injection de dépendances
- Documentation Swagger

### Composants principaux

#### Controllers
- `AuthController.cs` - Authentification JWT
- `UserController.cs` - Gestion des utilisateurs
- `ProductController.cs` - Gestion des produits
- `SupplierController.cs` - Gestion des fournisseurs
- `OrderController.cs` - Gestion des commandes

#### Middlewares
- `ExceptionHandlingMiddleware.cs` - Gestion centralisée des exceptions

#### Configuration
- `Program.cs` - Configuration de l'application et du pipeline HTTP

### Dépendances
- Application (Services)
- Infrastructure (Repositories)

---

## Couche Application (`AdvancedDevSample.Application`)

### Responsabilités
- Logique métier applicative
- Validation des données
- Transformation DTOs ↔ Entities
- Orchestration des opérations

### Structure

```
Application/
├── Services/
│   ├── ProductService.cs
│   ├── SupplierService.cs
│   ├── UserService.cs
│   └── OrderService.cs
├── DTOs/
│   ├── Auth/
│   ├── Products/
│   ├── Suppliers/
│   ├── Users/
│   ├── Orders/
│   └── Common/
└── Exceptions/
    └── ApplicationServiceException.cs
```

### Pattern utilisé
- **Service Layer Pattern** : Encapsulation de la logique métier

### Dépendances
- Domain (Interfaces et Entities)

---

## Couche Domain (`AdvancedDevSample.Domain`)

### Responsabilités
- Définition du modèle métier
- Règles métier fondamentales
- Interfaces des repositories
- Value Objects

### Structure

```
Domain/
├── Entities/
│   ├── Product.cs
│   ├── Supplier.cs
│   ├── User.cs
│   └── Order.cs
├── Interfaces/
│   ├── Products/
│   │   └── IProductRepository.cs
│   ├── Suppliers/
│   │   └── ISupplierRepository.cs
│   ├── Users/
│   │   └── IUserRepository.cs
│   ├── Orders/
│   │   └── IOrderRepository.cs
│   └── Auth/
│       └── IAuthRepository.cs
├── ValueObjects/
└── Exceptions/
```

### Principes
- **Indépendance** : Aucune dépendance externe
- **Richesse du modèle** : Logique métier dans les entités
- **Invariants** : Règles de validation strictes

### Dépendances
- Aucune (cœur de l'application)

---

## Couche Infrastructure (`AdvancedDevSample.Infrastructure`)

### Responsabilités
- Implémentation de la persistance
- Accès aux données (In-Memory pour cet exemple)
- Mapping Entities Domain → Entities Infrastructure

### Structure

```
Infrastructure/
├── Repositories/
│   ├── EfProductRepository.cs
│   ├── EfSupplierRepository.cs
│   ├── EfUserRepository.cs
│   ├── EfOrderRepository.cs
│   └── AuthRepository.cs
├── Entities/
│   └── (Entités spécifiques à EF si nécessaire)
└── Exceptions/
```

### Pattern utilisé
- **Repository Pattern** : Abstraction de l'accès aux données

### Implémentation actuelle
- **In-Memory Storage** : Listes en mémoire pour la persistance temporaire
- Prêt pour migration vers Entity Framework Core avec base de données réelle

### Dépendances
- Domain (Interfaces à implémenter)

---

## Couche Test (`AdvancedDevSample.Test`)

### Responsabilités
- Tests unitaires
- Tests d'intégration
- Validation du comportement

### Structure

```
Test/
├── Domaine/
├── Application/
└── API/
```

### Frameworks
- xUnit
- (À compléter avec les frameworks de mock)

---

## Flux de données

### Requête typique (GET)

```
1. HTTP Request
   │
   ▼
2. Controller (API)
   │ - Reçoit la requête
   │ - Valide les paramètres
   ▼
3. Service (Application)
   │ - Exécute la logique métier
   │ - Appelle le repository
   ▼
4. Repository (Infrastructure)
   │ - Récupère les données
   │ - Retourne les entités Domain
   ▼
5. Service (Application)
   │ - Transforme Entities → DTOs
   ▼
6. Controller (API)
   │ - Retourne la réponse HTTP
   ▼
7. HTTP Response
```

### Création d'une ressource (POST)

```
1. HTTP Request (JSON)
   │
   ▼
2. Controller (API)
   │ - Reçoit le DTO de création
   │ - Valide les données
   ▼
3. Service (Application)
   │ - Valide la logique métier
   │ - Transforme DTO → Entity
   ▼
4. Repository (Infrastructure)
   │ - Persiste l'entité
   │ - Retourne l'entité créée
   ▼
5. Service (Application)
   │ - Transforme Entity → DTO
   ▼
6. Controller (API)
   │ - Retourne 201 Created
   ▼
7. HTTP Response (JSON)
```

---

## Principes appliqués

### SOLID

- **S** - Single Responsibility : Chaque classe a une responsabilité unique
- **O** - Open/Closed : Extensions sans modification du code existant
- **L** - Liskov Substitution : Interfaces respectées
- **I** - Interface Segregation : Interfaces spécifiques et ciblées
- **D** - Dependency Inversion : Dépendances sur les abstractions

### Clean Architecture

- **Indépendance des frameworks** : Le domaine n'en dépend pas
- **Testabilité** : Logique métier facilement testable
- **Indépendance de l'UI** : L'API peut être remplacée
- **Indépendance de la BD** : In-Memory → SQL facilement

### DDD (Domain-Driven Design)

- Modèle riche
- Ubiquitous Language
- Bounded Contexts

---

## Patterns de conception

| Pattern | Localisation | Usage |
|---------|--------------|-------|
| Repository | Infrastructure | Abstraction de l'accès aux données |
| Service Layer | Application | Encapsulation de la logique métier |
| DTO | Application | Transfert de données entre couches |
| Dependency Injection | API | Inversion de contrôle |
| Middleware | API | Pipeline de traitement des requêtes |

---

## Sécurité

### Authentification JWT

```
┌─────────────┐      Login       ┌──────────────┐
│   Client    │ ───────────────► │ AuthController│
└─────────────┘                   └──────────────┘
       │                                  │
       │                            Valide credentials
       │                                  │
       │         ◄────────────────────────┘
       │            JWT Token
       │
       │      Request + Token
       │ ───────────────────────►  Middleware JWT
                                         │
                                   Valide Token
                                         │
                                    Controller
```

### Middleware d'authentification

- Validation du token JWT
- Extraction des claims (rôle, username)
- Injection du contexte utilisateur

---

## Configuration et Démarrage

### Injection de dépendances (Program.cs)

```csharp
// Services
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<SupplierService>();
// ...

// Repositories
builder.Services.AddScoped<IProductRepository, EfProductRepository>();
builder.Services.AddScoped<ISupplierRepository, EfSupplierRepository>();
// ...
```

### Pipeline HTTP

1. ExceptionHandlingMiddleware
2. Swagger (Dev uniquement)
3. HTTPS Redirection
4. Authentication
5. Authorization
6. Controllers

---

## Évolutions futures

- [ ] Migration vers base de données SQL Server / PostgreSQL
- [ ] Ajout de Entity Framework Core avec migrations
- [ ] Implémentation de CQRS (Command Query Responsibility Segregation)
- [ ] Ajout de MediatR pour le pattern Mediator
- [ ] Cache distribué (Redis)
- [ ] Event Sourcing
- [ ] API Gateway
- [ ] Containerisation Docker

---

*Dernière mise à jour : 2026-02-10*
