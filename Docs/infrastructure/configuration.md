# Infrastructure - Configuration

## Vue d'ensemble

Cette page documente la configuration future de l'infrastructure avec Entity Framework Core.

## État actuel

⚠️ **Actuellement** : Les repositories utilisent des **collections en mémoire** (`List<T>`).

➡️ **Future** : Migration vers Entity Framework Core avec une vraie base de données.

## Configuration Entity Framework Core (future)

### Connexion à la base de données

**appsettings.json** :

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AdvancedDevSampleDb;Trusted_Connection=True;"
  }
}
```

### DbContext

```csharp
public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureProduct(modelBuilder);
        ConfigureSupplier(modelBuilder);
        ConfigureUser(modelBuilder);
        ConfigureOrder(modelBuilder);
    }
    
    private static void ConfigureProduct(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            
            // Value Objects as Owned Entities
            entity.OwnsOne(p => p.Price, price =>
            {
                price.Property(p => p.Value).HasColumnName("Price");
            });
            
            entity.OwnsOne(p => p.Vat, vat =>
            {
                vat.Property(v => v.Rate).HasColumnName("VatRate");
            });
            
            // Relations
            entity.HasOne<Supplier>()
                .WithMany()
                .HasForeignKey(p => p.SupplierId);
        });
    }
    
    // Autres configurations...
}
```

### Enregistrement dans Program.cs

```csharp
// Configuration EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Repositories
builder.Services.AddScoped<IProductRepository, EfProductRepository>();
builder.Services.AddScoped<ISupplierRepository, EfSupplierRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();
builder.Services.AddScoped<IOrderRepository, EfOrderRepository>();
```

## Migrations

### Créer une migration

```bash
dotnet ef migrations add InitialCreate
```

### Appliquer la migration

```bash
dotnet ef database update
```

### Supprimer la dernière migration

```bash
dotnet ef migrations remove
```

## Seed Data (données initiales)

```csharp
public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();
        
        if (context.Products.Any())
            return;  // DB déjà initialisée
        
        var suppliers = new[]
        {
            new Supplier("Dell Inc.", "contact@dell.com", "+1234567890"),
            new Supplier("HP Inc.", "contact@hp.com", "+0987654321")
        };
        
        context.Suppliers.AddRange(suppliers);
        context.SaveChanges();
        
        var products = new[]
        {
            new Product("Laptop Dell XPS 15", "High performance", new Price(1299.99m), new VAT(20), suppliers[0].Id),
            new Product("HP EliteBook", "Business laptop", new Price(999.99m), new VAT(20), suppliers[1].Id)
        };
        
        context.Products.AddRange(products);
        context.SaveChanges();
    }
}

// Dans Program.cs
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.Initialize(context);
}
```

## Choix de base de données

### SQL Server

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

```csharp
options.UseSqlServer(connectionString)
```

### PostgreSQL

```bash
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

```csharp
options.UseNpgsql(connectionString)
```

### SQLite (pour développement)

```bash
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

```csharp
options.UseSqlite("Data Source=app.db")
```

## Configuration avancée

### Logging des requêtes SQL

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.EnableSensitiveDataLogging();  // Uniquement en développement
    options.LogTo(Console.WriteLine, LogLevel.Information);
});
```

### Stratégie de retry

```csharp
options.UseSqlServer(
    connectionString,
    sqlOptions => sqlOptions.EnableRetryOnFailure(
        maxRetryCount: 5,
        maxRetryDelay: TimeSpan.FromSeconds(30),
        errorNumbersToAdd: null
    )
);
```

## Navigation

- [Retour à Infrastructure →](../architecture/infrastructure.md)
- [Repositories →](repositories.md)
