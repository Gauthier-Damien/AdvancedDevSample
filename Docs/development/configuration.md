# Configuration de l'Application

## Fichiers de configuration

### appsettings.json

Configuration par défaut de l'application.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### appsettings.Development.json

Configuration spécifique au développement.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Information"
    }
  }
}
```

## Variables d'environnement

### ASPNETCORE_ENVIRONMENT

Définit l'environnement d'exécution :

- `Development` : Développement local
- `Staging` : Pré-production
- `Production` : Production

```bash
# Windows (PowerShell)
$env:ASPNETCORE_ENVIRONMENT="Development"

# Linux/Mac
export ASPNETCORE_ENVIRONMENT=Development
```

## Configuration des ports

### launchSettings.json

```json
{
  "profiles": {
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "https://localhost:5181;http://localhost:5180",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

## Configuration Swagger

Swagger est activé **uniquement en développement** :

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

## Logging

### Configuration des niveaux

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information",
      "AdvancedDevSample": "Debug"
    }
  }
}
```

### Utilisation dans le code

```csharp
public class ProductService
{
    private readonly ILogger<ProductService> _logger;
    
    public ProductService(ILogger<ProductService> logger)
    {
        _logger = logger;
    }
    
    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        _logger.LogInformation("Création du produit {Name}", dto.Name);
        // ...
    }
}
```

## Configuration future

### Connexion base de données

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=AdvancedDevSampleDb;Trusted_Connection=True;"
  }
}
```

### CORS

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

app.UseCors("AllowAll");
```

## Navigation

- [Retour au développement →](installation.md)
- [Bonnes pratiques →](best-practices.md)
