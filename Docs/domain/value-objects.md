# Domain - Value Objects

## Vue d'ensemble

Les **Value Objects** sont des objets **immuables** définis uniquement par leurs valeurs, sans identité propre.

## Caractéristiques

- ✅ **Immuables** : Ne peuvent pas être modifiés après création
- ✅ **Égalité par valeur** : Deux objets avec les mêmes valeurs sont égaux
- ✅ **Validation** : Garantissent toujours un état valide
- ✅ **Pas d'identité** : Pas de Guid ou Id

## Value Objects disponibles

### Price

Représente un prix avec validation.

```csharp
public class Price
{
    public decimal Value { get; }
    
    public Price(decimal value)
    {
        if (value <= 0)
            throw new InvalidPriceException("Le prix doit être strictement positif");
        
        Value = value;
    }
    
    // Égalité par valeur
    public override bool Equals(object? obj)
    {
        return obj is Price other && Value == other.Value;
    }
    
    public override int GetHashCode() => Value.GetHashCode();
    
    // Opérateurs
    public static bool operator ==(Price left, Price right) => left.Equals(right);
    public static bool operator !=(Price left, Price right) => !left.Equals(right);
}
```

**Invariant** : Le prix doit **toujours** être strictement positif.

### VAT (TVA)

Représente un taux de TVA.

```csharp
public class VAT
{
    public decimal Rate { get; }
    
    public VAT(decimal rate)
    {
        if (rate < 0 || rate > 100)
            throw new InvalidVatException("Le taux de TVA doit être entre 0 et 100");
        
        Rate = rate;
    }
    
    public decimal CalculateTax(Price price)
    {
        return price.Value * (Rate / 100);
    }
    
    public decimal CalculatePriceIncludingTax(Price price)
    {
        return price.Value * (1 + Rate / 100);
    }
}
```

**Invariant** : Le taux doit être entre 0 et 100%.

### Email (exemple possible)

```csharp
public class Email
{
    public string Value { get; }
    
    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("L'email ne peut pas être vide");
        
        if (!IsValidEmail(value))
            throw new ArgumentException("Format d'email invalide");
        
        Value = value.ToLowerInvariant();
    }
    
    private static bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
```

## Avantages des Value Objects

### 1. Encapsulation de la validation

```csharp
// ❌ Sans Value Object
public class Product
{
    public decimal Price { get; set; }
    
    public void UpdatePrice(decimal newPrice)
    {
        // Validation à répéter partout
        if (newPrice <= 0)
            throw new Exception("Prix invalide");
        Price = newPrice;
    }
}

// ✅ Avec Value Object
public class Product
{
    public Price Price { get; private set; }
    
    public void UpdatePrice(Price newPrice)
    {
        // La validation est dans Price
        Price = newPrice;  // Toujours valide !
    }
}
```

### 2. Sémantique métier

```csharp
// ❌ Perte de sémantique
public decimal CalculateTax(decimal price, decimal vatRate)
{
    return price * vatRate / 100;
}

// ✅ Sémantique claire
public decimal CalculateTax(Price price, VAT vat)
{
    return vat.CalculateTax(price);
}
```

### 3. Immuabilité

```csharp
var price1 = new Price(100);
var price2 = price1;  // Même référence

// Impossible de modifier price1 ou price2
// Pour changer, il faut créer un nouveau Price
var newPrice = new Price(150);
```

## Égalité par valeur

### Implémentation

```csharp
public class Price : IEquatable<Price>
{
    public decimal Value { get; }
    
    // Equals
    public bool Equals(Price? other)
    {
        if (other is null) return false;
        return Value == other.Value;
    }
    
    public override bool Equals(object? obj)
    {
        return Equals(obj as Price);
    }
    
    // GetHashCode
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    
    // Opérateurs
    public static bool operator ==(Price? left, Price? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }
    
    public static bool operator !=(Price? left, Price? right)
    {
        return !(left == right);
    }
}
```

### Utilisation

```csharp
var price1 = new Price(100);
var price2 = new Price(100);

Console.WriteLine(price1 == price2);  // True (égalité par valeur)
Console.WriteLine(price1.Equals(price2));  // True

var price3 = new Price(150);
Console.WriteLine(price1 == price3);  // False
```

## Records (C# 9+)

Alternative moderne pour les Value Objects simples :

```csharp
public record Price(decimal Value)
{
    public Price(decimal Value) : this(Value)
    {
        if (Value <= 0)
            throw new InvalidPriceException("Le prix doit être strictement positif");
    }
}

// Immuabilité et égalité par valeur automatiques
var p1 = new Price(100);
var p2 = new Price(100);
Console.WriteLine(p1 == p2);  // True
```

## Value Objects vs Primitives

| Primitive (decimal, string) | Value Object (Price, Email) |
|-----------------------------|------------------------------|
| Pas de validation | Validation garantie |
| Pas de sémantique | Sémantique métier claire |
| Facile à mal utiliser | Impossible d'avoir un état invalide |
| `decimal price` | `Price price` |

## Pattern utilisé : Primitive Obsession

**Problème** : Utiliser trop de types primitifs.

```csharp
// ❌ Primitive Obsession
public void CreateProduct(string name, decimal price, decimal vatRate)
{
    // Qu'est-ce qu'un decimal valide pour le prix ?
    // Et pour la TVA ?
}

// ✅ Value Objects
public void CreateProduct(string name, Price price, VAT vat)
{
    // Les types portent la sémantique
    // Les valeurs sont garanties valides
}
```

## Navigation

- [Retour au Domain →](../architecture/domain.md)
- [Entités →](entities.md)
- [Exceptions →](exceptions.md)
