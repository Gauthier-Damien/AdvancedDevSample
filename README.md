# AdvancedDevSample

[![.NET](https://img.shields.io/badge/.NET-9.0-purple)](https://dotnet.microsoft.com/)
[![Build and Test](https://github.com/Gauthier-Damien/AdvancedDevSample/actions/workflows/build.yml/badge.svg)](https://github.com/Gauthier-Damien/AdvancedDevSample/actions/workflows/build.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Gauthier-Damien_AdvancedDevSample&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=Gauthier-Damien_AdvancedDevSample)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Gauthier-Damien_AdvancedDevSample&metric=coverage)](https://sonarcloud.io/summary/new_code?id=Gauthier-Damien_AdvancedDevSample)

API RESTful de gestion de commandes, produits, utilisateurs et fournisseurs développée avec **ASP.NET Core 9.0** et une **architecture Clean Architecture** (DDD).

---

## 📚 Documentation

📖 **Documentation complète disponible sur GitHub Pages :**  
👉 **[https://gauthier-damien.github.io/AdvancedDevSample/](https://gauthier-damien.github.io/AdvancedDevSample/)**

---


## 🚀 Démarrage rapide

### Prérequis

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- IDE : [Rider](https://www.jetbrains.com/rider/), [Visual Studio 2022](https://visualstudio.microsoft.com/), ou [VS Code](https://code.visualstudio.com/)

### Installation

```bash
# Cloner le repository
git clone https://github.com/Gauthier-Damien/AdvancedDevSample.git
cd AdvancedDevSample

# Restaurer les dépendances
dotnet restore

# Lancer l'API
cd AdvancedDevSample.API
dotnet run
```

### Accès

Une fois l'API lancée, accédez à :

- **Swagger UI** : https://localhost:7000/swagger
- **API** : https://localhost:7000/api

### Comptes de démonstration

- **Étudiant** : `demo` / `demo123`
- **Administrateur** : `admin` / `admin123`

---

## ✨ Fonctionnalités

- 🔐 **Authentification JWT** - Login, refresh token, gestion des rôles
- 📦 **Gestion des produits** - CRUD, modification de prix, réductions, soft delete
- 📋 **Gestion des commandes** - Machine à états (Pending → Confirmed → Shipped → Delivered)
- 👥 **Gestion des utilisateurs** - CRUD, activation/désactivation des comptes
- 🏢 **Gestion des fournisseurs** - CRUD complet avec validation

---

## 🏗️ Architecture

Le projet implémente une **Clean Architecture** avec 4 couches :

```
AdvancedDevSample/
├── AdvancedDevSample.API/          # 🌐 Présentation (Controllers, Middlewares)
├── AdvancedDevSample.Application/  # 💼 Application (Services, DTOs)
├── AdvancedDevSample.Domain/       # 🎯 Domain (Entités, Règles métier)
├── AdvancedDevSample.Infrastructure/ # 🗄️ Infrastructure (Repositories)
└── AdvancedDevSample.Test/         # 🧪 Tests (137 tests unitaires)
```

### Principes appliqués

- ✅ **Clean Architecture** - Séparation stricte des responsabilités
- ✅ **Domain-Driven Design** - Logique métier dans le Domain
- ✅ **Dependency Inversion** - Interfaces dans Domain, implémentations dans Infrastructure
- ✅ **Repository Pattern** - Abstraction de la persistance
- ✅ **SOLID Principles** - Code maintenable et extensible

---

## 🔧 Technologies

| Couche | Technologies |
|--------|-------------|
| **API** | ASP.NET Core 9.0, Swagger/OpenAPI, JWT Authentication |
| **Application** | Services, Data Annotations, BCrypt |
| **Domain** | Entités, Value Objects, DomainException |
| **Infrastructure** | Repository Pattern (InMemory) |
| **Tests** | xUnit (137 tests), Fake Repositories |

---

## 🧪 Tests

### Exécuter les tests

```bash
# Tous les tests
dotnet test

# Avec couverture de code
dotnet test --collect:"XPlat Code Coverage"
```

### Couverture

- ✅ **137 tests unitaires** (100% de réussite)
- ✅ **Domain** - Tests des entités et règles métier
- ✅ **Application** - Tests des services
- ✅ **API** - Tests des contrôleurs
- ✅ **Couverture > 80%**

---

## 📊 Qualité du code

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Gauthier-Damien_AdvancedDevSample&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=Gauthier-Damien_AdvancedDevSample)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=Gauthier-Damien_AdvancedDevSample&metric=bugs)](https://sonarcloud.io/summary/new_code?id=Gauthier-Damien_AdvancedDevSample)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=Gauthier-Damien_AdvancedDevSample&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=Gauthier-Damien_AdvancedDevSample)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=Gauthier-Damien_AdvancedDevSample&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=Gauthier-Damien_AdvancedDevSample)

**Analyse SonarQube :**
- ✅ 0 Bugs
- ✅ 0 Vulnerabilities  
- ✅ Quality Gate : Passed
- ✅ 80% des Code Smells résolus

👉 [Voir l'analyse complète sur SonarCloud](https://sonarcloud.io/project/overview?id=Gauthier-Damien_AdvancedDevSample)

---

## 📖 Documentation

La documentation technique complète est disponible sur **GitHub Pages** :

🌐 **[https://gauthier-damien.github.io/AdvancedDevSample/](https://gauthier-damien.github.io/AdvancedDevSample/)**

### Contenu de la documentation

- 🏠 **Vue d'ensemble** - Présentation du projet
- ⚡ **Démarrage rapide** - Installation et premiers pas
- 🏗️ **Architecture** - Clean Architecture détaillée
- 🎯 **Domain** - Entités et règles métier
- 💼 **Application** - Services et DTOs
- 🌐 **API** - Endpoints REST et authentification
- 🗄️ **Infrastructure** - Repositories et persistance
---

## 📄 Licence

Ce projet est sous licence MIT. Voir le fichier [LICENSE](LICENSE) pour plus de détails.

---

## 👨‍💻 Auteur

**Gauthier Damien**

- GitHub: [@Gauthier-Damien](https://github.com/Gauthier-Damien)
- Projet: [AdvancedDevSample](https://github.com/Gauthier-Damien/AdvancedDevSample)

---

## 🔗 Liens utiles

- 📖 [Documentation complète](https://gauthier-damien.github.io/AdvancedDevSample/)
- 🔍 [SonarCloud](https://sonarcloud.io/project/overview?id=Gauthier-Damien_AdvancedDevSample)
- 🚀 [GitHub Actions](https://github.com/Gauthier-Damien/AdvancedDevSample/actions)
- 📊 [Releases](https://github.com/Gauthier-Damien/AdvancedDevSample/releases)
| **Infrastructure** | Repositories (In-Memory actuellement) |
| **Tests** | xUnit, Moq |

---

## 🧪 Tests

### Lancer les tests

```bash
# Tous les tests
dotnet test

# Tests avec couverture
dotnet test --collect:"XPlat Code Coverage"

# Tests d'un projet spécifique
dotnet test AdvancedDevSample.Test/AdvancedDevSample.Test.csproj
```

### Couverture

- ✅ **Tests unitaires** - Domain, Application
- ✅ **Tests d'intégration** - API, Controllers
- ✅ **Mocking** - Repositories avec Moq

---

## 📋 Règles métier

### Produits

- ✅ Le **prix** doit être **strictement positif**
- ✅ Un produit doit toujours avoir un **prix valide**
- ✅ La **TVA** doit être valide (entre 0 et 100%)

### Commandes

- ✅ Une commande doit contenir **au moins une ligne**
- ✅ Les **quantités** doivent être positives
- ✅ Les **prix unitaires** doivent être strictement positifs

---

## 🔌 API REST

### Endpoints disponibles

| Ressource | Endpoint | Méthodes |
|-----------|----------|----------|
| **Products** | `/api/products` | GET, POST, PUT, DELETE |
| **Suppliers** | `/api/suppliers` | GET, POST, PUT, DELETE |
| **Users** | `/api/users` | GET, POST, PUT, DELETE |
| **Orders** | `/api/orders` | GET, POST, PUT, DELETE |

### Exemple d'utilisation

```bash
# Récupérer tous les produits
curl https://localhost:5181/api/products

# Créer un produit
curl -X POST https://localhost:5181/api/products \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Laptop Dell XPS 15",
    "description": "Haute performance",
    "price": 1299.99,
    "vatRate": 20.0,
    "supplierId": "550e8400-e29b-41d4-a716-446655440000"
  }'
```

---

## 🛡️ Sécurité

- ✅ **Rate Limiting** - 100 requêtes/minute par IP
- ✅ **Validation automatique** - ModelState et règles métier
- ✅ **Gestion d'erreurs centralisée** - Middleware dédié
- ✅ **HTTPS** - Redirection automatique
- ✅ **Pas de stack trace en production** - Sécurité des données

---

## 💻 Développement

### Conventions de code

- **Classes** : PascalCase (`ProductService`)
- **Méthodes** : PascalCase (`GetAllAsync`)
- **Variables** : camelCase (`productId`)
- **Champs privés** : _camelCase (`_productRepository`)

### Commandes utiles

```bash
# Build
dotnet build

# Run API
dotnet run --project AdvancedDevSample.API

# Tests
dotnet test

# Clean
dotnet clean
```

---

### Workflow

1. Fork le projet
2. Créer une branche (`git checkout -b feature/AmazingFeature`)
3. Commit les changements (`git commit -m 'feat: add amazing feature'`)
4. Push vers la branche (`git push origin feature/AmazingFeature`)
5. Ouvrir une Pull Request

---

## 📊 Statistiques du projet

| Métrique | Valeur |
|----------|--------|
| **Couches** | 4 (API, Application, Domain, Infrastructure) |
| **Tests** | Unitaires + Intégration |
| **Endpoints API** | 16+ endpoints REST |
| **Framework** | .NET 9.0 |

---


*Dernière mise à jour : Février 2026*
