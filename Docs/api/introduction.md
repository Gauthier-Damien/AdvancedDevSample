# API - Introduction

## Vue d'ensemble

L'API **AdvancedDevSample** est une API REST développée avec **ASP.NET Core 9.0**. Elle expose des endpoints pour gérer un catalogue produits avec ses entités associées.

## Caractéristiques

- ✅ **Architecture RESTful** : Respect des conventions REST
- ✅ **Documentation OpenAPI/Swagger** : Interface interactive pour tester l'API
- ✅ **Validation automatique** : ModelState et règles métier
- ✅ **Gestion d'erreurs centralisée** : Middleware dédié
- ✅ **Protection DDoS** : Rate limiting par IP
- ✅ **Commentaires XML** : Documentation enrichie dans Swagger

## URL de base

```
Développement : https://localhost:5181/api
Production     : https://yourdomain.com/api
```

## Ressources disponibles

| Ressource | Description | Endpoint |
|-----------|-------------|----------|
| **Products** | Gestion du catalogue produits | `/api/products` |
| **Suppliers** | Gestion des fournisseurs | `/api/suppliers` |
| **Users** | Gestion des utilisateurs | `/api/users` |
| **Orders** | Gestion des commandes | `/api/orders` |

## Format des données

### Request/Response

- **Content-Type** : `application/json`
- **Encodage** : UTF-8

### Exemple de requête

```http
POST /api/products HTTP/1.1
Host: localhost:5181
Content-Type: application/json

{
  "name": "Laptop Dell XPS 15",
  "description": "Ordinateur portable haute performance",
  "price": 1299.99,
  "vatRate": 20.0,
  "supplierId": "550e8400-e29b-41d4-a716-446655440000"
}
```

### Exemple de réponse

```http
HTTP/1.1 201 Created
Content-Type: application/json
Location: /api/products/3fa85f64-5717-4562-b3fc-2c963f66afa6

{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Laptop Dell XPS 15",
  "description": "Ordinateur portable haute performance",
  "price": 1299.99,
  "vatRate": 20.0,
  "isActive": true,
  "supplierId": "550e8400-e29b-41d4-a716-446655440000"
}
```

## Codes de statut HTTP

| Code | Description | Utilisation |
|------|-------------|-------------|
| **200** | OK | Opération réussie (GET, PUT) |
| **201** | Created | Ressource créée (POST) |
| **204** | No Content | Suppression réussie (DELETE) |
| **400** | Bad Request | Validation échouée |
| **404** | Not Found | Ressource introuvable |
| **500** | Internal Server Error | Erreur serveur |

## Gestion des erreurs

Toutes les erreurs retournent un objet JSON standardisé :

```json
{
  "message": "Description de l'erreur",
  "details": "Détails supplémentaires (en dev uniquement)"
}
```

### Exemples d'erreurs

#### Erreur de validation (400)

```json
{
  "message": "Le prix doit être strictement positif",
  "details": "Price: -50 (valeur fournie)"
}
```

#### Ressource introuvable (404)

```json
{
  "message": "Produit avec l'ID '3fa85f64...' introuvable"
}
```

## Rate Limiting

Protection contre les abus et DDoS :

- **Limite** : 100 requêtes par minute par IP
- **En-tête de réponse** : `X-RateLimit-Remaining`

Dépassement de la limite :

```http
HTTP/1.1 429 Too Many Requests
Retry-After: 60

{
  "message": "Trop de requêtes. Veuillez réessayer dans 60 secondes."
}
```

## Accès Swagger UI

Interface interactive pour tester l'API :

```
https://localhost:5181/swagger
```

### Fonctionnalités Swagger

- 📖 Documentation complète de tous les endpoints
- 🧪 Test des requêtes directement depuis le navigateur
- 📝 Exemples de requêtes/réponses
- 🔍 Schémas des modèles de données

## Démarrage rapide

### 1. Lancer l'API

```bash
cd AdvancedDevSample.API
dotnet run
```

### 2. Accéder à Swagger

Ouvrir dans un navigateur :

```
https://localhost:5181/swagger
```

### 3. Tester un endpoint

1. Cliquer sur **GET /api/products**
2. Cliquer sur **"Try it out"**
3. Cliquer sur **"Execute"**
4. Observer la réponse

## Endpoints par ressource

- [Products →](endpoints.md#products)
- [Suppliers →](endpoints.md#suppliers)
- [Users →](endpoints.md#users)
- [Orders →](endpoints.md#orders)

## Middleware

- [Exception Handling →](middlewares.md#exception-handling)
- [Rate Limiting →](middlewares.md#rate-limiting)

## Controllers

- [Architecture des controllers →](controllers.md)

---

*Documentation générée automatiquement à partir des commentaires XML*
