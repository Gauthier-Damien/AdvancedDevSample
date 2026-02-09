using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Exceptions;

namespace AdvancedDevSample.Test.Domaine.Entities;

/// <summary>
/// Tests unitaires pour l'entité Supplier
/// </summary>
public class SupplierTests
{
    [Fact]
    public void Constructor_Should_Create_Supplier_With_Valid_Data()
    {

        var id = Guid.NewGuid();
        var name = "Tech Supplier Inc.";
        var email = "contact@techsupplier.com";
        var phone = "+33123456789";
        var address = "123 Tech Street, Paris";
        

        var supplier = new Supplier(id, name, email, phone, address);
        

        Assert.Equal(id, supplier.Id);
        Assert.Equal(name, supplier.Name);
        Assert.Equal(email, supplier.Email);
        Assert.Equal(phone, supplier.PhoneNumber);
        Assert.Equal(address, supplier.Address);
        Assert.True(supplier.IsActive);
    }

    [Fact]
    public void Constructor_Should_Throw_Exception_When_Name_Is_Empty()
    {

        var exception = Assert.Throws<DomainException>(() => 
            new Supplier(Guid.NewGuid(), "", "email@test.com", "123", "Address"));
        
        Assert.Equal("Le nom du fournisseur est obligatoire.", exception.Message);
    }

    [Fact]
    public void Constructor_Should_Throw_Exception_When_Name_Is_Null()
    {

        #pragma warning disable CS8625
        var exception = Assert.Throws<DomainException>(() => 
            new Supplier(Guid.NewGuid(), null!, "email@test.com", "123", "Address"));
        #pragma warning restore CS8625
        
        Assert.Equal("Le nom du fournisseur est obligatoire.", exception.Message);
    }

    [Fact]
    public void Constructor_Should_Throw_Exception_When_Email_Is_Empty()
    {

        var exception = Assert.Throws<DomainException>(() => 
            new Supplier(Guid.NewGuid(), "Name", "", "123", "Address"));
        
        Assert.Equal("L'email du fournisseur est obligatoire.", exception.Message);
    }

    [Fact]
    public void Constructor_Should_Throw_Exception_When_Email_Is_Invalid()
    {

        var exception = Assert.Throws<DomainException>(() => 
            new Supplier(Guid.NewGuid(), "Name", "invalid-email", "123", "Address"));
        
        Assert.Equal("L'email du fournisseur n'est pas valide.", exception.Message);
    }

    [Fact]
    public void Constructor_Should_Accept_Valid_Email()
    {

        var supplier = new Supplier(Guid.NewGuid(), "Name", "valid@email.com", "123", "Address");
        

        Assert.Equal("valid@email.com", supplier.Email);
    }

    [Fact]
    public void UpdateInfo_Should_Update_All_Properties()
    {

        var supplier = new Supplier(Guid.NewGuid(), "Old Name", "old@email.com", "111", "Old Address");
        

        supplier.UpdateInfo("New Name", "new@email.com", "222", "New Address");
        

        Assert.Equal("New Name", supplier.Name);
        Assert.Equal("new@email.com", supplier.Email);
        Assert.Equal("222", supplier.PhoneNumber);
        Assert.Equal("New Address", supplier.Address);
    }

    [Fact]
    public void UpdateInfo_Should_Throw_Exception_When_Name_Is_Invalid()
    {

        var supplier = new Supplier(Guid.NewGuid(), "Valid Name", "valid@email.com", "123", "Address");
        

        var exception = Assert.Throws<DomainException>(() => 
            supplier.UpdateInfo("", "valid@email.com", "123", "Address"));
        
        Assert.Equal("Le nom du fournisseur est obligatoire.", exception.Message);
    }

    [Fact]
    public void UpdateInfo_Should_Throw_Exception_When_Email_Is_Invalid()
    {

        var supplier = new Supplier(Guid.NewGuid(), "Valid Name", "valid@email.com", "123", "Address");
        

        var exception = Assert.Throws<DomainException>(() => 
            supplier.UpdateInfo("Valid Name", "invalid-email", "123", "Address"));
        
        Assert.Equal("L'email du fournisseur n'est pas valide.", exception.Message);
    }

    [Fact]
    public void SetActive_Should_Change_Status_To_False()
    {

        var supplier = new Supplier(Guid.NewGuid(), "Name", "email@test.com", "123", "Address");
        Assert.True(supplier.IsActive);
        

        supplier.SetActive(false);
        

        Assert.False(supplier.IsActive);
    }

    [Fact]
    public void SetActive_Should_Change_Status_To_True()
    {

        var supplier = new Supplier(Guid.NewGuid(), "Name", "email@test.com", "123", "Address");
        supplier.SetActive(false);
        Assert.False(supplier.IsActive);
        

        supplier.SetActive(true);
        

        Assert.True(supplier.IsActive);
    }

    [Fact]
    public void DefaultConstructor_Should_Create_Supplier_With_Default_Values()
    {

        var supplier = new Supplier();
        

        Assert.NotEqual(Guid.Empty, supplier.Id);
        Assert.Equal(string.Empty, supplier.Name);
        Assert.Equal(string.Empty, supplier.Email);
        Assert.Equal(string.Empty, supplier.PhoneNumber);
        Assert.Equal(string.Empty, supplier.Address);
        Assert.True(supplier.IsActive);
    }

    [Fact]
    public void Constructor_Should_Handle_Null_PhoneNumber()
    {

        #pragma warning disable CS8625
        var supplier = new Supplier(Guid.NewGuid(), "Name", "email@test.com", null!, "Address");
        #pragma warning restore CS8625
        

        Assert.Equal(string.Empty, supplier.PhoneNumber);
    }

    [Fact]
    public void Constructor_Should_Handle_Null_Address()
    {

        #pragma warning disable CS8625
        var supplier = new Supplier(Guid.NewGuid(), "Name", "email@test.com", "123", null!);
        #pragma warning restore CS8625
        

        Assert.Equal(string.Empty, supplier.Address);
    }

    [Fact]
    public void UpdateInfo_Should_Handle_Null_PhoneNumber_And_Address()
    {

        var supplier = new Supplier(Guid.NewGuid(), "Name", "email@test.com", "123", "Address");
        

        #pragma warning disable CS8625
        supplier.UpdateInfo("New Name", "new@email.com", null!, null!);
        #pragma warning restore CS8625
        

        Assert.Equal(string.Empty, supplier.PhoneNumber);
        Assert.Equal(string.Empty, supplier.Address);
    }
}
