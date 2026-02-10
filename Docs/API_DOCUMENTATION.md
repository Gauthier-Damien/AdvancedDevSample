# Documentation de l'API - AdvancedDevSample

> 🚧 **En cours de rédaction** - Cette documentation sera complétée prochainement

## Vue d'ensemble

L'API AdvancedDevSample est une API RESTful construite avec ASP.NET Core 9.0, suivant les principes de Clean Architecture.

## Endpoints

### 🔐 Authentification (`/api/auth`)

#### POST /api/auth/login
Authentifie un utilisateur et retourne un token JWT.

**Request Body:**
```json
{
  "username": "string",
  "password": "string"
}
```

**Response (200 OK):**
```json
{
  "token": "string",
  "expiresAt": "datetime"
}
```

**Response (401 Unauthorized):**
```json
{
  "message": "Invalid credentials"
}
```

---

### 👥 Utilisateurs (`/api/users`)

#### GET /api/users
Récupère la liste de tous les utilisateurs.

**Authorization:** Bearer Token requis

**Response (200 OK):**
```json
[
  {
    "id": "int",
    "name": "string",
    "email": "string",
    "role": "string"
  }
]
```

#### POST /api/users
Crée un nouvel utilisateur.

**Authorization:** Bearer Token requis (Admin uniquement)

**Request Body:**
```json
{
  "name": "string",
  "email": "string",
  "password": "string",
  "role": "string"
}
```

---

### 📦 Produits (`/api/products`)

#### GET /api/products
Récupère la liste de tous les produits.

#### GET /api/products/{id}
Récupère un produit spécifique.

#### POST /api/products
Crée un nouveau produit.

#### PUT /api/products/{id}
Met à jour un produit existant.

#### DELETE /api/products/{id}
Supprime un produit.

---

### 🏢 Fournisseurs (`/api/suppliers`)

#### GET /api/suppliers
Récupère la liste de tous les fournisseurs.

#### GET /api/suppliers/{id}
Récupère un fournisseur spécifique.

#### POST /api/suppliers
Crée un nouveau fournisseur.

#### PUT /api/suppliers/{id}
Met à jour un fournisseur existant.

#### DELETE /api/suppliers/{id}
Supprime un fournisseur.

---

### 📋 Commandes (`/api/orders`)

#### GET /api/orders
Récupère la liste de toutes les commandes.

#### GET /api/orders/{id}
Récupère une commande spécifique.

#### POST /api/orders
Crée une nouvelle commande.

#### PUT /api/orders/{id}
Met à jour une commande existante.

#### DELETE /api/orders/{id}
Supprime une commande.

---

## Codes de statut HTTP

| Code | Description |
|------|-------------|
| 200 | OK - Requête réussie |
| 201 | Created - Ressource créée avec succès |
| 204 | No Content - Suppression réussie |
| 400 | Bad Request - Données invalides |
| 401 | Unauthorized - Authentification requise |
| 403 | Forbidden - Permissions insuffisantes |
| 404 | Not Found - Ressource introuvable |
| 500 | Internal Server Error - Erreur serveur |

## Authentification

L'API utilise JWT (JSON Web Tokens) pour l'authentification :

1. **Obtenir un token** : `POST /api/auth/login`
2. **Utiliser le token** : Ajouter le header `Authorization: Bearer {token}` à chaque requête

### Comptes de démonstration

**Étudiant :**
- Username: `demo`
- Password: `demo123`
- Role: `Student`

**Administrateur :**
- Username: `admin`
- Password: `admin123`
- Role: `Admin`

## Gestion des erreurs

Toutes les erreurs sont retournées au format JSON :

```json
{
  "message": "Description de l'erreur",
  "details": "Détails supplémentaires (optionnel)"
}
```

## Exemples d'utilisation

### Avec cURL

```bash
# Login
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"demo","password":"demo123"}'

# Utiliser le token
curl -X GET http://localhost:5000/api/products \
  -H "Authorization: Bearer {votre_token}"
```

### Avec Swagger

1. Accéder à `http://localhost:5000/swagger`
2. Cliquer sur le cadenas 🔒 en haut à droite
3. Entrer le token au format : `Bearer {votre_token}`
4. Tester les endpoints directement depuis l'interface

## Configuration

Voir le fichier `appsettings.json` pour la configuration JWT :

```json
{
  "Jwt": {
    "SecretKey": "votre_cle_secrete_jwt",
    "Issuer": "AdvancedDevSample",
    "Audience": "AdvancedDevSampleUsers",
    "ExpirationMinutes": 60
  }
}
```

---

> 📝 **Note :** Cette documentation sera enrichie avec plus de détails sur chaque endpoint, les validations, et des exemples complets.

*Dernière mise à jour : 2026-02-10*
