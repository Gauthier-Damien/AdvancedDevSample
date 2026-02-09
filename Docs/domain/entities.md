# Domain - Entités

## Vue d'ensemble

Les **entités** sont des objets métier avec une **identité unique** qui persiste dans le temps.

## Caractéristiques

- ✅ **Identité unique** : Guid comme identifiant
- ✅ **Logique métier** : Comportements et règles
- ✅ **Invariants** : États toujours valides
- ✅ **Cycle de vie** : Peuvent être modifiées

## Entités disponibles

### Product

Représente un produit du catalogue.

```csharp
public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Price Price { get; private set; }
    public VAT Vat { get; private set; }
    public bool IsActive { get; private set; }
    public Guid SupplierId { get; private set; }
    
    public Product(string name, string description, Price price, VAT vat, Guid supplierId)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? string.Empty;
        Price = price ?? throw new ArgumentNullException(nameof(price));
        Vat = vat ?? throw new ArgumentNullException(nameof(vat));
        SupplierId = supplierId;
        IsActive = true;
    }
    
    // Méthodes métier
    public void UpdatePrice(Price newPrice)
    {
        Price = newPrice ?? throw new ArgumentNullException(nameof(newPrice));
    }
    
    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;
}
```

**Invariants** :
- Le prix doit toujours être valide (>0)
- Le nom ne peut pas être null
- La TVA doit être valide (0-100%)

### Supplier

Représente un fournisseur.

```csharp
public class Supplier
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    
    public Supplier(string name, string email, string phone)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Phone = phone ?? string.Empty;
    }
}
```

### User

Représente un utilisateur.

```csharp
public class User
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    
    public User(string firstName, string lastName, string email)
    {
        Id = Guid.NewGuid();
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }
}
```

### Order

Représente une commande avec ses lignes.

```csharp
public class Order
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public List<OrderLine> OrderLines { get; private set; }
    
    public Order(Guid userId, DateTime orderDate)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        OrderDate = orderDate;
        OrderLines = new List<OrderLine>();
    }
    
    public void AddLine(OrderLine line)
    {
        OrderLines.Add(line ?? throw new ArgumentNullException(nameof(line)));
    }
    
    public decimal GetTotalAmount()
    {
        return OrderLines.Sum(line => line.Quantity * line.UnitPrice);
    }
}
```

### OrderLine

Représente une ligne de commande.

```csharp
public class OrderLine
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    
    public OrderLine(Guid productId, int quantity, decimal unitPrice)
    {
        if (quantity <= 0)
            throw new ArgumentException("La quantité doit être strictement positive");
        if (unitPrice <= 0)
            throw new ArgumentException("Le prix unitaire doit être strictement positif");
            
        Id = Guid.NewGuid();
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}
```

## Principes de conception

### Encapsulation

Les setters sont **privés** pour protéger l'intégrité :

```csharp
public Price Price { get; private set; }

// Modification via méthode métier
public void UpdatePrice(Price newPrice)
{
    // Validation + logique métier
    Price = newPrice ?? throw new ArgumentNullException(nameof(newPrice));
}
```

### Validation dans le constructeur

```csharp
public Product(string name, Price price)
{
    // Validation immédiate
    Name = name ?? throw new ArgumentNullException(nameof(name));
    Price = price ?? throw new ArgumentNullException(nameof(price));
    
    // État valide garanti
}
```

### Méthodes métier

Les entités contiennent leur propre logique :

```csharp
public void Activate() => IsActive = true;
public void Deactivate() => IsActive = false;

public decimal GetTotalAmount()
{
    return OrderLines.Sum(line => line.Quantity * line.UnitPrice);
}
```

## Entity vs Value Object

| Entity | Value Object |
|--------|--------------|
| Identité unique (Id) | Défini par ses valeurs |
| Mutable | Immuable |
| Cycle de vie | Pas de cycle de vie |
| `Product`, `Order` | `Price`, `VAT` |

## Navigation

- [Retour au Domain →](../architecture/domain.md)
- [Value Objects →](value-objects.md)
- [Interfaces →](interfaces.md)
