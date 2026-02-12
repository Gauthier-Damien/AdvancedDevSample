# Couche Infrastructure - Persistance et Repositories

## 📋 Vue d'ensemble

La couche Infrastructure implémente la persistance des données et les interfaces définies par le Domain. Elle utilise le pattern Repository pour abstraire l'accès aux données.

## 🎯 Responsabilités

- **Implémentation des repositories** : Concrétisation des interfaces du Domain
- **Persistance des données** : Stockage et récupération
- **Gestion des collections** : InMemory pour démo/tests
- **Hachage des mots de passe** : BCrypt pour sécurité

## 🗄️ Stratégie de persistance

### InMemory Storage

Pour ce projet de démonstration, les données sont stockées en mémoire (InMemory).

**Avantages :**
- Pas de dépendance à une base de données
- Facilite les tests et la démonstration
- Simplicité de configuration

**Données pré-chargées (Seed) :**
- Utilisateur **demo** / demo123 (rôle Student)
- Utilisateur **admin** / admin123 (rôle Admin)

### Migration vers Entity Framework

La structure permet facilement de migrer vers Entity Framework Core :

1. Remplacer `InMemoryRepository` par `EfRepository`
2. Ajouter un DbContext
3. Configurer la connexion dans appsettings.json

**Exemple de DbContext (prévu) :**
```csharp
public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
}
```

---

## 📦 Repositories

### Pattern Repository

**Principe :** Abstraction de la persistance des données.

**Interface (Domain) :**
```csharp
public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product? GetByID(Guid id);
    void Save(Product product);
    void Delete(Guid id);
}
```

**Implémentation (Infrastructure) :**
```csharp
public class EfProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();
    
    public IEnumerable<Product> GetAll() => _products;
    
    public Product? GetByID(Guid id) => _products.FirstOrDefault(p => p.Id == id);
    
    public void Save(Product product)
    {
        var existing = GetByID(product.Id);
        if (existing != null)
            _products.Remove(existing);
        _products.Add(product);
    }
    
    public void Delete(Guid id)
    {
        var product = GetByID(id);
        if (product != null)
            _products.Remove(product);
    }
}
```

---

### EfProductRepository

**Responsabilité :** Persistance des produits.

**Méthodes :**
- `GetAll()` : Récupère tous les produits
- `GetByID(Guid id)` : Récupère un produit par ID
- `Save(Product product)` : Crée ou met à jour un produit
- `Delete(Guid id)` : Supprime un produit (hard delete)

**Stockage :** Liste InMemory `List<Product>`

---

### EfOrderRepository

**Responsabilité :** Persistance des commandes.

**Méthodes spécifiques :**
- `GetByCustomerId(Guid customerId)` : Récupère les commandes d'un client
- `GetAll()` : Récupère toutes les commandes
- `GetByID(Guid id)` : Récupère une commande par ID
- `Save(Order order)` : Crée ou met à jour une commande
- `Delete(Guid id)` : Supprime une commande

**Stockage :** Liste InMemory `List<Order>`

**Particularité :** Filtrage par client avec `GetByCustomerId()`.

---

### EfUserRepository

**Responsabilité :** Persistance des utilisateurs.

**Méthodes spécifiques :**
- `GetByUsername(string username)` : Récupère un utilisateur par username
- `GetAll()` : Récupère tous les utilisateurs
- `GetByID(Guid id)` : Récupère un utilisateur par ID
- `Save(User user)` : Crée ou met à jour un utilisateur
- `Delete(Guid id)` : Supprime un utilisateur

**Stockage :** Liste InMemory `List<User>`

**Particularité :** Recherche par username pour l'authentification.

---

### EfSupplierRepository

**Responsabilité :** Persistance des fournisseurs.

**Méthodes :**
- `GetAll()` : Récupère tous les fournisseurs
- `GetByID(Guid id)` : Récupère un fournisseur par ID
- `Save(Supplier supplier)` : Crée ou met à jour un fournisseur
- `Delete(Guid id)` : Supprime un fournisseur

**Stockage :** Liste InMemory `List<Supplier>`

---

### AuthRepository

**Responsabilité :** Authentification et gestion des refresh tokens.

**Méthodes :**

#### `GetUserByUsername(string username) : User?`
Récupère un utilisateur pour authentification.

#### `SaveRefreshToken(RefreshToken token) : void`
Sauvegarde un refresh token (création ou révocation).

**Stockage :** Liste InMemory `List<RefreshToken>`

#### `GetRefreshToken(string token) : RefreshToken?`
Récupère un refresh token pour validation.

#### `SeedUser(string username, string password, string role) : void`
Crée un utilisateur de démonstration avec mot de passe haché.

**Processus :**
1. Vérifie si l'utilisateur existe déjà
2. Hash le mot de passe avec BCrypt
3. Crée l'utilisateur avec le hash
4. Sauvegarde dans le repository

**Exemple d'utilisation (Program.cs) :**
```csharp
authRepo.SeedUser("demo", "demo123", "Student");
authRepo.SeedUser("admin", "admin123", "Admin");
```

---

## 🔐 Sécurité

### Hachage des mots de passe avec BCrypt

**Librairie :** `BCrypt.Net-Next`

**Hachage :**
```csharp
var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
user.SetPassword(passwordHash);
```

**Vérification :**
```csharp
bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
```

**Avantages de BCrypt :**
- Algorithme sécurisé et éprouvé
- Résistant au brute force (slow hashing)
- Salt automatique inclus
- Cost factor configurable

**Configuration par défaut :** Cost factor = 11 (recommandé)

---

## 🔄 Enregistrement des dépendances

**Configuration dans Program.cs :**

```csharp
// Repositories Infrastructure
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IProductRepository, EfProductRepository>();
builder.Services.AddScoped<ISupplierRepository, EfSupplierRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();
builder.Services.AddScoped<IOrderRepository, EfOrderRepository>();
```

**Cycle de vie :** `Scoped` (une instance par requête HTTP)

---

## 📊 Modèles de persistance

### ProductEntity (exemple pour EF Core)

Actuellement non utilisé (InMemory utilise directement les entités Domain).

Pour une migration vers EF Core, on pourrait créer :

```csharp
public class ProductEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal VATRate { get; set; }
    public bool IsActive { get; set; }
    public Guid? SupplierId { get; set; }
    
    // Navigation properties
    public SupplierEntity? Supplier { get; set; }
}
```

**Mapping :** `ProductEntity` ↔ `Product` (Domain)

---

## 🎯 Bonnes pratiques appliquées

### 1. Inversion de dépendances
- Domain définit les interfaces
- Infrastructure implémente les interfaces
- Dépendance inversée : Domain ← Infrastructure

### 2. Séparation des préoccupations
- Logique métier dans Domain
- Logique de persistance dans Infrastructure

### 3. Testabilité
- Repositories facilement mockables
- Fake repositories pour tests (InMemory)

### 4. Flexibilité
- Changement de stratégie de persistance sans impact sur Domain/Application
- Migration InMemory → SQL Server facile

### 5. Sécurité
- Mots de passe toujours hachés avec BCrypt
- Jamais de stockage en clair

---

## 🧪 Fake Repositories pour tests

**Utilisation dans les tests unitaires :**

```csharp
public class FakeProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();

    public IEnumerable<Product> GetAll() => _products;
    
    public Product? GetByID(Guid id) => _products.FirstOrDefault(p => p.Id == id);
    
    public void Save(Product product)
    {
        var existing = GetByID(product.Id);
        if (existing != null)
            _products.Remove(existing);
        _products.Add(product);
    }
    
    public void Delete(Guid id)
    {
        var product = GetByID(id);
        if (product != null)
            _products.Remove(product);
    }
}
```

**Avantage :** Isolation complète des tests, pas de dépendance externe.

---

## 📈 Évolution future

### Migration vers SQL Server

**Étapes :**

1. **Installer le package EF Core**
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

2. **Créer un DbContext**
```csharp
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    // ...
}
```

3. **Configurer la connexion**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=AdvancedDevSample;Trusted_Connection=True;"
  }
}
```

4. **Enregistrer le DbContext**
```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

5. **Créer les migrations**
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Audit et historique

Ajouter des propriétés d'audit :
- `CreatedAt` : Date de création
- `CreatedBy` : Utilisateur créateur
- `ModifiedAt` : Date de dernière modification
- `ModifiedBy` : Utilisateur modificateur

### Soft Delete global

Implémenter un filtre global pour les suppressions logiques :
```csharp
modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);
```
