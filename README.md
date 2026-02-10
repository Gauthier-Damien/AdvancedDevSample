# AdvancedDevSample

[![Documentation](https://img.shields.io/badge/docs-MkDocs-blue)](https://Gauthier-Damien.github.io/AdvancedDevSample/)
[![.NET](https://img.shields.io/badge/.NET-9.0-purple)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

Application de gestion de catalogue produits développée avec une **architecture Clean Code** et les principes **Domain-Driven Design (DDD)**.

---

## 📖 Documentation

**La documentation technique complète est disponible en ligne :**

### 🌐 [Accéder à la Documentation](https://Gauthier-Damien.github.io/AdvancedDevSample/)

La documentation inclut :
- 🏗️ **Architecture** - Clean Architecture, Domain, Application, Infrastructure, API
- 🔌 **API REST** - Endpoints, Controllers, Middlewares
- 🎯 **Domain** - Entités, Value Objects, Règles métier
- 🔧 **Application** - Services, DTOs, Orchestration
- 💾 **Infrastructure** - Repositories, Persistance
- 🧪 **Tests** - Tests unitaires et d'intégration
- 💻 **Développement** - Installation, Configuration, Bonnes pratiques

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

**Détails complets** : [Règles métier →](https://Gauthier-Damien.github.io/AdvancedDevSample/domain/entities/)

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

**Documentation API complète** : [API Reference →](https://Gauthier-Damien.github.io/AdvancedDevSample/api/introduction/)

---

## 🛡️ Sécurité

- ✅ **Rate Limiting** - 100 requêtes/minute par IP
- ✅ **Validation automatique** - ModelState et règles métier
- ✅ **Gestion d'erreurs centralisée** - Middleware dédié
- ✅ **HTTPS** - Redirection automatique
- ✅ **Pas de stack trace en production** - Sécurité des données

---

## 📚 Documentation détaillée

### Guides disponibles

- 📖 [**Documentation complète en ligne**](https://Gauthier-Damien.github.io/AdvancedDevSample/) - Documentation MkDocs
- 🚀 [Installation et configuration](https://Gauthier-Damien.github.io/AdvancedDevSample/development/installation/)
- 🏗️ [Architecture détaillée](https://Gauthier-Damien.github.io/AdvancedDevSample/architecture/overview/)
- 🔌 [API Reference](https://Gauthier-Damien.github.io/AdvancedDevSample/api/introduction/)
- 🧪 [Guide des tests](https://Gauthier-Damien.github.io/AdvancedDevSample/tests/unit-tests/)
- 💡 [Bonnes pratiques](https://Gauthier-Damien.github.io/AdvancedDevSample/development/best-practices/)

### Fichiers README spécialisés

- `README-MKDOCS.md` - Guide d'utilisation MkDocs
- `HEBERGEMENT-MKDOCS.md` - Options d'hébergement de la documentation
- `DEPLOIEMENT-GITHUB-PAGES.md` - Guide de déploiement GitHub Pages

---

## 💻 Développement

### Configuration de l'environnement

Voir le [guide d'installation complet](https://Gauthier-Damien.github.io/AdvancedDevSample/development/installation/).

### Conventions de code

- **Classes** : PascalCase (`ProductService`)
- **Méthodes** : PascalCase (`GetAllAsync`)
- **Variables** : camelCase (`productId`)
- **Champs privés** : _camelCase (`_productRepository`)

**Bonnes pratiques complètes** : [Guide →](https://Gauthier-Damien.github.io/AdvancedDevSample/development/best-practices/)

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

Les contributions sont les bienvenues ! Consultez le [guide des bonnes pratiques](https://Gauthier-Damien.github.io/AdvancedDevSample/development/best-practices/) avant de contribuer.

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

- 📖 **[Documentation MkDocs](https://Gauthier-Damien.github.io/AdvancedDevSample/)** ⭐
- 🐙 **[Repository GitHub](https://github.com/Gauthier-Damien/AdvancedDevSample)**
- 📊 **[Swagger UI](https://localhost:5181/swagger)** (après `dotnet run`)

---

## 📊 Statistiques du projet

| Métrique | Valeur |
|----------|--------|
| **Couches** | 4 (API, Application, Domain, Infrastructure) |
| **Tests** | Unitaires + Intégration |
| **Documentation** | 26 pages MkDocs |
| **Endpoints API** | 16+ endpoints REST |
| **Framework** | .NET 9.0 |

---

**⭐ N'oubliez pas de consulter la [documentation complète](https://Gauthier-Damien.github.io/AdvancedDevSample/) ! ⭐**

*Dernière mise à jour : Février 2026*
