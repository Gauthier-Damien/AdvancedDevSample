using AdvancedDevSample.Domain.Entities;

namespace AdvancedDevSample.Domain.Interfaces.Suppliers
{
    /// <summary>
    /// Port (Interface) : Contrat de persistance des fournisseurs
    /// </summary>
    public interface ISupplierRepository
    {
        Supplier? GetByID(Guid id);
        IEnumerable<Supplier> GetAll();
        void Save(Supplier supplier);
    }
}
