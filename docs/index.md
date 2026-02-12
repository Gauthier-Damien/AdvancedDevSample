# Documentation AdvancedDevSample

## 📋 Vue d'ensemble

AdvancedDevSample est une API RESTful développée avec ASP.NET Core 9.0, implémentant une architecture en couches (Clean Architecture) pour la gestion d'un système de commandes, produits, utilisateurs et fournisseurs.

## 🏗️ Architecture

Le projet suit les principes de la **Clean Architecture** avec une séparation claire des responsabilités :

- **API** : Couche de présentation (Controllers, Middlewares)
- **Application** : Couche de logique applicative (Services, DTOs)
- **Domain** : Couche métier (Entités, Interfaces, Règles métier)
- **Infrastructure** : Couche d'accès aux données (Repositories)
- **Test** : Tests unitaires pour toutes les couches

## 🔑 Fonctionnalités principales

### Authentification JWT
- Connexion avec génération de token d'accès
- Refresh token pour renouvellement sécurisé
- Gestion des rôles (Student, Admin)

### Gestion des Produits
- CRUD complet sur le catalogue produit
- Modification de prix avec validation métier
- Application de réductions
- Activation/Désactivation (soft delete)

### Gestion des Commandes
- Création et suivi des commandes
- Machine à états pour les transitions de statut
- Workflow : Pending → Confirmed → Shipped → Delivered
- Annulation avec règles métier

### Gestion des Utilisateurs
- CRUD complet
- Gestion des rôles
- Activation/Désactivation des comptes

### Gestion des Fournisseurs
- CRUD complet
- Association avec les produits
- Validation des données

## 📚 Structure de la documentation

### [Architecture](architecture/index.md)
Détails sur l'architecture en couches et les principes de conception.

### [Domain](domain/index.md)
Documentation des entités métier et des règles de gestion.

### [Application](application/index.md)
Documentation des services applicatifs et des DTOs.

### [API](api/index.md)
Documentation des endpoints REST et des contrôleurs.

### [Infrastructure](infrastructure/index.md)
Documentation des repositories et de la persistance.

## 🚀 Démarrage rapide

### Prérequis
- .NET 9.0 SDK
- Visual Studio 2022 ou Rider

### Lancement
```bash
cd AdvancedDevSample.API
dotnet run
```

### Comptes de démonstration
- **Étudiant** : `demo` / `demo123`
- **Administrateur** : `admin` / `admin123`

### Swagger
Une fois l'application lancée, accédez à :
- https://localhost:7000/swagger

## 🧪 Tests

Le projet contient 137 tests unitaires couvrant :
- Entités du Domain
- Services de l'Application
- Contrôleurs de l'API

```bash
dotnet test
```

## 📊 Qualité du code

Le projet est intégré avec **SonarCloud** pour l'analyse continue de la qualité du code :
- [Voir le rapport SonarCloud](https://sonarcloud.io/project/overview?id=Gauthier-Damien_AdvancedDevSample)

## 🔗 Liens utiles

- [Repository GitHub](https://github.com/Gauthier-Damien/AdvancedDevSample)
- [SonarCloud](https://sonarcloud.io/project/overview?id=Gauthier-Damien_AdvancedDevSample)
