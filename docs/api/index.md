# Couche API - Controllers et endpoints REST

## 📋 Vue d'ensemble

La couche API expose les fonctionnalités de l'application via des endpoints REST. Elle utilise ASP.NET Core avec documentation Swagger/OpenAPI.

## 🎯 Responsabilités

- **Exposition HTTP** : Endpoints RESTful pour les clients
- **Validation** : Validation automatique des ModelState
- **Documentation** : Swagger/OpenAPI avec commentaires XML
- **Sécurité** : Authentification JWT Bearer
- **Gestion d'erreurs** : Middleware centralisé

## 🔐 Authentification

### Configuration JWT Bearer

L'API utilise l'authentification JWT Bearer configurée dans `Program.cs`.

**Configuration requise (appsettings.json) :**
```json
{
  "JwtSettings": {
    "Secret": "your-secret-key-min-32-characters",
    "Issuer": "AdvancedDevSample",
    "Audience": "AdvancedDevSample-Users",
    "ExpirationMinutes": 60,
    "RefreshTokenExpirationDays": 7
  }
}
```

**Utilisation dans Swagger :**
1. Appeler `/api/Auth/login` avec credentials
2. Copier le `accessToken` de la réponse
3. Cliquer sur "Authorize" dans Swagger
4. Entrer : `Bearer {accessToken}`

## 📦 Controllers

### AuthController - Authentification

**Route de base :** `/api/Auth`

**Endpoints :**

#### `POST /api/Auth/login`
Authentifie un utilisateur et génère un token JWT.

**Request Body :**
```json
{
  "username": "demo",
  "password": "demo123"
}
```

**Response 200 OK :**
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
  "expiresAt": "2026-02-12T15:30:00Z",
  "username": "demo",
  "role": "Student"
}
```

**Responses :**
- `200 OK` : Authentification réussie
- `401 Unauthorized` : Identifiants invalides
- `403 Forbidden` : Compte désactivé

#### `POST /api/Auth/refresh`
Rafraîchit un access token expiré.

**Request Body :**
```json
{
  "refreshToken": "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
}
```

**Response 200 OK :**
Même structure que `/login`

**Responses :**
- `200 OK` : Token rafraîchi
- `401 Unauthorized` : Refresh token invalide ou expiré

---

### ProductController - Gestion des produits

**Route de base :** `/api/Product`  
**Autorisation :** Requise pour toutes les opérations (sauf GET)

**Endpoints :**

#### `GET /api/Product`
Récupère tous les produits actifs.

**Response 200 OK :**
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "Laptop Dell XPS 15",
    "description": "Laptop haute performance",
    "price": 1499.99,
    "vatRate": 20,
    "isActive": true
  }
]
```

**Autorisation :** Non requise

#### `GET /api/Product/{id}`
Récupère un produit par son ID.

**Parameters :**
- `id` (path) : GUID du produit

**Responses :**
- `200 OK` : Produit trouvé
- `404 Not Found` : Produit inexistant

**Autorisation :** Non requise

#### `POST /api/Product`
Crée un nouveau produit.

**Request Body :**
```json
{
  "name": "Laptop Dell XPS 15",
  "description": "Laptop haute performance",
  "price": 1499.99,
  "vatRate": 20
}
```

**Validation :**
- `name` : requis, max 200 caractères
- `description` : max 1000 caractères
- `price` : > 0
- `vatRate` : entre 0 et 100

**Response 201 Created :**
Location header : `/api/Product/{id}`

**Responses :**
- `201 Created` : Produit créé
- `400 Bad Request` : Validation échouée

**Autorisation :** Requise (Bearer token)

#### `PUT /api/Product/{id}/price`
Modifie le prix d'un produit.

**Request Body :**
```json
{
  "price": 1299.99
}
```

**Responses :**
- `200 OK` : Prix modifié
- `400 Bad Request` : Prix invalide (≤ 0)
- `404 Not Found` : Produit inexistant

**Autorisation :** Requise (Bearer token)

#### `PUT /api/Product/{id}/discount`
Applique une réduction en pourcentage.

**Request Body :**
```json
{
  "discountPercentage": 10
}
```

**Validation :**
- `discountPercentage` : entre 0 et 100

**Responses :**
- `200 OK` : Réduction appliquée
- `400 Bad Request` : Pourcentage invalide ou prix final ≤ 0
- `404 Not Found` : Produit inexistant

**Autorisation :** Requise (Bearer token)

#### `PUT /api/Product/{id}/status`
Active ou désactive un produit.

**Request Body :**
```json
{
  "isActive": false
}
```

**Responses :**
- `200 OK` : Statut modifié
- `404 Not Found` : Produit inexistant

**Autorisation :** Requise (Bearer token)

#### `DELETE /api/Product/{id}`
Suppression logique : désactive le produit.

**Responses :**
- `204 No Content` : Produit désactivé
- `404 Not Found` : Produit inexistant

**Autorisation :** Requise (Bearer token)

---

### OrderController - Gestion des commandes

**Route de base :** `/api/Order`  
**Autorisation :** Requise pour toutes les opérations

**Endpoints :**

#### `GET /api/Order`
Récupère toutes les commandes.

**Response 200 OK :**
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "orderNumber": "ORD-20260212-1234-abc123",
    "orderDate": "2026-02-12T10:30:00Z",
    "customerId": "7fa85f64-5717-4562-b3fc-2c963f66afa6",
    "totalAmountExcludingTax": 1000.00,
    "totalAmountIncludingTax": 1200.00,
    "status": 1,
    "statusLabel": "Confirmed",
    "deliveryAddress": "123 Rue de la Paix, Paris",
    "notes": "Livraison avant 18h"
  }
]
```

#### `GET /api/Order/customer/{customerId}`
Récupère les commandes d'un client.

**Parameters :**
- `customerId` (path) : GUID du client

#### `GET /api/Order/{id}`
Récupère une commande par son ID.

**Responses :**
- `200 OK` : Commande trouvée
- `404 Not Found` : Commande inexistante

#### `POST /api/Order`
Crée une nouvelle commande (statut Pending).

**Request Body :**
```json
{
  "customerId": "7fa85f64-5717-4562-b3fc-2c963f66afa6",
  "deliveryAddress": "123 Rue de la Paix, 75001 Paris",
  "notes": "Livraison avant 18h"
}
```

**Validation :**
- `customerId` : requis, GUID valide
- `deliveryAddress` : requis, max 500 caractères
- `notes` : optionnel, max 1000 caractères

**Response 201 Created**

#### `PUT /api/Order/{id}/totals`
Met à jour les montants HT et TTC.

**Request Body :**
```json
{
  "totalAmountExcludingTax": 1000.00,
  "totalAmountIncludingTax": 1200.00
}
```

**Validation :**
- Montant HT ≥ 0
- Montant TTC ≥ Montant HT

**Responses :**
- `200 OK` : Montants mis à jour
- `400 Bad Request` : Validation échouée ou commande annulée

#### `PUT /api/Order/{id}/confirm`
**Transition** : Pending → Confirmed

**Règles métier :**
- Statut actuel doit être Pending
- Montant total doit être > 0

**Responses :**
- `200 OK` : Commande confirmée
- `400 Bad Request` : Transition non autorisée

#### `PUT /api/Order/{id}/ship`
**Transition** : Confirmed → Shipped

**Règles métier :**
- Statut actuel doit être Confirmed

**Responses :**
- `200 OK` : Commande expédiée
- `400 Bad Request` : Transition non autorisée

#### `PUT /api/Order/{id}/deliver`
**Transition** : Shipped → Delivered

**Règles métier :**
- Statut actuel doit être Shipped

**Responses :**
- `200 OK` : Commande livrée
- `400 Bad Request` : Transition non autorisée

#### `PUT /api/Order/{id}/cancel`
Annule une commande.

**Règles métier :**
- Impossible si commande expédiée ou livrée

**Responses :**
- `200 OK` : Commande annulée
- `400 Bad Request` : Annulation non autorisée

---

### UserController - Gestion des utilisateurs

**Route de base :** `/api/User`  
**Autorisation :** Requise pour toutes les opérations

**Endpoints :**

#### `GET /api/User`
Récupère tous les utilisateurs actifs.

#### `GET /api/User/{id}`
Récupère un utilisateur par son ID.

#### `POST /api/User`
Crée un nouvel utilisateur.

**Request Body :**
```json
{
  "username": "jdoe",
  "email": "john.doe@example.com",
  "firstName": "John",
  "lastName": "Doe",
  "role": "Student"
}
```

**Validation :**
- `username` : requis, max 50 caractères
- `email` : requis, format email valide
- `firstName` : requis, max 100 caractères
- `lastName` : requis, max 100 caractères
- `role` : optionnel (par défaut "User")

#### `PUT /api/User/{id}`
Met à jour un utilisateur.

#### `DELETE /api/User/{id}`
Suppression logique : désactive l'utilisateur.

---

### SupplierController - Gestion des fournisseurs

**Route de base :** `/api/Supplier`  
**Autorisation :** Requise pour toutes les opérations

**Endpoints :**

#### `GET /api/Supplier`
Récupère tous les fournisseurs actifs.

#### `GET /api/Supplier/{id}`
Récupère un fournisseur par son ID.

#### `POST /api/Supplier`
Crée un nouveau fournisseur.

**Request Body :**
```json
{
  "name": "Tech Supplier Inc.",
  "email": "contact@techsupplier.com",
  "phoneNumber": "+33123456789",
  "address": "123 Tech Street, Paris"
}
```

**Validation :**
- `name` : requis, max 200 caractères
- `email` : requis, format email
- `phoneNumber` : optionnel, max 20 caractères
- `address` : optionnel, max 500 caractères

#### `PUT /api/Supplier/{id}`
Met à jour un fournisseur.

#### `DELETE /api/Supplier/{id}`
Suppression logique : désactive le fournisseur.

---

## 🛡️ Middleware

### ExceptionHandlingMiddleware

Middleware centralisé pour la gestion des erreurs.

**Emplacement :** `Middlewares/ExceptionHandlingMiddleware.cs`

**Fonctionnement :**
1. Intercepte toutes les exceptions non gérées
2. Log l'erreur
3. Retourne une réponse JSON standardisée

**Format de réponse d'erreur :**
```json
{
  "message": "Description de l'erreur",
  "statusCode": 400
}
```

**Mapping des exceptions :**
- `DomainException` → 400 Bad Request
- `ApplicationServiceException` → Code HTTP défini dans l'exception
- `Exception` (non gérée) → 500 Internal Server Error

**Enregistrement dans Program.cs :**
```csharp
app.UseMiddleware<ExceptionHandlingMiddleware>();
```

---

## 📚 Documentation Swagger

### Configuration

**Titre :** AdvancedDevSample API  
**Version :** v1  
**URL locale :** https://localhost:7000/swagger

### Commentaires XML

Les commentaires XML sont activés pour enrichir la documentation Swagger.

**Configuration (csproj) :**
```xml
<PropertyGroup>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
  <NoWarn>$(NoWarn);1591</NoWarn>
</PropertyGroup>
```

**Exemple de commentaire :**
```csharp
/// <summary>
/// Crée un nouveau produit dans le catalogue
/// </summary>
/// <param name="request">Données du produit à créer</param>
/// <returns>Produit créé</returns>
/// <response code="201">Produit créé avec succès</response>
/// <response code="400">Données invalides</response>
[HttpPost]
[ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public IActionResult CreateProduct([FromBody] CreateProductRequest request)
{
    // ...
}
```

### Sécurité dans Swagger

Configuration de l'authentification Bearer dans Swagger :

```csharp
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
});
```

---

## 🧪 Tests d'intégration

Les controllers sont testés via des tests d'intégration (à venir).

**Exemple avec fichier .http :**
```http
### Login
POST https://localhost:7000/api/Auth/login
Content-Type: application/json

{
  "username": "demo",
  "password": "demo123"
}

### Get Products
GET https://localhost:7000/api/Product
Authorization: Bearer {{accessToken}}
```

---

## 🎯 Bonnes pratiques appliquées

### 1. Versioning d'API
Routes préfixées par `/api/` pour faciliter le versioning futur.

### 2. RESTful
- GET : Lecture
- POST : Création
- PUT : Modification
- DELETE : Suppression

### 3. Codes HTTP appropriés
- 200 OK : Succès
- 201 Created : Ressource créée
- 204 No Content : Suppression réussie
- 400 Bad Request : Validation échouée
- 401 Unauthorized : Non authentifié
- 403 Forbidden : Non autorisé
- 404 Not Found : Ressource inexistante

### 4. Validation automatique
ModelState validé automatiquement par ASP.NET Core.

### 5. Documentation complète
Tous les endpoints documentés avec commentaires XML.
