# API - Endpoints

## Products

Gestion complète du catalogue produits.

### GET /api/products

Récupère la liste de tous les produits.

**Réponse** : `200 OK`

```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "Laptop Dell XPS 15",
    "description": "Ordinateur portable haute performance",
    "price": 1299.99,
    "vatRate": 20.0,
    "isActive": true,
    "supplierId": "550e8400-e29b-41d4-a716-446655440000"
  }
]
```

### GET /api/products/{id}

Récupère un produit par son ID.

**Paramètres** :
- `id` (UUID) : Identifiant unique du produit

**Réponses** :
- `200 OK` : Produit trouvé
- `404 Not Found` : Produit introuvable

### POST /api/products

Crée un nouveau produit.

**Corps de la requête** :

```json
{
  "name": "Laptop Dell XPS 15",
  "description": "Ordinateur portable haute performance",
  "price": 1299.99,
  "vatRate": 20.0,
  "supplierId": "550e8400-e29b-41d4-a716-446655440000"
}
```

**Réponses** :
- `201 Created` : Produit créé avec succès
- `400 Bad Request` : Données invalides

### PUT /api/products/{id}

Met à jour un produit existant.

**Paramètres** :
- `id` (UUID) : Identifiant du produit à modifier

**Corps de la requête** : Même structure que POST

**Réponses** :
- `200 OK` : Modification réussie
- `404 Not Found` : Produit introuvable
- `400 Bad Request` : Données invalides

### DELETE /api/products/{id}

Supprime un produit.

**Paramètres** :
- `id` (UUID) : Identifiant du produit à supprimer

**Réponses** :
- `204 No Content` : Suppression réussie
- `404 Not Found` : Produit introuvable

---

## Suppliers

Gestion des fournisseurs.

### GET /api/suppliers

Liste tous les fournisseurs.

### GET /api/suppliers/{id}

Récupère un fournisseur par ID.

### POST /api/suppliers

Crée un nouveau fournisseur.

**Corps de la requête** :

```json
{
  "name": "Dell Inc.",
  "email": "contact@dell.com",
  "phone": "+33 1 23 45 67 89"
}
```

### PUT /api/suppliers/{id}

Met à jour un fournisseur.

### DELETE /api/suppliers/{id}

Supprime un fournisseur.

---

## Users

Gestion des utilisateurs.

### GET /api/users

Liste tous les utilisateurs.

### GET /api/users/{id}

Récupère un utilisateur par ID.

### POST /api/users

Crée un nouvel utilisateur.

**Corps de la requête** :

```json
{
  "firstName": "Jean",
  "lastName": "Dupont",
  "email": "jean.dupont@example.com"
}
```

### PUT /api/users/{id}

Met à jour un utilisateur.

### DELETE /api/users/{id}

Supprime un utilisateur.

---

## Orders

Gestion des commandes.

### GET /api/orders

Liste toutes les commandes.

### GET /api/orders/{id}

Récupère une commande par ID.

### POST /api/orders

Crée une nouvelle commande.

**Corps de la requête** :

```json
{
  "userId": "550e8400-e29b-41d4-a716-446655440001",
  "orderDate": "2026-02-09T10:30:00Z",
  "items": [
    {
      "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "quantity": 2,
      "unitPrice": 1299.99
    }
  ]
}
```

### PUT /api/orders/{id}

Met à jour une commande.

### DELETE /api/orders/{id}

Supprime une commande.
