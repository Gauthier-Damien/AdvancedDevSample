# Couche Application - Services et DTOs

## 📋 Vue d'ensemble

La couche Application orchestre les cas d'usage de l'application. Elle coordonne les entités du Domain et transforme les données entre les DTOs et les entités métier.

## 🎯 Responsabilités

- **Orchestration** : Coordonne les appels aux entités et repositories
- **Transformation** : Convertit DTO ↔ Entité Domain
- **Validation** : Valide les données d'entrée (Data Annotations)
- **Gestion d'erreurs** : Capture et transforme les exceptions Domain

## 📦 Services

### ProductService - Gestion du catalogue produit

**Responsabilité :** Orchestration des opérations CRUD sur les produits.

**Méthodes principales :**

#### `GetAllProducts() : IEnumerable<ProductResponse>`
Récupère tous les produits actifs du catalogue.

#### `GetProductById(Guid id) : ProductResponse`
Récupère un produit par son identifiant.

**Exceptions :**
- `ApplicationServiceException` (404) si le produit n'existe pas

#### `CreateProduct(CreateProductRequest) : ProductResponse`
Crée un nouveau produit dans le catalogue.

**Validation DTO :**
- Name : requis, max 200 caractères
- Description : max 1000 caractères
- Price : > 0
- VATRate : entre 0 et 100

#### `ChangeProductPrice(Guid id, decimal newPrice) : ProductResponse`
Modifie le prix d'un produit existant.

**Règles métier appliquées :**
- Le nouveau prix doit être > 0 (validé par le Domain)

#### `ApplyDiscount(Guid id, decimal percentage) : ProductResponse`
Applique une réduction en pourcentage sur le prix.

**Règles métier appliquées :**
- Le pourcentage doit être entre 0 et 100
- Le prix final doit rester > 0

#### `ToggleProductStatus(Guid id, bool isActive) : ProductResponse`
Active ou désactive un produit.

#### `DeleteProduct(Guid id) : void`
Suppression logique (soft delete) : désactive le produit au lieu de le supprimer.

---

### OrderService - Gestion des commandes

**Responsabilité :** Orchestration du cycle de vie des commandes avec machine à états.

**Méthodes principales :**

#### `GetAllOrders() : IEnumerable<OrderResponse>`
Récupère toutes les commandes.

#### `GetOrdersByCustomer(Guid customerId) : IEnumerable<OrderResponse>`
Récupère les commandes d'un client spécifique.

#### `GetOrderById(Guid id) : OrderResponse`
Récupère une commande par son identifiant.

#### `CreateOrder(CreateOrderRequest) : OrderResponse`
Crée une nouvelle commande avec le statut "Pending".

**Validation DTO :**
- CustomerId : requis
- DeliveryAddress : requis, max 500 caractères
- Notes : optionnel, max 1000 caractères

#### `UpdateOrderTotals(Guid id, UpdateOrderTotalsRequest) : OrderResponse`
Met à jour les montants HT et TTC de la commande.

**Règles métier appliquées :**
- Montant HT ≥ 0
- Montant TTC ≥ Montant HT
- Impossible si la commande est annulée

#### `ConfirmOrder(Guid id) : OrderResponse`
**Transition** : Pending → Confirmed

**Règles métier appliquées :**
- Le statut actuel doit être "Pending"
- Le montant doit être > 0

#### `ShipOrder(Guid id) : OrderResponse`
**Transition** : Confirmed → Shipped

**Règles métier appliquées :**
- Le statut actuel doit être "Confirmed"

#### `DeliverOrder(Guid id) : OrderResponse`
**Transition** : Shipped → Delivered

**Règles métier appliquées :**
- Le statut actuel doit être "Shipped"

#### `CancelOrder(Guid id) : OrderResponse`
**Transition** : Pending/Confirmed → Cancelled

**Règles métier appliquées :**
- Impossible si la commande est expédiée ou livrée

---

### UserService - Gestion des utilisateurs

**Responsabilité :** CRUD complet sur les utilisateurs du système.

**Méthodes principales :**

#### `GetAllUsers() : IEnumerable<UserResponse>`
Récupère tous les utilisateurs actifs.

#### `GetUserById(Guid id) : UserResponse`
Récupère un utilisateur par son identifiant.

#### `CreateUser(CreateUserRequest) : UserResponse`
Crée un nouvel utilisateur.

**Validation DTO :**
- Username : requis, max 50 caractères
- Email : requis, format email
- FirstName : requis, max 100 caractères
- LastName : requis, max 100 caractères
- Role : optionnel (par défaut "User")

#### `UpdateUser(Guid id, UpdateUserRequest) : UserResponse`
Met à jour les informations d'un utilisateur.

#### `DeleteUser(Guid id) : void`
Suppression logique : désactive l'utilisateur.

---

### SupplierService - Gestion des fournisseurs

**Responsabilité :** CRUD complet sur les fournisseurs.

**Méthodes principales :**

#### `GetAllSuppliers() : IEnumerable<SupplierResponse>`
Récupère tous les fournisseurs actifs.

#### `GetSupplierById(Guid id) : SupplierResponse`
Récupère un fournisseur par son identifiant.

#### `CreateSupplier(CreateSupplierRequest) : SupplierResponse`
Crée un nouveau fournisseur.

**Validation DTO :**
- Name : requis, max 200 caractères
- Email : requis, format email
- PhoneNumber : optionnel, max 20 caractères
- Address : optionnel, max 500 caractères

#### `UpdateSupplier(Guid id, UpdateSupplierRequest) : SupplierResponse`
Met à jour les informations d'un fournisseur.

#### `DeleteSupplier(Guid id) : void`
Suppression logique : désactive le fournisseur.

---

### AuthService - Authentification JWT

**Responsabilité :** Gestion de l'authentification et des tokens JWT.

**Méthodes principales :**

#### `Login(LoginRequest) : LoginResponse`
Authentifie un utilisateur et génère un token d'accès JWT.

**Processus :**
1. Récupère l'utilisateur par username
2. Vérifie le mot de passe avec BCrypt
3. Vérifie que le compte est actif
4. Génère un access token (JWT) et un refresh token
5. Sauvegarde le refresh token en base
6. Retourne les tokens et les informations utilisateur

**Validation DTO :**
- Username : requis
- Password : requis

**Exceptions :**
- `ApplicationServiceException` (401) si les identifiants sont invalides
- `ApplicationServiceException` (403) si le compte est désactivé

**Claims JWT générés :**
- `NameIdentifier` : Id de l'utilisateur
- `Name` : Username
- `Email` : Email de l'utilisateur
- `Role` : Rôle (Student, Admin, User)
- `FullName` : Prénom + Nom

#### `RefreshToken(RefreshTokenRequest) : LoginResponse`
Rafraîchit un access token expiré avec un refresh token valide.

**Processus :**
1. Récupère le refresh token
2. Vérifie qu'il est valide (non expiré, non révoqué)
3. Récupère l'utilisateur associé
4. Révoque l'ancien refresh token
5. Génère de nouveaux tokens
6. Retourne les nouveaux tokens

**Validation DTO :**
- RefreshToken : requis

**Exceptions :**
- `ApplicationServiceException` (401) si le refresh token est invalide ou expiré

**Configuration JWT :**
- Secret : Défini dans appsettings.json
- Issuer : AdvancedDevSample
- Audience : AdvancedDevSample-Users
- Expiration access token : 60 minutes (configurable)
- Expiration refresh token : 7 jours (configurable)

---

## 📋 DTOs (Data Transfer Objects)

### Principes
- Séparation Request/Response
- Validation avec Data Annotations
- Propriétés publiques pour binding

### Structure

```
DTOs/
├── Auth/
│   ├── LoginRequest.cs
│   ├── LoginResponse.cs
│   └── RefreshTokenRequest.cs
├── Products/
│   ├── CreateProductRequest.cs
│   ├── ProductResponse.cs
│   ├── ChangePriceRequest.cs
│   ├── ApplyDiscountRequest.cs
│   └── ToggleProductStatusRequest.cs
├── Orders/
│   ├── CreateOrderRequest.cs
│   ├── OrderResponse.cs
│   └── UpdateOrderTotalsRequest.cs
├── Users/
│   ├── CreateUserRequest.cs
│   ├── UpdateUserRequest.cs
│   └── UserResponse.cs
└── Suppliers/
    ├── CreateSupplierRequest.cs
    ├── UpdateSupplierRequest.cs
    └── SupplierResponse.cs
```

### Exemples de validation

#### CreateProductRequest
```csharp
public class CreateProductRequest
{
    [Required(ErrorMessage = "Le nom est obligatoire")]
    [MaxLength(200)]
    public string Name { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0")]
    public decimal Price { get; set; }

    [Required]
    [Range(0, 100, ErrorMessage = "Le taux de TVA doit être entre 0 et 100")]
    public decimal VATRate { get; set; }
}
```

#### LoginRequest
```csharp
public class LoginRequest
{
    [Required(ErrorMessage = "Le nom d'utilisateur est obligatoire")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Le mot de passe est obligatoire")]
    public string Password { get; set; }
}
```

#### OrderResponse
```csharp
public class OrderResponse
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalAmountExcludingTax { get; set; }
    public decimal TotalAmountIncludingTax { get; set; }
    public OrderStatus Status { get; set; }
    public string StatusLabel { get; set; } // ToString() du Status
    public string DeliveryAddress { get; set; }
    public string Notes { get; set; }
}
```

---

## ⚠️ Gestion des erreurs

### ApplicationServiceException

Exception levée par la couche Application pour les erreurs applicatives.

```csharp
public class ApplicationServiceException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public ApplicationServiceException(string message, HttpStatusCode statusCode)
        : base(message)
    {
        StatusCode = statusCode;
    }
}
```

**Codes HTTP utilisés :**
- `404 Not Found` : Ressource non trouvée
- `401 Unauthorized` : Authentification échouée
- `403 Forbidden` : Compte désactivé
- `400 Bad Request` : Données invalides (via ModelState)

### Conversion des exceptions Domain

Les services capturent les `DomainException` et les convertissent en `ApplicationServiceException` avec le code HTTP approprié.

```csharp
try
{
    product.UpdatePrice(newPrice);
}
catch (DomainException ex)
{
    throw new ApplicationServiceException(ex.Message, HttpStatusCode.BadRequest);
}
```

---

## 🧪 Tests

Tous les services sont couverts par des tests unitaires avec des repositories fake (in-memory).

**Exemples de tests :**
- `ProductServiceTests.cs` : 15+ tests
- `OrderServiceTests.cs` : 20+ tests
- `UserServiceTests.cs` : 10+ tests
- `SupplierServiceTests.cs` : 10+ tests

**Pattern de test :**
```csharp
[Fact]
public void CreateProduct_Should_Create_And_Return_Product()
{
    // Arrange
    var request = new CreateProductRequest
    {
        Name = "Test Product",
        Price = 99.99m,
        VATRate = 20
    };

    // Act
    var result = _productService.CreateProduct(request);

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Test Product", result.Name);
    Assert.Equal(99.99m, result.Price);
}
```

---

## 🎯 Bonnes pratiques appliquées

### 1. Séparation Request/Response
Pas de réutilisation des DTOs entre requêtes et réponses.

### 2. Validation déclarative
Utilisation de Data Annotations pour la validation.

### 3. Mapping explicite
Méthodes privées `MapToResponse()` pour la conversion.

### 4. Gestion d'erreurs cohérente
Toutes les erreurs passent par `ApplicationServiceException`.

### 5. Injection de dépendances
Les repositories sont injectés via le constructeur.
