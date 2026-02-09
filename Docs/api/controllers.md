# API - Controllers

## Vue d'ensemble

Les **Controllers** sont responsables de la réception des requêtes HTTP et de la coordination avec les services applicatifs.

## Controllers disponibles

### ProductController

Gestion du catalogue produits (CRUD complet).

**Route** : `/api/products`

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;
    
    /// <summary>
    /// Récupère tous les produits du catalogue
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }
    
    /// <summary>
    /// Récupère un produit par son identifiant
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);
        return product != null ? Ok(product) : NotFound();
    }
    
    /// <summary>
    /// Crée un nouveau produit
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(CreateProductDto dto)
    {
        var product = await _productService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }
    
    /// <summary>
    /// Met à jour un produit existant
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> Update(Guid id, UpdateProductDto dto)
    {
        var product = await _productService.UpdateAsync(id, dto);
        return Ok(product);
    }
    
    /// <summary>
    /// Supprime un produit
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _productService.DeleteAsync(id);
        return NoContent();
    }
}
```

### SupplierController

Gestion des fournisseurs.

**Route** : `/api/suppliers`

**Opérations** :
- `GET /api/suppliers` - Liste tous les fournisseurs
- `GET /api/suppliers/{id}` - Récupère un fournisseur
- `POST /api/suppliers` - Crée un fournisseur
- `PUT /api/suppliers/{id}` - Met à jour un fournisseur
- `DELETE /api/suppliers/{id}` - Supprime un fournisseur

### UserController

Gestion des utilisateurs.

**Route** : `/api/users`

**Opérations** : CRUD complet

### OrderController

Gestion des commandes.

**Route** : `/api/orders`

**Opérations** : CRUD complet avec gestion des lignes de commande

## Pattern utilisé

### Dependency Injection

Tous les controllers reçoivent leurs dépendances via le constructeur :

```csharp
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;
    
    public ProductController(ProductService productService)
    {
        _productService = productService;
    }
}
```

### ActionResult Pattern

Les méthodes retournent des `ActionResult<T>` pour typage fort et codes HTTP appropriés :

```csharp
public async Task<ActionResult<ProductDto>> GetById(Guid id)
{
    // Retourne 200 OK ou 404 Not Found
    var product = await _productService.GetByIdAsync(id);
    return product != null ? Ok(product) : NotFound();
}
```

## Attributs de routage

### [ApiController]

Active la validation automatique du ModelState et autres fonctionnalités :

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    // Validation automatique
    // Binding automatique des sources
    // Inférence automatique des types de réponse
}
```

### [HttpGet], [HttpPost], etc.

Définissent les verbes HTTP et routes :

```csharp
[HttpGet]                    // GET /api/products
[HttpGet("{id}")]           // GET /api/products/{id}
[HttpPost]                  // POST /api/products
[HttpPut("{id}")]          // PUT /api/products/{id}
[HttpDelete("{id}")]       // DELETE /api/products/{id}
```

## Validation

### Validation automatique

ASP.NET Core valide automatiquement le ModelState :

```csharp
[HttpPost]
public async Task<ActionResult<ProductDto>> Create(CreateProductDto dto)
{
    // Si ModelState invalide, retourne automatiquement 400 Bad Request
    // grâce à [ApiController]
    
    var product = await _productService.CreateAsync(dto);
    return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
}
```

### DTOs avec attributs de validation

```csharp
public record CreateProductDto(
    [Required] string Name,
    [StringLength(500)] string Description,
    [Range(0.01, double.MaxValue)] decimal Price,
    [Range(0, 100)] decimal VatRate,
    Guid SupplierId
);
```

## Gestion des erreurs

Les controllers **ne gèrent pas les exceptions** directement. Le middleware `ExceptionHandlingMiddleware` s'en charge :

```csharp
// ❌ À éviter
[HttpGet("{id}")]
public async Task<ActionResult<ProductDto>> GetById(Guid id)
{
    try
    {
        var product = await _productService.GetByIdAsync(id);
        return Ok(product);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}

// ✅ À faire
[HttpGet("{id}")]
public async Task<ActionResult<ProductDto>> GetById(Guid id)
{
    // Le middleware gère les exceptions
    var product = await _productService.GetByIdAsync(id);
    return product != null ? Ok(product) : NotFound();
}
```

## Codes de statut HTTP

| Action | Succès | Erreur |
|--------|--------|--------|
| GetAll | 200 OK | 500 Internal Server Error |
| GetById | 200 OK | 404 Not Found |
| Create | 201 Created | 400 Bad Request |
| Update | 200 OK | 404 Not Found / 400 Bad Request |
| Delete | 204 No Content | 404 Not Found |

## Commentaires XML

Tous les controllers et méthodes sont documentés avec des commentaires XML pour Swagger :

```csharp
/// <summary>
/// Récupère tous les produits du catalogue.
/// Retourne une liste vide si aucun produit n'existe.
/// </summary>
/// <returns>Liste de tous les produits sous forme de DTOs</returns>
/// <response code="200">Succès - Liste des produits retournée</response>
[HttpGet]
[ProducesResponseType(StatusCodes.Status200OK)]
public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
{
    // ...
}
```

## Navigation

- [Retour à l'API →](introduction.md)
- [Endpoints détaillés →](endpoints.md)
- [Middlewares →](middlewares.md)
