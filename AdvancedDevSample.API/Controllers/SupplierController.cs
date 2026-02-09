using AdvancedDevSample.Application.DTOs.Suppliers;
using AdvancedDevSample.Application.Services;
using Microsoft.AspNetCore.Mvc;
namespace AdvancedDevSample.API.Controllers
{
    /// <summary>
    /// Contrôleur pour gérer les opérations CRUD sur les fournisseurs.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly SupplierService _supplierService;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="SupplierController"/>.
        /// </summary>
        /// <param name="supplierService">Service pour gérer les fournisseurs.</param>
        public SupplierController(SupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        /// <summary>
        /// Récupère tous les fournisseurs.
        /// </summary>
        /// <returns>Liste de tous les fournisseurs.</returns>
        /// <response code="200">Liste récupérée avec succès</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllSuppliers()
        {
            var suppliers = _supplierService.GetAllSuppliers();
            return Ok(suppliers);
        }

        /// <summary>
        /// Récupère un fournisseur spécifique par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant du fournisseur.</param>
        /// <returns>Le fournisseur correspondant.</returns>
        /// <response code="200">Fournisseur trouvé</response>
        /// <response code="404">Fournisseur non trouvé</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSupplierById(Guid id)
        {
            var supplier = _supplierService.GetSupplierById(id);
            return Ok(supplier);
        }

        /// <summary>
        /// Crée un nouveau fournisseur.
        /// </summary>
        /// <param name="request">Données du fournisseur à créer.</param>
        /// <returns>Le fournisseur créé.</returns>
        /// <response code="201">Fournisseur créé avec succès</response>
        /// <response code="400">Données invalides</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateSupplier([FromBody] CreateSupplierRequest request)
        {
            var supplier = _supplierService.CreateSupplier(request);
            return CreatedAtAction(nameof(GetSupplierById), new { id = supplier.Id }, supplier);
        }

        /// <summary>
        /// Met à jour un fournisseur existant.
        /// </summary>
        /// <param name="id">Identifiant du fournisseur à mettre à jour.</param>
        /// <param name="request">Données mises à jour du fournisseur.</param>
        /// <returns>Le fournisseur mis à jour.</returns>
        /// <response code="200">Fournisseur mis à jour avec succès</response>
        /// <response code="404">Fournisseur non trouvé</response>
        /// <response code="400">Données invalides</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateSupplier(Guid id, [FromBody] UpdateSupplierRequest request)
        {
            var supplier = _supplierService.UpdateSupplier(id, request);
            return Ok(supplier);
        }

        /// <summary>
        /// Supprime un fournisseur par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant du fournisseur à supprimer.</param>
        /// <returns>Confirmation de la suppression.</returns>
        /// <response code="204">Fournisseur supprimé avec succès</response>
        /// <response code="404">Fournisseur non trouvé</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteSupplier(Guid id)
        {
            _supplierService.DeleteSupplier(id);
            return NoContent();
        }
    }
}