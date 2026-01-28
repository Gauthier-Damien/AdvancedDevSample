# 📍 Guide de Navigation - Docs

Vous vous perdez dans la documentation ? Ce guide vous aidera à trouver ce que vous cherchez ! 🗺️

---

## 🎯 Où commencer ?

### Je ne sais rien du projet
→ Lire dans cet ordre:
1. **[README.md](./README.md)** (ce fichier structure la doc)
2. **[ARCHITECTURE_OVERVIEW.md](./ARCHITECTURE_OVERVIEW.md)** (schémas et diagrammes)
3. **[INDEX.md](./INDEX.md)** (vue d'ensemble détaillée)

### Je dois implémenter une fonctionnalité
→ Lire:
1. **[02_Domain_Documentation.md](./02_Domain_Documentation.md)** (comprendre les règles)
2. **[03_Application_Documentation.md](./03_Application_Documentation.md)** (orchestrer)
3. **[04_Infrastructure_Documentation.md](./04_Infrastructure_Documentation.md)** (persister)
4. **[01_API_Documentation.md](./01_API_Documentation.md)** (exposer)

### Je dois tester l'API
→ Lire:
1. **[01_API_Documentation.md](./01_API_Documentation.md)** → Section "Exemples Postman"

### Je dois comprendre une erreur
→ Lire:
1. **[01_API_Documentation.md](./01_API_Documentation.md)** → "Codes d'erreur"
2. **[02_Domain_Documentation.md](./02_Domain_Documentation.md)** → "Règles métier"

---

## 🔍 Recherche par mot-clé

### "Price" / "Prix"
| Qui en parle ? | Quoi ? |
|---|---|
| **[02_Domain](./02_Domain_Documentation.md#value-object--price-objet-valeur)** | ✅ Définition du Value Object Price |
| **[02_Domain](./02_Domain_Documentation.md#règle-1--prix-strictement-positif-priorité-critique)** | ✅ Règle métier "Prix > 0" |
| **[02_Domain](./02_Domain_Documentation.md#règle-3--invariant-de-prix-priorité-critique)** | ✅ Invariant de prix |
| **[03_Application](./03_Application_Documentation.md#updatepricedto)** | ✅ DTO UpdatePriceRequest |
| **[01_API](./01_API_Documentation.md#3-modifier-le-prix-dun-produit)** | ✅ Endpoint PUT /price |

### "Product" / "Produit"
| Qui en parle ? | Quoi ? |
|---|---|
| **[02_Domain](./02_Domain_Documentation.md#entité--product-agrégat-racine)** | ✅ Définition de l'entité Product |
| **[03_Application](./03_Application_Documentation.md#productservice-implémentation)** | ✅ Service ProductService |
| **[04_Infrastructure](./04_Infrastructure_Documentation.md#productrepository-implémentation)** | ✅ Repository ProductRepository |
| **[01_API](./01_API_Documentation.md#endpoints-disponibles)** | ✅ Tous les endpoints produit |

### "Validation" / "Règles métier"
| Qui en parle ? | Quoi ? |
|---|---|
| **[02_Domain](./02_Domain_Documentation.md#règles-métier)** | ✅ Les 5 règles métier |
| **[03_Application](./03_Application_Documentation.md#gestion-des-erreurs)** | ✅ Mappage des exceptions |
| **[01_API](./01_API_Documentation.md#gestion-derreurs)** | ✅ Codes d'erreur HTTP |

### "Repository" / "Persistance"
| Qui en parle ? | Quoi ? |
|---|---|
| **[02_Domain](./02_Domain_Documentation.md#ports-interfaces)** | ✅ Interface IProductRepository |
| **[04_Infrastructure](./04_Infrastructure_Documentation.md#repositories)** | ✅ Implémentation ProductRepository |
| **[04_Infrastructure](./04_Infrastructure_Documentation.md#dbcontext)** | ✅ ApplicationDbContext |

### "Exception" / "Erreur"
| Qui en parle ? | Quoi ? |
|---|---|
| **[02_Domain](./02_Domain_Documentation.md#exceptions)** | ✅ DomainException |
| **[03_Application](./03_Application_Documentation.md#gestion-des-erreurs)** | ✅ ApplicationException, EntityNotFoundException |
| **[01_API](./01_API_Documentation.md#gestion-derreurs)** | ✅ Codes HTTP et codes métier |

---

## 🎭 Par profil

### 👨‍💻 Développeur Backend C#
```
Je comprends le code .NET
Je dois implémenter une feature
Je dois déboguer un problème

Chemin de lecture:
├─ ARCHITECTURE_OVERVIEW.md    (comprendre globalement)
├─ 02_Domain_Documentation.md  (entités et règles)
├─ 03_Application_Documentation.md (services)
├─ 04_Infrastructure_Documentation.md (persistance)
└─ 01_API_Documentation.md     (exposition)
```

### 🌐 Intégrateur API / Frontend
```
Je dois appeler l'API
Je dois comprendre les endpoints
Je dois gérer les erreurs

Chemin de lecture:
├─ README.md                   (orientation)
├─ ARCHITECTURE_OVERVIEW.md    (vue globale)
├─ 01_API_Documentation.md     (endpoints, DTOs, erreurs)
└─ Exemples Postman            (tester)
```

### 🏛️ Architecte / Tech Lead
```
Je dois vérifier l'architecture
Je dois valider les patterns
Je dois onboarder des devs

Chemin de lecture:
├─ INDEX.md                    (vue d'ensemble)
├─ ARCHITECTURE_OVERVIEW.md    (schémas)
├─ 02_Domain_Documentation.md  (DDD, invariants)
├─ 03_Application_Documentation.md (patterns)
├─ 04_Infrastructure_Documentation.md (choix tech)
└─ 01_API_Documentation.md     (contrats)
```

### 📊 Product Manager / Business Analyst
```
Je dois comprendre les fonctionnalités
Je dois valider les use cases
Je dois documenter les règles métier

Chemin de lecture:
├─ README.md                   (orientation)
├─ ARCHITECTURE_OVERVIEW.md    (les 5 use cases)
├─ 02_Domain_Documentation.md  (règles métier)
└─ 03_Application_Documentation.md (use cases détaillés)
```

---

## 📚 Par cas d'usage

### UC1: "Lister les produits"
**Pour comprendre, consulter:**
1. [ARCHITECTURE_OVERVIEW.md](./ARCHITECTURE_OVERVIEW.md#les-5-use-cases) - Présentation
2. [03_Application_Documentation.md](./03_Application_Documentation.md#uc1-lister-tous-les-produits) - Logique
3. [01_API_Documentation.md](./01_API_Documentation.md#1-lister-tous-les-produits) - Endpoint

### UC2: "Afficher un produit"
**Pour comprendre, consulter:**
1. [ARCHITECTURE_OVERVIEW.md](./ARCHITECTURE_OVERVIEW.md#les-5-use-cases)
2. [03_Application_Documentation.md](./03_Application_Documentation.md#uc2-afficher-un-produit)
3. [01_API_Documentation.md](./01_API_Documentation.md#2-récupérer-un-produit-par-id)

### UC3: "Modifier le prix" ⭐ (Cas critique avec invariant)
**Pour comprendre, consulter:**
1. [ARCHITECTURE_OVERVIEW.md](./ARCHITECTURE_OVERVIEW.md#cas-dusage--modifier-le-prix-dun-produit)
2. [02_Domain_Documentation.md](./02_Domain_Documentation.md#règle-1--prix-strictement-positif-priorité-critique)
3. [03_Application_Documentation.md](./03_Application_Documentation.md#uc3-modifier-le-prix)
4. [01_API_Documentation.md](./01_API_Documentation.md#3-modifier-le-prix-dun-produit)

### UC4: "Appliquer une promotion"
**Pour comprendre, consulter:**
1. [02_Domain_Documentation.md](./02_Domain_Documentation.md#règle-5--promotion-valide-priorité-haute)
2. [03_Application_Documentation.md](./03_Application_Documentation.md#uc4-appliquer-une-promotion)
3. [01_API_Documentation.md](./01_API_Documentation.md#4-appliquer-une-promotion)

### UC5: "Activer/Désactiver produit"
**Pour comprendre, consulter:**
1. [02_Domain_Documentation.md](./02_Domain_Documentation.md#règle-4--état-dactivation-priorité-moyenne)
2. [03_Application_Documentation.md](./03_Application_Documentation.md#uc5-modifier-le-statut)
3. [01_API_Documentation.md](./01_API_Documentation.md#5-modifier-le-statut-dactivation)

---

## 🔥 Sujets "chauds" (fréquemment visités)

### 1. "Pourquoi l'API retourne 409 quand je modifie le prix ?"
→ **Lire**: [02_Domain_Documentation.md](./02_Domain_Documentation.md#règle-1--prix-strictement-positif-priorité-critique) + [01_API_Documentation.md](./01_API_Documentation.md#codes-derreur-applicatifs)

### 2. "Où définir une nouvelle règle métier ?"
→ **Lire**: [02_Domain_Documentation.md](./02_Domain_Documentation.md#règles-métier)

### 3. "Comment mapper un DTO en entité Domain ?"
→ **Lire**: [03_Application_Documentation.md](./03_Application_Documentation.md#mappage-domain--dtos)

### 4. "Comment créer une migration ?"
→ **Lire**: [04_Infrastructure_Documentation.md](./04_Infrastructure_Documentation.md#créer-une-migration)

### 5. "Quel endpoint doit-je appeler pour... ?"
→ **Lire**: [01_API_Documentation.md](./01_API_Documentation.md#endpoints-disponibles)

### 6. "Quels codes d'erreur sont possibles ?"
→ **Lire**: [01_API_Documentation.md](./01_API_Documentation.md#codes-derreur-applicatifs)

### 7. "Comment les exceptions se propagent ?"
→ **Lire**: [ARCHITECTURE_OVERVIEW.md](./ARCHITECTURE_OVERVIEW.md#gestion-derreur--prix-invalide)

---

## 🎓 Exercices pratiques

### Exercice 1: Tracer un appel API
**Objectif**: Comprendre le flux complet
**À faire**:
1. Lire [ARCHITECTURE_OVERVIEW.md](./ARCHITECTURE_OVERVIEW.md#cas-dusage--modifier-le-prix-dun-produit)
2. Ouvrir Postman et appeler `PUT /api/products/{id}/price`
3. Ajouter une breakpoint dans ProductController
4. Suivre le flux jusqu'à la base de données

### Exercice 2: Ajouter une nouvelle règle métier
**Objectif**: Comprendre où implémenter
**À faire**:
1. Lire [02_Domain_Documentation.md](./02_Domain_Documentation.md#entité--product-agrégat-racine)
2. Modifier la méthode `UpdatePrice()` pour ajouter une nouvelle validation
3. Lire [03_Application_Documentation.md](./03_Application_Documentation.md#productservice-implémentation)
4. Vérifier que la couche Application mappe l'exception correctement
5. Tester avec Postman

### Exercice 3: Créer une migration
**Objectif**: Ajouter un champ à Product
**À faire**:
1. Lire [04_Infrastructure_Documentation.md](./04_Infrastructure_Documentation.md#productconfiguration)
2. Modifier ProductConfiguration pour ajouter un champ
3. Créer une migration
4. Appliquer la migration
5. Vérifier le schema en SQL

---

## 📋 Checklist "Je suis perdu"

- [ ] Je suis déjà passé par [README.md](./README.md) ?
- [ ] J'ai lu [ARCHITECTURE_OVERVIEW.md](./ARCHITECTURE_OVERVIEW.md) ?
- [ ] J'ai lu [INDEX.md](./INDEX.md) ?
- [ ] J'ai identifié mon profil dans "Par profil" ci-dessus ?
- [ ] J'ai trouvé le sujet pertinent dans "Recherche par mot-clé" ?
- [ ] J'ai consulté le cas d'usage correspondant dans "Par cas d'usage" ?
- [ ] J'ai cherché dans "Sujets chauds" ?

Si après tout ça vous êtes encore perdu, vous pouvez:
1. Utiliser Ctrl+F pour rechercher dans les docs
2. Consulter les exemples de code dans chaque documentation
3. Regarder les diagrammes dans [ARCHITECTURE_OVERVIEW.md](./ARCHITECTURE_OVERVIEW.md)

---

## 🗂️ Vue d'ensemble des fichiers

```
Docs/
├─ README.md                          ← Vous êtes probablement ici d'abord
├─ GUIDE_NAVIGATION.md               ← Ce fichier (aide à se repérer)
├─ ARCHITECTURE_OVERVIEW.md          ← Schémas et diagrammes (très visuel)
├─ INDEX.md                          ← Vue d'ensemble complète
│
├─ 01_API_Documentation.md           ← COUCHE API
│  ├─ Endpoints REST
│  ├─ DTOs (requête/réponse)
│  ├─ Codes d'erreur
│  └─ Exemples Postman
│
├─ 02_Domain_Documentation.md        ← COUCHE DOMAIN (CŒUR MÉTIER)
│  ├─ Concepts métier
│  ├─ Entités (Product, Supplier)
│  ├─ Value Objects (Price, VAT)
│  ├─ Règles métier (5 règles)
│  └─ Invariants (Price > 0)
│
├─ 03_Application_Documentation.md   ← COUCHE APPLICATION
│  ├─ Services applicatifs
│  ├─ Use Cases (5 cas)
│  ├─ DTOs (internes)
│  └─ Mappage Domain ↔ DTO
│
└─ 04_Infrastructure_Documentation.md ← COUCHE INFRASTRUCTURE
   ├─ Repositories
   ├─ Entity Framework Core
   ├─ DbContext & Configurations
   └─ Migrations
```

---

## 💡 Tips & Tricks

### Astuce 1: Utiliser les ancres
Tous les fichiers utilisent des sections avec `#`. Vous pouvez y accéder directement:
```
[02_Domain](./02_Domain_Documentation.md#règle-1--prix-strictement-positif-priorité-critique)
```

### Astuce 2: Chercher un mot-clé
Utiliser Ctrl+F pour chercher dans une doc:
- "invariant" → trouve toutes les mentions
- "UpdatePrice" → trouve où elle est utilisée
- "409" → trouve les erreurs 409

### Astuce 3: Lire les sections "Annexe"
Chaque doc a une section Annexe avec:
- Diagrammes
- Tableaux
- Glossaire
- Références croisées

### Astuce 4: Suivre les liens
Chaque doc contient des liens vers les autres. En suivant les liens, vous apprendrez comment les couches interagissent.

### Astuce 5: Lire le code d'exemple
Chaque section "Code Example" contient du vrai code C#. Essayez de le comprendre !

---

## 🆘 Besoin d'aide ?

| Question | Réponse |
|----------|--------|
| "Où est X ?" | Utiliser le tableau "Recherche par mot-clé" |
| "Comment faire Y ?" | Utiliser le tableau "Sujets chauds" ou "Par cas d'usage" |
| "Je ne comprends pas Z" | Lire la section "Introduction" du doc correspondant |
| "Quel code modifie X ?" | Chercher dans "Par profil" ou "Par cas d'usage" |
| "Je suis complètement perdu" | Lire dans l'ordre: README → ARCHITECTURE_OVERVIEW → INDEX |

---

**Guide de Navigation - AdvancedDevSample Docs**

*Créé pour vous aider à naviguer efficacement dans la documentation. 🗺️*

*Dernière mise à jour: 28/01/2026*
