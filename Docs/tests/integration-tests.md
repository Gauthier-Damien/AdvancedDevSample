# Tests - Tests d'Intégration

## Vue d'ensemble

Les **tests d'intégration** vérifient le fonctionnement de plusieurs composants ensemble, notamment l'API.

## WebApplicationFactory

Utilisation de `WebApplicationFactory<Program>` pour tester l'API en mode intégration.

```xml
<PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="9.0.0" />
```

## Configuration

```csharp
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remplacer les repositories par des implémentations de test
            // si nécessaire
        });
    }
}
```

## Tests des Controllers

```csharp
public class ProductControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    
    public ProductControllerIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task GetAll_ShouldReturn200AndProducts()
    {
        // Act
        var response = await _client.GetAsync("/api/products");
        
        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var content = await response.Content.ReadAsStringAsync();
        Assert.NotEmpty(content);
    }
    
    [Fact]
    public async Task Create_WithValidData_ShouldReturn201()
    {
        // Arrange
        var dto = new
        {
            name = "Test Product",
            description = "Test Description",
            price = 99.99m,
            vatRate = 20m,
            supplierId = Guid.NewGuid()
        };
        
        var content = new StringContent(
            JsonSerializer.Serialize(dto),
            Encoding.UTF8,
            "application/json"
        );
        
        // Act
        var response = await _client.PostAsync("/api/products", content);
        
        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
    
    [Fact]
    public async Task Create_WithInvalidPrice_ShouldReturn400()
    {
        // Arrange
        var dto = new
        {
            name = "Test Product",
            description = "Test Description",
            price = -50m,  // Prix invalide
            vatRate = 20m,
            supplierId = Guid.NewGuid()
        };
        
        var content = new StringContent(
            JsonSerializer.Serialize(dto),
            Encoding.UTF8,
            "application/json"
        );
        
        // Act
        var response = await _client.PostAsync("/api/products", content);
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
```

## Tests du Middleware

```csharp
[Fact]
public async Task ExceptionHandlingMiddleware_ShouldReturn500OnError()
{
    // Arrange
    var invalidId = Guid.NewGuid();
    
    // Act
    var response = await _client.GetAsync($"/api/products/{invalidId}");
    
    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    
    var content = await response.Content.ReadAsStringAsync();
    Assert.Contains("introuvable", content.ToLower());
}
```

## Tests du Rate Limiting

```csharp
[Fact]
public async Task RateLimiting_ShouldReturn429AfterTooManyRequests()
{
    // Arrange
    var tasks = new List<Task<HttpResponseMessage>>();
    
    // Act - Envoyer 150 requêtes rapidement
    for (int i = 0; i < 150; i++)
    {
        tasks.Add(_client.GetAsync("/api/products"));
    }
    
    var responses = await Task.WhenAll(tasks);
    
    // Assert - Au moins une devrait être 429 (limite = 100/min)
    Assert.Contains(responses, r => r.StatusCode == HttpStatusCode.TooManyRequests);
}
```

## Exécution

```bash
dotnet test --filter "Category=Integration"
```

## Best Practices

### ✅ À faire

- Tester les endpoints complets
- Vérifier les codes de statut HTTP
- Tester la validation
- Tester les middlewares

### ❌ À éviter

- Tests lents (bases de données réelles)
- Tests qui modifient un état partagé

## Navigation

- [Retour aux Tests →](../index.md#tests)
- [Tests unitaires →](unit-tests.md)
