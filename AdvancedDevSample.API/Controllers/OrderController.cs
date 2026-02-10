using AdvancedDevSample.Application.DTOs.Orders;
using AdvancedDevSample.Application.Services;
using Microsoft.AspNetCore.Mvc;
namespace AdvancedDevSample.API.Controllers
{
    /// <summary>
    /// Contrôleur pour gérer les opérations CRUD sur les commandes.
    /// Gère le cycle de vie complet d'une commande : création, confirmation, expédition, livraison et annulation.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        /// <summary>
        /// Initialise une nouvelle instance du contrôleur de commandes.
        /// </summary>
        /// <param name="orderService">Service de gestion des commandes injecté par DI</param>
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Récupère toutes les commandes disponibles.
        /// </summary>
        /// <returns>Liste de toutes les commandes.</returns>
        /// <response code="200">Liste des commandes récupérée avec succès</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
        public IActionResult GetAllOrders()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }

        /// <summary>
        /// Récupère une commande spécifique par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de la commande.</param>
        /// <returns>La commande correspondante.</returns>
        /// <response code="200">Commande trouvée</response>
        /// <response code="404">Commande non trouvée</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetOrderById(Guid id)
        {
            var order = _orderService.GetOrderById(id);
            return Ok(order);
        }

        /// <summary>
        /// Récupère les commandes d'un client spécifique.
        /// </summary>
        /// <param name="customerId">Identifiant du client.</param>
        /// <returns>Liste des commandes du client.</returns>
        /// <response code="200">Liste des commandes récupérée avec succès</response>
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
        [HttpGet("customer/{customerId}")]
        public IActionResult GetOrdersByCustomer(Guid customerId)
        {
            var orders = _orderService.GetOrdersByCustomer(customerId);
            return Ok(orders);
        }

        /// <summary>
        /// Crée une nouvelle commande.
        /// </summary>
        /// <param name="request">Données de la commande à créer.</param>
        /// <returns>La commande créée.</returns>
        /// <response code="201">Commande créée avec succès</response>
        /// <response code="400">Données invalides</response>
        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
        {
            var order = _orderService.CreateOrder(request);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        /// <summary>
        /// Met à jour les totaux d'une commande existante.
        /// </summary>
        /// <param name="id">Identifiant de la commande à mettre à jour.</param>
        /// <param name="request">Nouveaux totaux de la commande.</param>
        /// <returns>La commande mise à jour.</returns>
        /// <response code="200">Commande mise à jour avec succès</response>
        /// <response code="404">Commande non trouvée</response>
        /// <response code="400">Données invalides</response>
        [HttpPut("{id}/totals")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateOrderTotals(Guid id, [FromBody] UpdateOrderTotalsRequest request)
        {
            var order = _orderService.UpdateOrderTotals(id, request);
            return Ok(order);
        }

        /// <summary>
        /// Confirme une commande.
        /// </summary>
        /// <param name="id">Identifiant de la commande à confirmer.</param>
        /// <returns>La commande confirmée.</returns>
        /// <response code="200">Commande confirmée avec succès</response>
        /// <response code="404">Commande non trouvée</response>
        /// <response code="400">Transition d'état invalide</response>
        /// <remarks>
        /// Transition d'état : Pending - Confirmed.
        /// Une commande doit être en état Pending pour être confirmée.
        /// </remarks>
        [HttpPost("{id}/confirm")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ConfirmOrder(Guid id)
        {
            var order = _orderService.ConfirmOrder(id);
            return Ok(order);
        }

        /// <summary>
        /// Expédie une commande.
        /// </summary>
        /// <param name="id">Identifiant de la commande à expédier.</param>
        /// <returns>La commande expédiée.</returns>
        /// <response code="200">Commande expédiée avec succès</response>
        /// <response code="404">Commande non trouvée</response>
        /// <response code="400">Transition d'état invalide</response>
        /// <remarks>
        /// Transition d'état : Confirmed - Shipped.
        /// Une commande doit être confirmée avant d'être expédiée.
        /// </remarks>
        [HttpPost("{id}/ship")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ShipOrder(Guid id)
        {
            var order = _orderService.ShipOrder(id);
            return Ok(order);
        }

        /// <summary>
        /// Marque une commande comme livrée.
        /// </summary>
        /// <param name="id">Identifiant de la commande à marquer comme livrée.</param>
        /// <returns>La commande marquée comme livrée.</returns>
        /// <response code="200">Commande marquée comme livrée avec succès</response>
        /// <response code="404">Commande non trouvée</response>
        /// <response code="400">Transition d'état invalide</response>
        /// <remarks>
        /// Transition d'état : Shipped - Delivered.
        /// Une commande doit être expédiée avant d'être marquée comme livrée.
        /// </remarks>
        [HttpPost("{id}/deliver")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeliverOrder(Guid id)
        {
            var order = _orderService.DeliverOrder(id);
            return Ok(order);
        }

        /// <summary>
        /// Annule une commande.
        /// </summary>
        /// <param name="id">Identifiant de la commande à annuler.</param>
        /// <returns>La commande annulée.</returns>
        /// <response code="200">Commande annulée avec succès</response>
        /// <response code="404">Commande non trouvée</response>
        /// <response code="400">Transition d'état invalide</response>
        /// <remarks>
        /// Transition d'état : Pending/Confirmed - Cancelled.
        /// Une commande ne peut être annulée que si elle n'est pas encore expédiée.
        /// </remarks>
        [HttpPost("{id}/cancel")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CancelOrder(Guid id)
        {
            var order = _orderService.CancelOrder(id);
            return Ok(order);
        }
    }
}