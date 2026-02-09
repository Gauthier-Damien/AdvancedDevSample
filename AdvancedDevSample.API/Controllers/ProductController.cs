using AdvancedDevSample.Application.DTOs.Products;
using AdvancedDevSample.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedDevSample.API.Controllers
{
    /// <summary>
    /// Contrôleur pour la gestion du catalogue produit
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        /// <summary>
        /// Initialise une nouvelle instance du contrôleur de produits.
        /// </summary>
        /// <param name="productService">Service de gestion des produits injecté par DI</param>
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Récupère la liste de tous les produits actifs
        /// </summary>
        /// <returns>Liste des produits</returns>
        /// <response code="200">Liste des produits récupérée avec succès</response>
        /// <remarks>Ne retourne que les produits dont le statut IsActive est true</remarks>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        /// <summary>
        /// Récupère les détails d'un produit par son ID
        /// </summary>
        /// <param name="id">Identifiant unique du produit</param>
        /// <returns>Détails du produit</returns>
        /// <response code="200">Produit trouvé</response>
        /// <response code="404">Produit non trouvé</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProductById(Guid id)
        {
            var product = _productService.GetProductById(id);
            return Ok(product);
        }

        /// <summary>
        /// Crée un nouveau produit dans le catalogue
        /// </summary>
        /// <param name="request">Données du produit à créer</param>
        /// <returns>Produit créé</returns>
        /// <response code="201">Produit créé avec succès</response>
        /// <response code="400">Données invalides (prix négatif ou nul, nom vide, etc.)</response>
        /// <remarks>
        /// Règles métier appliquées :
        /// - Le prix doit être strictement positif
        /// - Le produit doit avoir un fournisseur valide
        /// - La TVA doit être comprise entre 0 et 100%
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateProduct([FromBody] CreateProductRequest request)
        {
            var product = _productService.CreateProduct(request);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        /// <summary>
        /// Modifie le prix d'un produit
        /// </summary>
        /// <param name="id">Identifiant du produit</param>
        /// <param name="request">Nouveau prix</param>
        /// <returns>Produit avec prix mis à jour</returns>
        /// <response code="200">Prix modifié avec succès</response>
        /// <response code="400">Prix invalide (négatif ou nul) ou produit inactif</response>
        /// <response code="404">Produit non trouvé</response>
        /// <remarks>
        /// Le prix doit être strictement positif (invariant du domaine).
        /// Cette opération déclenche une validation de l'invariant du produit.
        /// </remarks>
        [HttpPut("{id}/price")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePrice(Guid id, [FromBody] ChangePriceRequest request)
        {
            var product = _productService.ChangeProductPrice(id, request.Price);
            return Ok(product);
        }

        /// <summary>
        /// Applique une promotion (réduction en pourcentage) sur un produit
        /// </summary>
        /// <param name="id">Identifiant du produit</param>
        /// <param name="request">Pourcentage de réduction (0.01 à 100)</param>
        /// <returns>Produit avec prix réduit</returns>
        /// <response code="200">Promotion appliquée avec succès</response>
        /// <response code="400">Pourcentage invalide (hors plage 0.01-100) ou prix résultant invalide</response>
        /// <response code="404">Produit non trouvé</response>
        /// <remarks>
        /// Le pourcentage de réduction est appliqué au prix actuel du produit.
        /// Le prix résultant doit rester strictement positif (invariant respecté).
        /// </remarks>
        [HttpPost("{id}/discount")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ApplyDiscount(Guid id, [FromBody] ApplyDiscountRequest request)
        {
            var product = _productService.ApplyDiscount(id, request.DiscountPercentage);
            return Ok(product);
        }

        /// <summary>
        /// Active ou désactive un produit
        /// </summary>
        /// <param name="id">Identifiant du produit</param>
        /// <param name="request">Nouveau statut</param>
        /// <returns>Produit avec statut mis à jour</returns>
        /// <response code="200">Statut modifié avec succès</response>
        /// <response code="404">Produit non trouvé</response>
        /// <remarks>
        /// Les produits désactivés n'apparaissent plus dans la liste des produits actifs
        /// mais restent en base de données (soft delete pattern).
        /// </remarks>
        [HttpPatch("{id}/status")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ToggleProductStatus(Guid id, [FromBody] ToggleProductStatusRequest request)
        {
            var product = _productService.ToggleProductStatus(id, request.IsActive);
            return Ok(product);
        }

        /// <summary>
        /// Supprime un produit (soft delete - le produit est désactivé)
        /// </summary>
        /// <param name="id">Identifiant du produit à supprimer</param>
        /// <response code="204">Produit supprimé avec succès (désactivé)</response>
        /// <response code="404">Produit non trouvé</response>
        /// <remarks>
        /// Cette opération effectue un "soft delete" : le produit n'est pas supprimé de la base de données,
        /// mais son statut IsActive est mis à false. Il n'apparaîtra plus dans la liste des produits actifs.
        /// </remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProduct(Guid id)
        {
            _productService.DeleteProduct(id);
            return NoContent(); // 204 No Content - suppression réussie
        }
    }
}
