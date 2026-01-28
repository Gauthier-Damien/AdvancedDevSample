# 🎯 Architecture en un coup d'œil

## Schéma global

```mermaid
graph TB
    Client["👤 Client HTTP<br/>(Postman, Browser)"]
    
    API["🎯 API LAYER<br/>(Présentation REST)<br/>---<br/>GET /api/products<br/>GET /api/products/{id}<br/>PUT /api/products/{id}/price<br/>POST /api/products/{id}/apply-promotion<br/>PUT /api/products/{id}/status"]
    
    App["🔧 APPLICATION LAYER<br/>(Orchestration)<br/>---<br/>ProductService<br/>• GetAllAsync()<br/>• GetByIdAsync(id)<br/>• UpdatePriceAsync()<br/>• ApplyPromotionAsync()<br/>• SetStatusAsync()"]
    
    Domain["🏛️ DOMAIN LAYER ⭐<br/>(Cœur Métier)<br/>---<br/>Product, Supplier<br/>Price, VAT<br/>---<br/>🛡️ Price > 0<br/>🛡️ Invariants<br/>🛡️ Rules"]
    
    Infra["💾 INFRASTRUCTURE LAYER<br/>(Persistance)<br/>---<br/>ProductRepository<br/>ApplicationDbContext<br/>Migrations<br/>Entity Configurations"]
    
    DB["🗄️ SQL SERVER DATABASE<br/>Tables: Products,<br/>Suppliers, VAT"]
    
    Client -->|REST Calls| API
    API -->|appelle| App
    App -->|utilise| Domain
    Domain -->|interface via| Infra
    Infra -->|accède à| DB
```

---

## Dépendances entre couches

```mermaid
graph TD
    API["🎯 API<br/>(REST)"]
    App["🔧 Application<br/>(Services)"]
    Domain["🏛️ Domain<br/>(Métier)"]
    Infra["💾 Infrastructure<br/>(Persistance)"]
    DB[("🗄️ Database")]
    
    API -->|appelle| App
    App -->|utilise| Domain
    Domain -->|interface via| Infra
    Infra -->|accède| DB
    
    style API fill:#e1f5ff
    style App fill:#f3e5f5
    style Domain fill:#fce4ec
    style Infra fill:#e0f2f1
    style DB fill:#fff3e0
```

**Règle d'or :** Chaque couche ne dépend que de la couche en dessous. JAMAIS l'inverse.

---

## Cas d'usage : "Modifier le prix d'un produit"

```mermaid
sequenceDiagram
    participant Client
    participant API as API Controller
    participant Service as ProductService
    participant Domain as Product (Domain)
    participant Repo as Repository
    participant DB as Database

    Client->>API: PUT /products/{id}/price<br/>{ newPrice: 149.99 }
    API->>API: Valide DTO
    API->>Service: UpdatePriceAsync(id, newPrice)
    
    Service->>Service: Valide pré-conditions
    Service->>Repo: GetByIdAsync(id)
    Repo->>DB: SELECT FROM Products
    DB-->>Repo: Product
    Repo-->>Service: Product
    
    Service->>Domain: product.UpdatePrice(149.99)
    alt Validation Invariant OK
        Domain->>Domain: Valide: 149.99 > 0 ✓
        Domain->>Domain: Price = 149.99
        Domain-->>Service: Success
        Service->>Repo: UpdateAsync(product)
        Repo->>DB: UPDATE Products SET Price = 149.99
        DB-->>Repo: OK
        Repo-->>Service: OK
        Service-->>API: ProductDto
        API-->>Client: 200 OK + ProductDto
    else Validation échoue
        Domain->>Domain: Valide: newPrice <= 0 ❌
        Domain-->>Service: DomainException("Price > 0")
        Service-->>API: ApplicationException
        API-->>Client: 409 Conflict + Error
    end
```

---

## Gestion d'erreur : "Prix invalide"

```
┌──────────────────────────────────────┐
│ Client envoie: newPrice = -50        │
└──────────────────────────────────────┘
         │
         ↓
┌──────────────────────────────────────┐
│ Application Layer valide (OK)        │
│ price > 0 ? ✓                        │
└──────────────────────────────────────┘
         │
         ↓
## Gestion d'erreur : "Prix invalide"

```mermaid
sequenceDiagram
    participant Client
    participant AppLayer as Application Layer
    participant DomainLayer as Domain Layer
    participant APILayer as API Layer

    Client->>AppLayer: UpdatePriceAsync(id, -50)
    
    AppLayer->>AppLayer: Valide: -50 > 0 ?
    Note over AppLayer: ✓ Pré-validation OK
    
    AppLayer->>DomainLayer: product.UpdatePrice(-50)
    
    DomainLayer->>DomainLayer: Valide INVARIANT<br/>-50 > 0 ?
    DomainLayer->>DomainLayer: ❌ INVARIANT VIOLATION
    DomainLayer-->>DomainLayer: DomainException<br/>"Le prix doit être > 0"
    
    DomainLayer-->>AppLayer: DomainException
    AppLayer-->>AppLayer: Catch et mappe
    AppLayer-->>APILayer: ApplicationException
    
    APILayer-->>APILayer: Catch et mappe HTTP
    APILayer-->>APILayer: 409 Conflict
    
    APILayer-->>Client: 409 Conflict + Error
```

---

## Tableau comparatif des responsabilités

| Aspect | API | Application | Domain | Infrastructure |
|--------|-----|-------------|--------|----------------|
| **Endpoints HTTP** | ✅ | ❌ | ❌ | ❌ |
| **DTOs** | ✅ | ✅ | ❌ | ❌ |
| **Validation métier** | ❌ | ✅ | ✅ | ❌ |
| **Orchestration** | ❌ | ✅ | ❌ | ❌ |
| **Entités métier** | ❌ | ❌ | ✅ | ❌ |
| **Règles métier** | ❌ | ❌ | ✅ | ❌ |
| **Repositories** | ❌ | ❌ | ❌ | ✅ |
| **Base de données** | ❌ | ❌ | ❌ | ✅ |
| **EF Core** | ❌ | ❌ | ❌ | ✅ |

---

## Stack technologique

```
┌─────────────────────────┐
│ Framework : ASP.NET Core 6.0+
│ Langage : C# 10+
│ ORM : Entity Framework Core
│ Base de données : SQL Server (ou autre)
│ Architecture : Clean Architecture + DDD
└─────────────────────────┘
```

---

## Les 5 Use Cases

```
1️⃣  GET /api/products
    Lister tous les produits actifs
    
2️⃣  GET /api/products/{id}
    Afficher les détails d'un produit
    
3️⃣  PUT /api/products/{id}/price
    Modifier le prix (>0, INVARIANT protégé)
    
4️⃣  POST /api/products/{id}/apply-promotion
    Appliquer une réduction (0-100%, price > 0)
    
5️⃣  PUT /api/products/{id}/status
    Activer/Désactiver un produit
```

---

## Les 5 Règles Métier

```
🛡️  CRITIQUE: Price > 0
    └─ Le prix doit toujours être > 0
    └─ Protégé à chaque mutation
    └─ Immuable et valide

✅ HAUTE: Produit avec prix valide
    └─ Tout produit a un prix
    └─ Un produit sans prix ne peut pas exister
    
✅ HAUTE: Invariant de prix
    └─ Price reste valide après chaque opération
    └─ Mutation atomique (tout ou rien)
    
✅ MOYENNE: État d'activation
    └─ Produit actif → visible publiquement
    └─ Produit inactif → caché, mais modifiable
    
✅ HAUTE: Promotion valide
    └─ Pourcentage entre 0 et 100%
    └─ Prix final respecte Règle 1
```

---

## Contrats entre les couches

### API ↔ Application (Interface: IProductService)
```csharp
Task<IEnumerable<ProductDto>> GetAllAsync();
Task<ProductDto> GetByIdAsync(Guid id);
Task<ProductDto> UpdatePriceAsync(Guid id, decimal newPrice);
Task<ProductDto> ApplyPromotionAsync(Guid id, decimal discount);
Task<ProductDto> SetStatusAsync(Guid id, bool isActive);
```

### Application ↔ Domain (Entités)
```csharp
Product.UpdatePrice(decimal newPrice)  // Lève DomainException si invalide
Product.ApplyDiscount(decimal %)       // Lève DomainException si invalide
Product.SetStatus(bool isActive)       // Simple
```

### Application ↔ Infrastructure (Interface: IProductRepository)
```csharp
Task<Product> GetByIdAsync(Guid id);
Task<IEnumerable<Product>> GetAllActiveAsync();
Task UpdateAsync(Product product);
```

---

## Checklist de compréhension

- [ ] Je comprends le flux de dépendances (haut → bas)
- [ ] Je sais où se trouvent les règles métier (Domain)
- [ ] Je sais où se trouvent les endpoints (API)
- [ ] Je sais comment les exceptions se propagent
- [ ] Je comprends le cas "Modifier le prix"
- [ ] Je sais ce qu'est un invariant (Price > 0)
- [ ] Je comprends le rôle de chaque couche
- [ ] Je sais utiliser les repositories
- [ ] Je comprends les DTOs et mappers
- [ ] Je sais localiser une fonctionnalité dans le code

---

## Ressources

- [INDEX.md](./INDEX.md) - Vue d'ensemble complète
- [01_API_Documentation.md](./01_API_Documentation.md) - Détails de l'API
- [02_Domain_Documentation.md](./02_Domain_Documentation.md) - Cœur métier
- [03_Application_Documentation.md](./03_Application_Documentation.md) - Orchestration
- [04_Infrastructure_Documentation.md](./04_Infrastructure_Documentation.md) - Persistance

---

**Architecture en un coup d'œil - AdvancedDevSample**

*Pour plus de détails, consulter les documentations respectives de chaque couche.*
