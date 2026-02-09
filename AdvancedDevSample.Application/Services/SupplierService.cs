using AdvancedDevSample.Application.DTOs.Suppliers;
using AdvancedDevSample.Application.Exceptions;
using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Interfaces.Suppliers;

namespace AdvancedDevSample.Application.Services
{
    /// <summary>
    /// Gestion des fournisseurs (CRUD).
    /// </summary>
    public class SupplierService
    {
        private readonly ISupplierRepository _repo;

        public SupplierService(ISupplierRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<SupplierResponse> GetAllSuppliers()
        {
            var suppliers = _repo.GetAll();
            return suppliers.Where(s => s.IsActive).Select(MapToResponse);
        }

        public SupplierResponse GetSupplierById(Guid id)
        {
            var supplier = GetSupplier(id);
            return MapToResponse(supplier);
        }

        public SupplierResponse CreateSupplier(CreateSupplierRequest request)
        {
            var supplier = new Supplier(Guid.NewGuid(), request.Name, request.Email, request.PhoneNumber, request.Address);
            _repo.Save(supplier);
            return MapToResponse(supplier);
        }

        public SupplierResponse UpdateSupplier(Guid id, UpdateSupplierRequest request)
        {
            var supplier = GetSupplier(id);
            supplier.UpdateInfo(request.Name, request.Email, request.PhoneNumber, request.Address);
            _repo.Save(supplier);
            return MapToResponse(supplier);
        }

        /// <summary>
        /// Soft delete : désactive le fournisseur.
        /// </summary>
        public void DeleteSupplier(Guid id)
        {
            var supplier = GetSupplier(id);
            supplier.SetActive(false);
            _repo.Save(supplier);
        }

        private Supplier GetSupplier(Guid id)
        {
            return _repo.GetByID(id) ?? throw new ApplicationServiceException("Fournisseur non trouvé", System.Net.HttpStatusCode.NotFound);
        }

        private SupplierResponse MapToResponse(Supplier supplier)
        {
            return new SupplierResponse
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Email = supplier.Email,
                PhoneNumber = supplier.PhoneNumber,
                Address = supplier.Address,
                IsActive = supplier.IsActive
            };
        }
    }
}