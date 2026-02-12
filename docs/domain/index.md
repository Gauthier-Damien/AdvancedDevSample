# Couche Domain - Entités et règles métier

## 📋 Vue d'ensemble

La couche Domain contient le cœur métier de l'application. Elle définit les entités, les règles de gestion et les invariants qui doivent toujours être respectés.

## 🎯 Principes

- **Aucune dépendance externe** : Le Domain ne dépend d'aucune autre couche
- **Règles métier encapsulées** : Toute la logique métier est dans les entités
- **Invariants protégés** : Les propriétés sont `private set` pour garantir la cohérence
- **Exceptions métier** : Utilisation de `DomainException` pour les violations de règles

## 📦 Entités

### Product - Produit du catalogue

**Responsabilité :** Gestion d'un produit du catalogue avec validation du prix.

**Propriétés :**
- `Id` : Identifiant unique
- `Name` : Nom du produit
- `Description` : Description détaillée
- `Price` : Prix unitaire (doit être > 0)
- `VATRate` : Taux de TVA (entre 0 et 100%)
- `IsActive` : Statut actif/inactif
- `SupplierId` : Lien vers le fournisseur (optionnel)

**Invariants métier :**
- Le prix doit toujours être strictement positif
- Le taux de TVA doit être entre 0 et 100%

**Méthodes métier :**
- `UpdatePrice(decimal newPrice)` : Modifie le prix avec validation
- `ApplyDiscount(decimal percentage)` : Applique une réduction
- `ChangeIsActive(bool newState)` : Active/désactive le produit
- `AssignSupplier(Guid supplierId)` : Associe un fournisseur
- `RemoveSupplier()` : Retire le fournisseur

---

### Order - Commande client

**Responsabilité :** Gestion du cycle de vie d'une commande avec machine à états.

**Propriétés :**
- `Id` : Identifiant unique
- `OrderNumber` : Numéro de commande généré
- `OrderDate` : Date de création
- `CustomerId` : Identifiant du client
- `TotalAmountExcludingTax` : Montant HT
- `TotalAmountIncludingTax` : Montant TTC
- `Status` : Statut de la commande
- `DeliveryAddress` : Adresse de livraison
- `Notes` : Notes complémentaires

**Statuts possibles :**
```csharp
public enum OrderStatus
{
    Pending,    // En attente
    Confirmed,  // Confirmée
    Shipped,    // Expédiée
    Delivered,  // Livrée
    Cancelled   // Annulée
}
```

**Machine à états - Transitions autorisées :**
```
Pending → Confirmed → Shipped → Delivered
    ↓
Cancelled (uniquement si non expédiée)
```

**Invariants métier :**
- Une commande doit avoir un client
- Une commande doit avoir une adresse de livraison
- Le montant TTC doit être ≥ montant HT
- Les transitions de statut doivent respecter le workflow

**Méthodes métier :**
- `Confirm()` : Pending → Confirmed (si montant > 0)
- `Ship()` : Confirmed → Shipped
- `Deliver()` : Shipped → Delivered
- `Cancel()` : Annulation (impossible si expédiée ou livrée)
- `UpdateTotals(decimal ht, decimal ttc)` : Mise à jour des montants
- `UpdateDeliveryAddress(string address)` : Modifie l'adresse (si non expédiée)

---

### User - Utilisateur du système

**Responsabilité :** Gestion d'un utilisateur avec authentification.

**Propriétés :**
- `Id` : Identifiant unique
- `Username` : Nom d'utilisateur unique
- `Email` : Adresse email
- `FirstName` : Prénom
- `LastName` : Nom de famille
- `Role` : Rôle (Student, Admin, User)
- `IsActive` : Compte actif/inactif
- `PasswordHash` : Hash BCrypt du mot de passe

**Propriété calculée :**
- `FullName` : Retourne `FirstName + " " + LastName`

**Invariants métier :**
- Le username est obligatoire
- L'email est obligatoire et doit contenir "@"
- Le prénom et le nom sont obligatoires
- Le rôle est obligatoire (par défaut "User")

**Méthodes métier :**
- `UpdateInfo(username, email, firstName, lastName)` : Mise à jour des informations
- `ChangeRole(string newRole)` : Modifie le rôle
- `SetActive(bool isActive)` : Active/désactive le compte
- `SetPassword(string passwordHash)` : Définit le hash du mot de passe

---

### Supplier - Fournisseur

**Responsabilité :** Gestion d'un fournisseur avec validation des coordonnées.

**Propriétés :**
- `Id` : Identifiant unique
- `Name` : Nom du fournisseur
- `Email` : Adresse email
- `PhoneNumber` : Numéro de téléphone
- `Address` : Adresse postale
- `IsActive` : Statut actif/inactif

**Invariants métier :**
- Le nom est obligatoire
- L'email est obligatoire et doit contenir "@"

**Méthodes métier :**
- `UpdateInfo(name, email, phone, address)` : Mise à jour complète
- `SetActive(bool isActive)` : Active/désactive le fournisseur

---

### RefreshToken - Token de rafraîchissement JWT

**Responsabilité :** Gestion des refresh tokens pour l'authentification JWT.

**Propriétés :**
- `Id` : Identifiant unique
- `UserId` : Identifiant de l'utilisateur
- `Token` : Token de rafraîchissement
- `ExpiresAt` : Date d'expiration
- `CreatedAt` : Date de création
- `RevokedAt` : Date de révocation (nullable)
- `RevokedReason` : Raison de la révocation

**Méthodes métier :**
- `IsValid()` : Vérifie si le token est valide (non expiré et non révoqué)
- `Revoke(string reason)` : Révoque le token

## 🔧 Interfaces des Repositories

Le Domain définit les contrats que l'Infrastructure doit implémenter.

### IProductRepository
```csharp
public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product? GetByID(Guid id);
    void Save(Product product);
    void Delete(Guid id);
}
```

### IOrderRepository
```csharp
public interface IOrderRepository
{
    IEnumerable<Order> GetAll();
    IEnumerable<Order> GetByCustomerId(Guid customerId);
    Order? GetByID(Guid id);
    void Save(Order order);
    void Delete(Guid id);
}
```

### IUserRepository
```csharp
public interface IUserRepository
{
    IEnumerable<User> GetAll();
    User? GetByID(Guid id);
    User? GetByUsername(string username);
    void Save(User user);
    void Delete(Guid id);
}
```

### ISupplierRepository
```csharp
public interface ISupplierRepository
{
    IEnumerable<Supplier> GetAll();
    Supplier? GetByID(Guid id);
    void Save(Supplier supplier);
    void Delete(Guid id);
}
```

### IAuthRepository
```csharp
public interface IAuthRepository
{
    User? GetUserByUsername(string username);
    void SaveRefreshToken(RefreshToken refreshToken);
    RefreshToken? GetRefreshToken(string token);
    void SeedUser(string username, string password, string role);
}
```

## ⚠️ Exceptions métier

### DomainException

Utilisée pour signaler une violation de règle métier.

```csharp
public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}
```

**Exemples d'utilisation :**
- Prix négatif ou nul
- Email invalide
- Transition de statut non autorisée
- Données obligatoires manquantes

## 🎯 Bonnes pratiques appliquées

### 1. Encapsulation
Les propriétés sont `private set` pour empêcher les modifications directes.

```csharp
public decimal Price { get; private set; }
```

### 2. Constructeurs validants
Tous les constructeurs valident les invariants.

```csharp
public Product(Guid id, decimal price)
{
    if (price <= 0)
        throw new DomainException("Le prix doit être strictement positif.");
    
    Id = id;
    Price = price;
}
```

### 3. Méthodes métier explicites
Les modifications passent par des méthodes métier nommées.

```csharp
public void UpdatePrice(decimal newPrice)
{
    if (newPrice <= 0)
        throw new DomainException("Le prix doit être strictement positif.");
    
    Price = newPrice;
}
```

### 4. Immutabilité des Value Objects
Les objets-valeurs sont immuables (création par constructeur uniquement).

## 📊 Diagramme de classes simplifié

```
┌─────────────┐
│   Product   │
├─────────────┤
│ - Id        │
│ - Price     │
│ - Name      │
└──────┬──────┘
       │
       │ 0..1
       ▼
┌─────────────┐
│  Supplier   │
├─────────────┤
│ - Id        │
│ - Name      │
│ - Email     │
└─────────────┘

┌─────────────┐      ┌─────────────┐
│    Order    │──────│    User     │
├─────────────┤ n  1 ├─────────────┤
│ - Id        │      │ - Id        │
│ - Status    │      │ - Username  │
│ - Total     │      │ - Role      │
└─────────────┘      └─────────────┘
```

## 🧪 Tests

Tous les invariants et règles métier sont couverts par des tests unitaires :

- `ProductTests.cs` : 20+ tests
- `OrderTests.cs` : 30+ tests  
- `UserTests.cs` : 15+ tests
- `SupplierTests.cs` : 15+ tests

**Exemple de test :**
```csharp
[Fact]
public void UpdatePrice_Should_Throw_Exception_When_Price_Is_Negative()
{
    var product = new Product(Guid.NewGuid(), 100, true);
    
    var exception = Assert.Throws<DomainException>(() 
        => product.UpdatePrice(-50));
    
    Assert.Equal("Le prix doit être strictement positif.", exception.Message);
}
```
