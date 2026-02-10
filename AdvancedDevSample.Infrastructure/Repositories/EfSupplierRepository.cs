using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Interfaces.Suppliers;
using System.Collections.Concurrent;

namespace AdvancedDevSample.Infrastructure.Repositories
{
    /// <summary>
    /// Adapter : implémente ISupplierRepository avec stockage InMemory.
    /// </summary>
    public class EfSupplierRepository : ISupplierRepository
    {
        private static readonly ConcurrentDictionary<Guid, Supplier> _suppliers = new();

        public Supplier? GetByID(Guid id)
        {
            _suppliers.TryGetValue(id, out var supplier);
            return supplier;
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _suppliers.Values.ToList();
        }

        public void Save(Supplier supplier)
        {
            if (supplier == null)
                throw new ArgumentNullException(nameof(supplier));

            _suppliers[supplier.Id] = supplier;
        }
    }
}
