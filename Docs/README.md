# 📚 Documentation - AdvancedDevSample

Bienvenue dans la documentation technique du projet **AdvancedDevSample** !

> ⚠️ **Important** : Ceci est un exercice pédagogique. La documentation est organisée par couches architecturales pour faciliter la navigation.

---

## 🗺️ Commencer ici

### 1️⃣ **[INDEX.md](./INDEX.md)** ← Lisez ceci d'abord
Vue d'ensemble du projet avec graphiques de dépendances et guide de lecture.

---

## 📖 Documentation par couche

### 🎯 **[Couche API](./01_API_Documentation.md)** - Présentation REST
**Endpoints, DTOs, gestion d'erreurs HTTP**

Responsable de :
- Exposer les endpoints REST
- Valider les DTOs
- Formatter les réponses HTTP

**Sections principales :**
- [Endpoints REST](./01_API_Documentation.md#endpoints-rest)
- [Modèles de données (DTOs)](./01_API_Documentation.md#modèles-de-données-dtos)
- [Gestion d'erreurs](./01_API_Documentation.md#gestion-derreurs)

**Exemples :**
```
GET /api/products              → Liste tous les produits
GET /api/products/{id}         → Détails d'un produit
PUT /api/products/{id}/price   → Modifie le prix
```

---

### 🏛️ **[Couche Domain](./02_Domain_Documentation.md)** - Cœur métier
**Entités, Value Objects, Règles métier, Invariants**

Responsable de :
- Définir les entités (Product, Supplier)
- Implémenter les value objects (Price, VAT)
- Valider les règles métier
- Protéger les invariants critiques

**Sections principales :**
- [Concepts métier](./02_Domain_Documentation.md#concepts-métier)
- [Entités](./02_Domain_Documentation.md#entités)
- [Value Objects](./02_Domain_Documentation.md#value-objects)
- [Règles métier](./02_Domain_Documentation.md#règles-métier)

**Règles clés :**
- ✅ Prix > 0 (INVARIANT CRITIQUE)
- ✅ Produit toujours avec prix valide
- ✅ État d'activation (actif/inactif)
- ✅ Promotions valides (0-100%)

---

### 🔧 **[Couche Application](./03_Application_Documentation.md)** - Orchestration
**Services applicatifs, Use Cases, DTOs, Mappage**

Responsable de :
- Orchestrer les use cases
- Appeler les services de domaine
- Mapper Domain ↔ DTOs
- Gérer les transactions

**Sections principales :**
- [Services applicatifs](./03_Application_Documentation.md#services-applicatifs)
- [Use Cases](./03_Application_Documentation.md#use-cases)
- [DTOs](./03_Application_Documentation.md#dtos-data-transfer-objects)
- [Mappage](./03_Application_Documentation.md#mappage-domain--dtos)

**Use Cases implémentés :**
1. Lister tous les produits
2. Afficher un produit
3. Modifier le prix
4. Appliquer une promotion
5. Activer/Désactiver un produit

---

### 💾 **[Couche Infrastructure](./04_Infrastructure_Documentation.md)** - Persistance
**Repositories, Entity Framework Core, Migrations BD**

Responsable de :
- Implémenter les repositories
- Gérer Entity Framework Core
- Créer et appliquer les migrations
- Accéder à la base de données

**Sections principales :**
- [Repositories](./04_Infrastructure_Documentation.md#repositories)
- [Entity Framework Core](./04_Infrastructure_Documentation.md#entity-framework-core)
- [DbContext](./04_Infrastructure_Documentation.md#dbcontext)
- [Migrations](./04_Infrastructure_Documentation.md#migrations)

---

## 🔄 Flux de dépendances

```
┌────────────────────┐
│   API Layer        │  ← Endpoints REST
├────────────────────┤
│ Application Layer  │  ← Services, Use Cases
├────────────────────┤
│ Domain Layer       │  ← Entités, Règles métier
├────────────────────┤
│ Infrastructure     │  ← Repositories, BD
└────────────────────┘
```

**Règle d'or :** Les couches hautes dépendent des couches basses, jamais l'inverse.

---

## 🎓 Guide de lecture par profil

### Pour un **développeur** :
```
1. INDEX.md                          (vue d'ensemble)
   ↓
2. Domain Documentation              (comprendre les concepts)
   ↓
3. Application Documentation         (comprendre la logique)
   ↓
4. Infrastructure Documentation      (comprendre la persistance)
   ↓
5. API Documentation                 (comprendre l'interface)
```

### Pour un **intégrateur API** :
```
1. API Documentation                 (endpoints & modèles)
   ↓
2. Domain Documentation              (si besoin de comprendre les règles)
   ↓
3. Exemples Postman dans Annexe
```

### Pour un **développeur Infrastructure** :
```
1. Domain Documentation              (interfaces/ports)
   ↓
2. Infrastructure Documentation      (implémentation)
   ↓
3. Application Documentation         (use cases)
```

---

## 🔍 Recherche rapide

| Je cherche... | Aller à... |
|---|---|
| Les endpoints disponibles | [API / Endpoints REST](./01_API_Documentation.md#endpoints-rest) |
| Les règles métier | [Domain / Règles métier](./02_Domain_Documentation.md#règles-métier) |
| Comment le prix est validé | [Domain / Invariant de prix](./02_Domain_Documentation.md#règle-3--invariant-de-prix) |
| Les use cases | [Application / Use Cases](./03_Application_Documentation.md#use-cases) |
| Comment mapper Domain → DTO | [Application / Mappage](./03_Application_Documentation.md#mappage-domain--dtos) |
| Comment persister les données | [Infrastructure / Repositories](./04_Infrastructure_Documentation.md#repositories) |
| Les modèles de BD | [Infrastructure / Entity Configurations](./04_Infrastructure_Documentation.md#entity-configurations-fluent-api) |
| Les erreurs possibles | [API / Codes d'erreur](./01_API_Documentation.md#codes-derreur-applicatifs) |

---

## 📊 Vue d'ensemble des fichiers

```
Docs/
├── README.md                              (ce fichier)
├── INDEX.md                               (vue d'ensemble avec liens)
├── 01_API_Documentation.md                (couche Présentation)
├── 02_Domain_Documentation.md             (couche Métier)
├── 03_Application_Documentation.md        (couche Orchestration)
└── 04_Infrastructure_Documentation.md     (couche Persistance)
```

---

## 🔗 Liens d'accès direct

### API
- [Tous les endpoints](./01_API_Documentation.md#endpoints-disponibles)
- [Modèles de données](./01_API_Documentation.md#modèles-de-données-dtos)
- [Codes d'erreur](./01_API_Documentation.md#codes-derreur-applicatifs)
- [Exemples Postman](./01_API_Documentation.md#a-exemples-postman)

### Domain
- [Concepts métier](./02_Domain_Documentation.md#concepts-métier)
- [Entité Product](./02_Domain_Documentation.md#entité--product-agrégat-racine)
- [Value Object Price](./02_Domain_Documentation.md#value-object--price-objet-valeur)
- [Règles métier](./02_Domain_Documentation.md#règles-métier)

### Application
- [IProductService](./03_Application_Documentation.md#iproductservice-portinterface)
- [ProductService](./03_Application_Documentation.md#productservice-implémentation)
- [Tous les DTOs](./03_Application_Documentation.md#dtos-data-transfer-objects)
- [Use Cases](./03_Application_Documentation.md#use-cases)

### Infrastructure
- [ProductRepository](./04_Infrastructure_Documentation.md#productrepository-implémentation)
- [ApplicationDbContext](./04_Infrastructure_Documentation.md#applicationdbcontext)
- [Configurations EF Core](./04_Infrastructure_Documentation.md#entity-configurations-fluent-api)
- [Migrations](./04_Infrastructure_Documentation.md#migrations)

---

## ❓ Questions fréquentes

**Q: Où définir une nouvelle règle métier ?**  
R: Dans la couche [Domain](./02_Domain_Documentation.md), en ajoutant une validation dans la méthode concernée.

**Q: Où ajouter un nouvel endpoint ?**  
R: Dans la couche [API](./01_API_Documentation.md), puis ajouter une méthode au service [Application](./03_Application_Documentation.md).

**Q: Où implémenter l'accès à la base de données ?**  
R: Dans la couche [Infrastructure](./04_Infrastructure_Documentation.md), en implémentant le repository.

**Q: Comment mapper une entité Domain en DTO ?**  
R: Utiliser les mappers dans la couche [Application](./03_Application_Documentation.md#mappage-domain--dtos).

**Q: Où créer une nouvelle table ?**  
R: Créer une Entity Configuration dans [Infrastructure](./04_Infrastructure_Documentation.md#entity-configurations-fluent-api), puis une migration.

---

## 📝 Glossaire rapide

| Terme | Définition | Voir |
|-------|-----------|------|
| **API** | Couche de présentation REST | [01](./01_API_Documentation.md) |
| **Domain** | Cœur métier, entités et règles | [02](./02_Domain_Documentation.md) |
| **Application** | Orchestration des use cases | [03](./03_Application_Documentation.md) |
| **Infrastructure** | Persistance et détails techniques | [04](./04_Infrastructure_Documentation.md) |
| **Use Case** | Scénario d'utilisation du système | [03](./03_Application_Documentation.md#use-cases) |
| **DTO** | Objet de transfert de données | [03](./03_Application_Documentation.md#dtos-data-transfer-objects) |
| **Entity** | Objet avec identité unique | [02](./02_Domain_Documentation.md#entités) |
| **Value Object** | Objet immuable identifié par valeur | [02](./02_Domain_Documentation.md#value-objects) |
| **Repository** | Abstraction de persistance | [04](./04_Infrastructure_Documentation.md#repositories) |
| **Invariant** | Condition toujours vraie | [02](./02_Domain_Documentation.md#règles-métier) |

---

## 🚀 Checklist d'onboarding

- [ ] Lire [INDEX.md](./INDEX.md) pour comprendre l'architecture
- [ ] Lire [Domain](./02_Domain_Documentation.md) pour les concepts métier
- [ ] Lire [Application](./03_Application_Documentation.md) pour la logique
- [ ] Lire [Infrastructure](./04_Infrastructure_Documentation.md) pour la persistance
- [ ] Lire [API](./01_API_Documentation.md) pour l'interface
- [ ] Tester les endpoints avec Postman (exemples dans [API](./01_API_Documentation.md#a-exemples-postman))
- [ ] Exécuter les migrations BD (voir [Infrastructure](./04_Infrastructure_Documentation.md#appliquer-une-migration))
- [ ] Comprendre les [règles métier](./02_Domain_Documentation.md#règles-métier)

---

## 💬 Support

| Question | Réponse |
|----------|--------|
| La doc est confuse ? | Consulter [INDEX.md](./INDEX.md) |
| Je ne sais pas où aller ? | Utiliser le tableau [Recherche rapide](#-recherche-rapide) |
| Je ne comprends pas une couche ? | Lire sa section "Introduction" |
| Je veux voir du code ? | Consulter les sections "Code Example" |

---

## 📅 Historique

| Version | Date | Description |
|---------|------|-------------|
| 1.0 | 28/01/2026 | Documentation initiale structurée par couches |

---

## 👥 Contribuer

Pour mettre à jour la documentation :
1. Identifier la couche concernée
2. Modifier le fichier correspondant
3. Maintenir la cohérence avec les autres sections
4. Mettre à jour les références croisées

---

**Documentation créée avec ❤️ pour faciliter la compréhension du projet.**

*Dernière mise à jour : 28/01/2026*
