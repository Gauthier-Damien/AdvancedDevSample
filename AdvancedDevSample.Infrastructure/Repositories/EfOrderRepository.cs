using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Interfaces.Orders;

namespace AdvancedDevSample.Infrastructure.Repositories
{
    /// <summary>
    /// Adapter : implémente IOrderRepository avec stockage InMemory.
    /// </summary>
    public class EfOrderRepository : IOrderRepository
    {
        private static readonly Dictionary<Guid, Order> _orders = new();

        public Order? GetByID(Guid id)
        {
            _orders.TryGetValue(id, out var order);
            return order;
        }

        public IEnumerable<Order> GetAll()
        {
            return _orders.Values.ToList();
        }

        /// <summary>
        /// Requête spécifique : filtre les commandes par client.
        /// </summary>
        public IEnumerable<Order> GetByCustomerId(Guid customerId)
        {
            return _orders.Values.Where(o => o.CustomerId == customerId).ToList();
        }

        public void Save(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _orders[order.Id] = order;
        }
    }
}