# Changelog - AdvancedDevSample

Toutes les modifications notables du projet seront documentées dans ce fichier.

Le format est basé sur [Keep a Changelog](https://keepachangelog.com/fr/1.0.0/),
et ce projet adhère au [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Non publié]

### À venir
- Documentation complète de l'API avec tous les endpoints
- Diagrammes UML de l'architecture
- Guide de contribution pour les développeurs
- Documentation des tests unitaires et d'intégration
- Guide de déploiement

---

## [1.0.0] - 2026-02-10

### Ajouté

#### Authentification JWT
- Implémentation complète du système d'authentification par JWT
- Middleware de validation des tokens
- Configuration JWT dans `appsettings.json`
- Endpoint `/api/auth/login` pour l'authentification
- Support des rôles utilisateur (Student, Admin)
- Comptes de démonstration pré-configurés
  - Student: `demo` / `demo123`
  - Admin: `admin` / `admin123`

#### Architecture
- Structure en Clean Architecture (4 couches)
- Couche API avec controllers et middlewares
- Couche Application avec services et DTOs
- Couche Domain avec entités et interfaces
- Couche Infrastructure avec repositories
- Pattern Repository pour l'accès aux données
- Injection de dépendances complète

#### Fonctionnalités métier
- Gestion des produits (CRUD)
- Gestion des fournisseurs (CRUD)
- Gestion des utilisateurs (CRUD)
- Gestion des commandes (CRUD)

#### Documentation
- Création de la branche `Docs` dédiée à la documentation
- Organisation des audits et rapports dans `/Docs`
- README principal avec index de la documentation
- ORGANISATION.md avec les standards et workflow
- API_DOCUMENTATION.md (template en cours)
- ARCHITECTURE.md avec description détaillée des couches
- 7 documents d'audit et de rapport :
  - AUDIT_CODE.md
  - AUDIT_COMPLET_FINAL.md
  - CORRECTIFS_PRIORITAIRES.md
  - RAPPORT_CORRECTIFS_APPLIQUES.md
  - JWT_IMPLEMENTATION_SUCCESS.md
  - RECAPITULATIF_JWT_FINAL.md
  - GUIDE_TEST_JWT.md

#### Configuration
- Swagger UI pour la documentation interactive de l'API
- Configuration HTTPS
- Support du développement local
- Génération automatique de commentaires XML

#### Tests
- Structure de tests unitaires (AdvancedDevSample.Test)
- Organisation par couche (Domaine, Application, API)

### Modifié

#### Corrections appliquées
- Correction des namespaces et organisation des dossiers
- Amélioration de la gestion des exceptions
- Standardisation des DTOs
- Optimisation de l'injection de dépendances
- Refactoring des services pour respecter les principes SOLID

### Sécurisé
- Protection des endpoints par authentification JWT
- Validation des tokens à chaque requête
- Gestion sécurisée des mots de passe (à améliorer avec hashing)
- Configuration HTTPS activée

---

## [0.1.0] - 2026-02-09

### Ajouté
- Initialisation du projet AdvancedDevSample
- Structure de base des 4 couches
- Controllers de base (sans authentification)
- Repositories in-memory
- Configuration initiale de Swagger

---

## Format du Changelog

### Types de modifications
- **Ajouté** : nouvelles fonctionnalités
- **Modifié** : changements dans les fonctionnalités existantes
- **Déprécié** : fonctionnalités bientôt supprimées
- **Supprimé** : fonctionnalités supprimées
- **Corrigé** : corrections de bugs
- **Sécurisé** : corrections de vulnérabilités

---

*Pour voir la liste complète des commits, consultez l'historique Git.*
