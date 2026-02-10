# Structure du projet AdvancedDevSample

## 📁 Arborescence complète

```
AdvancedDevSample/
│
├── 📄 AdvancedDevSample.sln              # Solution principale
├── 📄 README.md                           # Documentation principale
│
├── 📂 Docs/                               # 📚 DOCUMENTATION
│   ├── README.md                          # Index de la documentation
│   ├── ORGANISATION.md                    # Organisation de la branche Docs
│   ├── STRUCTURE_PROJET.md               # Ce fichier
│   │
│   ├── 🏗️ Architecture & Standards
│   │   ├── ARCHITECTURE.md               # Architecture détaillée
│   │   ├── CONTRIBUTING.md               # Guide de contribution
│   │   └── CHANGELOG.md                  # Historique des versions
│   │
│   ├── 📋 Audits & Rapports
│   │   ├── AUDIT_CODE.md                 # Audit initial
│   │   ├── AUDIT_COMPLET_FINAL.md       # Audit final complet
│   │   ├── CORRECTIFS_PRIORITAIRES.md   # Correctifs prioritaires
│   │   └── RAPPORT_CORRECTIFS_APPLIQUES.md
│   │
│   ├── 🔐 JWT & Authentification
│   │   ├── JWT_IMPLEMENTATION_SUCCESS.md
│   │   ├── RECAPITULATIF_JWT_FINAL.md
│   │   └── GUIDE_TEST_JWT.md
│   │
│   └── 📖 API
│       └── API_DOCUMENTATION.md          # Documentation des endpoints
│
├── 📂 AdvancedDevSample.API/             # 🌐 COUCHE PRÉSENTATION
│   ├── Program.cs                         # Point d'entrée de l'application
│   ├── appsettings.json                   # Configuration principale
│   ├── appsettings.Development.json       # Configuration développement
│   ├── AdvancedDevSample.API.csproj      # Projet .NET
│   │
│   ├── 📂 Controllers/                    # Contrôleurs API
│   │   ├── AuthController.cs             # Authentification
│   │   ├── UserController.cs             # Gestion utilisateurs
│   │   ├── ProductController.cs          # Gestion produits
│   │   ├── SupplierController.cs         # Gestion fournisseurs
│   │   └── OrderController.cs            # Gestion commandes
│   │
│   ├── 📂 Middlewares/                    # Middlewares personnalisés
│   │   └── ExceptionHandlingMiddleware.cs # Gestion des exceptions
│   │
│   └── 📂 Properties/
│       └── launchSettings.json            # Configuration de lancement
│
├── 📂 AdvancedDevSample.Application/     # 💼 COUCHE APPLICATION
│   ├── AdvancedDevSample.Application.csproj
│   │
│   ├── 📂 Services/                       # Services métier
│   │   ├── ProductService.cs
│   │   ├── SupplierService.cs
│   │   ├── UserService.cs
│   │   └── OrderService.cs
│   │
│   ├── 📂 DTOs/                           # Data Transfer Objects
│   │   ├── Auth/
│   │   │   ├── LoginRequestDto.cs
│   │   │   └── LoginResponseDto.cs
│   │   ├── Products/
│   │   │   ├── ProductDto.cs
│   │   │   ├── CreateProductDto.cs
│   │   │   └── UpdateProductDto.cs
│   │   ├── Suppliers/
│   │   │   ├── SupplierDto.cs
│   │   │   ├── CreateSupplierDto.cs
│   │   │   └── UpdateSupplierDto.cs
│   │   ├── Users/
│   │   │   ├── UserDto.cs
│   │   │   ├── CreateUserDto.cs
│   │   │   └── UpdateUserDto.cs
│   │   ├── Orders/
│   │   │   ├── OrderDto.cs
│   │   │   ├── CreateOrderDto.cs
│   │   │   └── UpdateOrderDto.cs
│   │   └── Common/
│   │       └── PagedResultDto.cs
│   │
│   └── 📂 Exceptions/
│       └── ApplicationServiceException.cs
│
├── 📂 AdvancedDevSample.Domain/          # 🎯 COUCHE DOMAINE
│   ├── AdvancedDevSample.Domain.csproj
│   │
│   ├── 📂 Entities/                       # Entités métier
│   │   ├── Product.cs
│   │   ├── Supplier.cs
│   │   ├── User.cs
│   │   └── Order.cs
│   │
│   ├── 📂 Interfaces/                     # Interfaces des repositories
│   │   ├── Products/
│   │   │   └── IProductRepository.cs
│   │   ├── Suppliers/
│   │   │   └── ISupplierRepository.cs
│   │   ├── Users/
│   │   │   └── IUserRepository.cs
│   │   ├── Orders/
│   │   │   └── IOrderRepository.cs
│   │   └── Auth/
│   │       └── IAuthRepository.cs
│   │
│   ├── 📂 ValueObjects/                   # Objets valeur
│   │
│   └── 📂 Exceptions/                     # Exceptions métier
│
├── 📂 AdvancedDevSample.Infrastructure/  # 🔧 COUCHE INFRASTRUCTURE
│   ├── AdvancedDevSample.Infrastructure.csproj
│   │
│   ├── 📂 Repositories/                   # Implémentation des repositories
│   │   ├── EfProductRepository.cs
│   │   ├── EfSupplierRepository.cs
│   │   ├── EfUserRepository.cs
│   │   ├── EfOrderRepository.cs
│   │   └── AuthRepository.cs
│   │
│   ├── 📂 Entities/                       # Entités EF (si différentes)
│   │
│   └── 📂 Exceptions/                     # Exceptions infrastructure
│
└── 📂 AdvancedDevSample.Test/            # 🧪 COUCHE TESTS
    ├── AdvancedDevSample.Test.csproj
    │
    ├── 📂 Domaine/                        # Tests du domaine
    ├── 📂 Application/                    # Tests de l'application
    └── 📂 API/                            # Tests de l'API
```

## 🎨 Légende des icônes

| Icône | Signification |
|-------|--------------|
| 📂 | Dossier |
| 📄 | Fichier de configuration/documentation |
| 🌐 | API / Présentation |
| 💼 | Application / Services |
| 🎯 | Domaine / Cœur métier |
| 🔧 | Infrastructure / Persistance |
| 🧪 | Tests |
| 📚 | Documentation |
| 🏗️ | Architecture |
| 📋 | Audits et rapports |
| 🔐 | Sécurité / JWT |
| 📖 | Documentation API |

## 📊 Statistiques du projet

### Couches et responsabilités

| Couche | Projets | Rôle principal |
|--------|---------|----------------|
| **Présentation** | AdvancedDevSample.API | Gestion HTTP, Controllers, Middlewares |
| **Application** | AdvancedDevSample.Application | Services métier, DTOs, Logique applicative |
| **Domaine** | AdvancedDevSample.Domain | Entités, Interfaces, Règles métier |
| **Infrastructure** | AdvancedDevSample.Infrastructure | Repositories, Persistance, Accès données |
| **Tests** | AdvancedDevSample.Test | Tests unitaires et d'intégration |

### Contrôleurs et endpoints

| Contrôleur | Endpoints | Authentification |
|------------|-----------|------------------|
| AuthController | 1 | ❌ Non |
| UserController | 5 | ✅ Oui |
| ProductController | 5 | ✅ Oui |
| SupplierController | 5 | ✅ Oui |
| OrderController | 5 | ✅ Oui |

**Total : 21 endpoints**

### Documentation

| Type | Nombre de fichiers |
|------|-------------------|
| Architecture & Standards | 3 |
| Audits & Rapports | 4 |
| JWT & Authentification | 3 |
| API | 1 |
| Organisation | 2 |
| **TOTAL** | **13 fichiers** |

## 🔄 Flux de dépendances

```
┌──────────────────────────────────────────┐
│                   API                     │ ← Point d'entrée
│  (Controllers, Middlewares, Program.cs)  │
└────────────────┬─────────────────────────┘
                 │ dépend de ↓
                 │
┌────────────────▼─────────────────────────┐
│             Application                   │
│        (Services, DTOs)                   │
└────────────────┬─────────────────────────┘
                 │ dépend de ↓
                 │
┌────────────────▼─────────────────────────┐
│              Domain                       │ ← Cœur (sans dépendances)
│    (Entities, Interfaces, ValueObjects)  │
└────────────────▲─────────────────────────┘
                 │ implémente ↑
                 │
┌────────────────┴─────────────────────────┐
│          Infrastructure                   │
│         (Repositories)                    │
└──────────────────────────────────────────┘
```

**Principe clé** : Les dépendances pointent toujours vers le **Domain** (centre)

## 🛠️ Technologies utilisées

### Frameworks et librairies

- **ASP.NET Core 9.0** - Framework web
- **Entity Framework Core** (préparé) - ORM
- **JWT Bearer Authentication** - Authentification
- **Swagger/OpenAPI** - Documentation API
- **xUnit** - Framework de tests

### Patterns et principes

- ✅ Clean Architecture
- ✅ Repository Pattern
- ✅ Service Layer Pattern
- ✅ Dependency Injection
- ✅ SOLID Principles
- ✅ DTO Pattern

## 📝 Fichiers de configuration

### appsettings.json
```json
{
  "Jwt": {
    "SecretKey": "...",
    "Issuer": "AdvancedDevSample",
    "Audience": "AdvancedDevSampleUsers",
    "ExpirationMinutes": 60
  },
  "Logging": { ... }
}
```

### launchSettings.json
- Configuration des profils de lancement
- URLs de développement
- Variables d'environnement

## 🚀 Commandes utiles

```powershell
# Build du projet
dotnet build

# Lancer l'API
dotnet run --project AdvancedDevSample.API

# Lancer les tests
dotnet test

# Restaurer les packages
dotnet restore

# Nettoyer le projet
dotnet clean
```

## 📌 Points d'entrée importants

| Fichier | Description |
|---------|-------------|
| `Program.cs` | Configuration de l'application, DI, pipeline HTTP |
| `appsettings.json` | Configuration JWT, Logging |
| `Controllers/` | Points d'entrée HTTP de l'API |
| `Services/` | Logique métier principale |
| `Repositories/` | Accès aux données |

---

*Dernière mise à jour : 2026-02-10*
