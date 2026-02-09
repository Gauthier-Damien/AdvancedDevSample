using AdvancedDevSample.Domain.Entities;

namespace AdvancedDevSample.Domain.Interfaces.Orders
{
    /// <summary>
    /// Port (Interface) : Contrat de persistance des commandes
    /// </summary>
    public interface IOrderRepository
    {
        Order? GetByID(Guid id);
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetByCustomerId(Guid customerId);
        void Save(Order order);
    }
}
