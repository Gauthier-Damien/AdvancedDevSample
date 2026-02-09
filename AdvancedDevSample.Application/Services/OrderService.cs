using AdvancedDevSample.Application.DTOs.Orders;
using AdvancedDevSample.Application.Exceptions;
using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Interfaces.Orders;

namespace AdvancedDevSample.Application.Services
{
    /// <summary>
    /// Gestion du cycle de vie des commandes.
    /// Orchestre les transitions d'état : Pending → Confirmed → Shipped → Delivered.
    /// </summary>
    public class OrderService
    {
        private readonly IOrderRepository _repo;

        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<OrderResponse> GetAllOrders()
        {
            var orders = _repo.GetAll();
            return orders.Select(MapToResponse);
        }

        public IEnumerable<OrderResponse> GetOrdersByCustomer(Guid customerId)
        {
            var orders = _repo.GetByCustomerId(customerId);
            return orders.Select(MapToResponse);
        }

        public OrderResponse GetOrderById(Guid id)
        {
            var order = GetOrder(id);
            return MapToResponse(order);
        }

        public OrderResponse CreateOrder(CreateOrderRequest request)
        {
            var order = new Order(request.CustomerId, request.DeliveryAddress, request.Notes);
            _repo.Save(order);
            return MapToResponse(order);
        }

        public OrderResponse UpdateOrderTotals(Guid id, UpdateOrderTotalsRequest request)
        {
            var order = GetOrder(id);
            order.UpdateTotals(request.TotalAmountExcludingTax, request.TotalAmountIncludingTax);
            _repo.Save(order);
            return MapToResponse(order);
        }

        /// <summary>
        /// Transition : Pending → Confirmed.
        /// </summary>
        public OrderResponse ConfirmOrder(Guid id)
        {
            var order = GetOrder(id);
            order.Confirm();
            _repo.Save(order);
            return MapToResponse(order);
        }

        /// <summary>
        /// Transition : Confirmed → Shipped.
        /// </summary>
        public OrderResponse ShipOrder(Guid id)
        {
            var order = GetOrder(id);
            order.Ship();
            _repo.Save(order);
            return MapToResponse(order);
        }

        /// <summary>
        /// Transition : Shipped → Delivered.
        /// </summary>
        public OrderResponse DeliverOrder(Guid id)
        {
            var order = GetOrder(id);
            order.Deliver();
            _repo.Save(order);
            return MapToResponse(order);
        }

        /// <summary>
        /// Annulation possible uniquement si non expédiée.
        /// </summary>
        public OrderResponse CancelOrder(Guid id)
        {
            var order = GetOrder(id);
            order.Cancel();
            _repo.Save(order);
            return MapToResponse(order);
        }

        private Order GetOrder(Guid id)
        {
            return _repo.GetByID(id) ?? throw new ApplicationServiceException("Commande non trouvée", System.Net.HttpStatusCode.NotFound);
        }

        private OrderResponse MapToResponse(Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                OrderDate = order.OrderDate,
                CustomerId = order.CustomerId,
                TotalAmountExcludingTax = order.TotalAmountExcludingTax,
                TotalAmountIncludingTax = order.TotalAmountIncludingTax,
                Status = order.Status,
                StatusLabel = order.Status.ToString(),
                DeliveryAddress = order.DeliveryAddress,
                Notes = order.Notes
            };
        }
    }
}