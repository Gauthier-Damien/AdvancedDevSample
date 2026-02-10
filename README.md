# AdvancedDevSample

[![.NET](https://img.shields.io/badge/.NET-9.0-purple)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

Application de gestion de catalogue produits développée avec une **architecture Clean Code** et les principes **Domain-Driven Design (DDD)**.

---

## 🌳 Branches du projet

Ce projet utilise une organisation en branches pour séparer le code de la documentation :

- **`master`** - Branche principale de production
- **`Codding`** - Branche de développement du code
- **`Docs`** - Branche dédiée à la documentation complète

📚 **Pour accéder à la documentation complète**, basculez sur la branche `Docs` :
```bash
git checkout Docs
cd Docs
# Consultez README.md ou INDEX.md pour naviguer
```

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

# Compiler la solution
dotnet build

# Lancer l'API
cd AdvancedDevSample.API
dotnet run
```

### Accès à l'API

Une fois l'API lancée :

- **Swagger UI** : `https://localhost:5181/swagger`
- **API** : `https://localhost:5181/api`

---

## 📦 Fonctionnalités

### Catalogue produits

- ✅ Liste des produits
- ✅ Afficher les informations produit
- ✅ Modifier les prix
- ✅ Appliquer des promotions
- ✅ Activer/Désactiver un produit

### Gestion

- 📦 **Produits** - CRUD complet
- 🏢 **Fournisseurs** - Gestion des fournisseurs
- 👥 **Utilisateurs** - Base utilisateurs
- 📋 **Commandes** - Système de commandes avec lignes de détail

---

## 🏗️ Architecture

Le projet suit une **architecture Clean Code** avec séparation stricte des responsabilités :

```
AdvancedDevSample/
├── AdvancedDevSample.API/          # Couche Présentation (Controllers, Middlewares)
├── AdvancedDevSample.Application/  # Couche Application (Services, DTOs)
├── AdvancedDevSample.Domain/       # Couche Domain (Entités, Règles métier)
├── AdvancedDevSample.Infrastructure/ # Couche Infrastructure (Repositories)
└── AdvancedDevSample.Test/         # Tests unitaires et d'intégration
```

### Principes

- ✅ **Separation of Concerns** - Chaque couche a une responsabilité claire
- ✅ **Dependency Inversion** - Le Domain ne dépend de rien
- ✅ **Repository Pattern** - Abstraction de la persistance
- ✅ **CQRS** - Séparation lecture/écriture (DTOs)

**Pour plus de détails** : [Architecture complète →](https://Gauthier-Damien.github.io/AdvancedDevSample/architecture/overview/)

---

## 🔧 Technologies

| Couche | Technologies |
|--------|-------------|
| **API** | ASP.NET Core 9.0, Swagger/OpenAPI |
| **Application** | Services, DTOs, Mapping |
| **Domain** | Entités, Value Objects, Interfaces |
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

# AdvancedDevSample

[![.NET](https://img.shields.io/badge/.NET-9.0-purple)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)
[![Build and Test](https://github.com/Gauthier-Damien/AdvancedDevSample/actions/workflows/build.yml/badge.svg)](https://github.com/Gauthier-Damien/AdvancedDevSample/actions/workflows/build.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Gauthier-Damien_AdvancedDevSample&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=Gauthier-Damien_AdvancedDevSample)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Gauthier-Damien_AdvancedDevSample&metric=coverage)](https://sonarcloud.io/summary/new_code?id=Gauthier-Damien_AdvancedDevSample)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=Gauthier-Damien_AdvancedDevSample&metric=bugs)](https://sonarcloud.io/summary/new_code?id=Gauthier-Damien_AdvancedDevSample)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=Gauthier-Damien_AdvancedDevSample&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=Gauthier-Damien_AdvancedDevSample)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=Gauthier-Damien_AdvancedDevSample&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=Gauthier-Damien_AdvancedDevSample)

Application de gestion de catalogue produits développée avec une **architecture Clean Code** et les principes **Domain-Driven Design (DDD)**.

---

## 🌳 Branches du projet

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

# Compiler la solution
dotnet build

# Lancer l'API
cd AdvancedDevSample.API
dotnet run
```

### Accès à l'API

Une fois l'API lancée :

- **Swagger UI** : `https://localhost:5181/swagger`
- **API** : `https://localhost:5181/api`

---

## 📦 Fonctionnalités

### Catalogue produits

- ✅ Liste des produits
- ✅ Afficher les informations produit
- ✅ Modifier les prix
- ✅ Appliquer des promotions
- ✅ Activer/Désactiver un produit

### Gestion

- 📦 **Produits** - CRUD complet
- 🏢 **Fournisseurs** - Gestion des fournisseurs
- 👥 **Utilisateurs** - Base utilisateurs
- 📋 **Commandes** - Système de commandes avec lignes de détail

---

## 🏗️ Architecture

Le projet suit une **architecture Clean Code** avec séparation stricte des responsabilités :

```
AdvancedDevSample/
├── AdvancedDevSample.API/          # Couche Présentation (Controllers, Middlewares)
├── AdvancedDevSample.Application/  # Couche Application (Services, DTOs)
├── AdvancedDevSample.Domain/       # Couche Domain (Entités, Règles métier)
├── AdvancedDevSample.Infrastructure/ # Couche Infrastructure (Repositories)
└── AdvancedDevSample.Test/         # Tests unitaires et d'intégration
```

### Principes

- ✅ **Separation of Concerns** - Chaque couche a une responsabilité claire
- ✅ **Dependency Inversion** - Le Domain ne dépend de rien
- ✅ **Repository Pattern** - Abstraction de la persistance
- ✅ **CQRS** - Séparation lecture/écriture (DTOs)

---

## 🔧 Technologies

| Couche | Technologies |
|--------|-------------|
| **API** | ASP.NET Core 9.0, Swagger/OpenAPI |
| **Application** | Services, DTOs, Mapping |
| **Domain** | Entités, Value Objects, Interfaces |
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

## 🤝 Contribution

Les contributions sont les bienvenues !

### Workflow

1. Fork le projet
2. Créer une branche (`git checkout -b feature/AmazingFeature`)
3. Commit les changements (`git commit -m 'feat: add amazing feature'`)
4. Push vers la branche (`git push origin feature/AmazingFeature`)
5. Ouvrir une Pull Request

---

## 📄 License

Ce projet est sous licence MIT. Voir le fichier `LICENSE` pour plus de détails.

---

## 👤 Auteur

**Gautier Damien**

- GitHub: [@Gauthier-Damien](https://github.com/Gauthier-Damien)
- Repository: [AdvancedDevSample](https://github.com/Gauthier-Damien/AdvancedDevSample)

---

## 🔗 Liens utiles

- 🐙 **[Repository GitHub](https://github.com/Gauthier-Damien/AdvancedDevSample)**
- 📊 **[Swagger UI](https://localhost:5181/swagger)** (après `dotnet run`)

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
