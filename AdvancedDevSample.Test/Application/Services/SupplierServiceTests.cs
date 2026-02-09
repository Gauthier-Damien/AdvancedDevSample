using AdvancedDevSample.Application.DTOs.Suppliers;
using AdvancedDevSample.Application.Exceptions;
using AdvancedDevSample.Application.Services;
using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Interfaces.Suppliers;

namespace AdvancedDevSample.Test.Application.Services;

/// <summary>
/// Tests unitaires pour SupplierService
/// </summary>
public class SupplierServiceTests
{
    private readonly FakeSupplierRepository _fakeRepository;
    private readonly SupplierService _service;

    public SupplierServiceTests()
    {
        _fakeRepository = new FakeSupplierRepository();
        _service = new SupplierService(_fakeRepository);
    }

    [Fact]
    public void GetAllSuppliers_Should_Return_Only_Active_Suppliers()
    {
        // Arrange
        var activeSupplier = new Supplier(Guid.NewGuid(), "Active Supplier", "active@test.com", "123", "Address");
        var inactiveSupplier = new Supplier(Guid.NewGuid(), "Inactive Supplier", "inactive@test.com", "456", "Address");
        inactiveSupplier.SetActive(false);

        _fakeRepository.Add(activeSupplier);
        _fakeRepository.Add(inactiveSupplier);

        // Act
        var result = _service.GetAllSuppliers().ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal("Active Supplier", result[0].Name);
    }

    [Fact]
    public void GetSupplierById_Should_Return_Supplier_When_Exists()
    {
        // Arrange
        var supplier = new Supplier(Guid.NewGuid(), "Test Supplier", "test@supplier.com", "123", "Address");
        _fakeRepository.Add(supplier);

        // Act
        var result = _service.GetSupplierById(supplier.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(supplier.Id, result.Id);
        Assert.Equal("Test Supplier", result.Name);
    }

    [Fact]
    public void GetSupplierById_Should_Throw_Exception_When_Not_Found()
    {
        // Act & Assert
        var exception = Assert.Throws<ApplicationServiceException>((Action)(() =>
            _service.GetSupplierById(Guid.NewGuid())));

        Assert.Equal("Fournisseur non trouvé", exception.Message);
    }

    [Fact]
    public void CreateSupplier_Should_Create_And_Return_Supplier()
    {
        // Arrange
        var request = new CreateSupplierRequest
        {
            Name = "New Supplier",
            Email = "new@supplier.com",
            PhoneNumber = "123456789",
            Address = "123 Main Street"
        };

        // Act
        var result = _service.CreateSupplier(request);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal("New Supplier", result.Name);
        Assert.Equal("new@supplier.com", result.Email);
        Assert.True(result.IsActive);
    }

    [Fact]
    public void UpdateSupplier_Should_Update_All_Properties()
    {
        // Arrange
        var supplier = new Supplier(Guid.NewGuid(), "Old Name", "old@email.com", "111", "Old Address");
        _fakeRepository.Add(supplier);

        var request = new UpdateSupplierRequest
        {
            Name = "New Name",
            Email = "new@email.com",
            PhoneNumber = "222",
            Address = "New Address"
        };

        // Act
        var result = _service.UpdateSupplier(supplier.Id, request);

        // Assert
        Assert.Equal("New Name", result.Name);
        Assert.Equal("new@email.com", result.Email);
        Assert.Equal("222", result.PhoneNumber);
        Assert.Equal("New Address", result.Address);
    }

    [Fact]
    public void UpdateSupplier_Should_Throw_Exception_When_Not_Found()
    {
        // Arrange
        var request = new UpdateSupplierRequest
        {
            Name = "Name",
            Email = "email@test.com",
            PhoneNumber = "123",
            Address = "Address"
        };

        // Act & Assert
        var exception = Assert.Throws<ApplicationServiceException>((Action)(() =>
            _service.UpdateSupplier(Guid.NewGuid(), request)));

        Assert.Equal("Fournisseur non trouvé", exception.Message);
    }

    [Fact]
    public void DeleteSupplier_Should_Set_Supplier_Inactive()
    {
        // Arrange
        var supplier = new Supplier(Guid.NewGuid(), "Test Supplier", "test@supplier.com", "123", "Address");
        _fakeRepository.Add(supplier);

        // Act
        _service.DeleteSupplier(supplier.Id);

        // Assert
        var savedSupplier = _fakeRepository.GetByID(supplier.Id);
        Assert.NotNull(savedSupplier);
        Assert.False(savedSupplier.IsActive);
    }

    [Fact]
    public void DeleteSupplier_Should_Throw_Exception_When_Not_Found()
    {
        // Act & Assert
        var exception = Assert.Throws<ApplicationServiceException>((Action)(() =>
            _service.DeleteSupplier(Guid.NewGuid())));

        Assert.Equal("Fournisseur non trouvé", exception.Message);
    }
}

/// <summary>
/// Fake Repository pour les tests (InMemory)
/// </summary>
public class FakeSupplierRepository : ISupplierRepository
{
    private readonly Dictionary<Guid, Supplier> _suppliers = new();

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
        _suppliers[supplier.Id] = supplier;
    }

    public void Add(Supplier supplier)
    {
        _suppliers[supplier.Id] = supplier;
    }
}
