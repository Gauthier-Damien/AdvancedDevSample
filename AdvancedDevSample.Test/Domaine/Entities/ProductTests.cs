using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Exceptions;

namespace AdvancedDevSample.Test.Domaine.Entities;

public class ProductTests
{
    [Fact]
    public void ChangePrice_Should_Update_Price_When_Product_Is_Active()
    {
        //Arrange : prepare a valid product
        Product product = new Product();
        product.UpdatePrice(10); //initial value
        
        // execute action
        product.UpdatePrice(20);
        
        //verification
        Assert.Equal(20, product.Price);
    }

    [Fact]
    public void ChangePrice_Should_Throw_Exception_When_Product_Is_Not_Active()
    {
        Product product = new Product();
        product.UpdatePrice(10); //Initial value
        
        //Simulate Deactivated product using the proper method
        product.ChangeIsActive(false);
        

        var exception = Assert.Throws<DomainException>(() => product.UpdatePrice(20));
        
        Assert.Equal("Impossible de modifier un produit inactif.", exception.Message);
    }

    [Fact]
    public void ApplyDiscount_Should_Decrease_Price()
    {
        Product product = new Product();
        product.UpdatePrice(100); //Init value
        
        //Act - Applique 30% de réduction
        product.ApplyDiscount(30);
        
        //Assert - Prix devrait être 70 (100 - 30%)
        Assert.Equal(70, product.Price);
    }

    [Fact]
    public void UpdatePrice_Should_Throw_Exception_When_Price_Is_Zero_Or_Negative()
    {

        Product product = new Product();
        product.UpdatePrice(100);
        

        var exception = Assert.Throws<DomainException>(() => product.UpdatePrice(0));
        Assert.Equal("Le prix doit être strictement positif.", exception.Message);
        
        var exception2 = Assert.Throws<DomainException>(() => product.UpdatePrice(-10));
        Assert.Equal("Le prix doit être strictement positif.", exception2.Message);
    }

    [Fact]
    public void Constructor_Should_Throw_Exception_When_Price_Is_Invalid()
    {

        var exception = Assert.Throws<DomainException>(() => new Product(Guid.NewGuid(), 0, true));
        Assert.Equal("Le prix doit être strictement positif.", exception.Message);
    }

    [Fact]
    public void ApplyDiscount_Should_Throw_Exception_When_Percentage_Is_Invalid()
    {

        Product product = new Product();
        product.UpdatePrice(100);
        
        //Act & Assert - Pourcentage négatif
        var exception1 = Assert.Throws<DomainException>(() => product.ApplyDiscount(-10));
        Assert.Equal("Le pourcentage de réduction doit être strictement positif.", exception1.Message);
        
        //Act & Assert - Pourcentage > 100
        var exception2 = Assert.Throws<DomainException>(() => product.ApplyDiscount(150));
        Assert.Equal("Le pourcentage de réduction ne peut pas dépasser 100%.", exception2.Message);
    }

    [Fact]
    public void Constructor_Complete_Should_Create_Product_With_All_Properties()
    {

        var id = Guid.NewGuid();
        var supplierId = Guid.NewGuid();
        

        var product = new Product(id, "Laptop", "Gaming laptop", 999.99m, 20, true, supplierId);
        

        Assert.Equal(id, product.Id);
        Assert.Equal("Laptop", product.Name);
        Assert.Equal("Gaming laptop", product.Description);
        Assert.Equal(999.99m, product.Price);
        Assert.Equal(20, product.VATRate);
        Assert.True(product.IsActive);
        Assert.Equal(supplierId, product.SupplierId);
    }

    [Fact]
    public void Constructor_Should_Throw_Exception_When_VATRate_Is_Invalid()
    {
        //Act & Assert - TVA négative
        var exception1 = Assert.Throws<DomainException>(() => 
            new Product(Guid.NewGuid(), "Product", "Description", 100, -5, true));
        Assert.Equal("Le taux de TVA doit être entre 0 et 100%.", exception1.Message);
        
        //Act & Assert - TVA > 100
        var exception2 = Assert.Throws<DomainException>(() => 
            new Product(Guid.NewGuid(), "Product", "Description", 100, 150, true));
        Assert.Equal("Le taux de TVA doit être entre 0 et 100%.", exception2.Message);
    }

    [Fact]
    public void ChangeIsActive_Should_Update_Status()
    {

        var product = new Product();
        product.UpdatePrice(50);
        

        product.ChangeIsActive(false);
        

        Assert.False(product.IsActive);
        
        //Act - Réactiver
        product.ChangeIsActive(true);
        

        Assert.True(product.IsActive);
    }

    [Fact]
    public void AssignSupplier_Should_Set_SupplierId()
    {

        var product = new Product();
        product.UpdatePrice(100);
        var supplierId = Guid.NewGuid();
        

        product.AssignSupplier(supplierId);
        

        Assert.Equal(supplierId, product.SupplierId);
    }

    [Fact]
    public void RemoveSupplier_Should_Clear_SupplierId()
    {

        var product = new Product(Guid.NewGuid(), "Product", "Desc", 100, 20, true, Guid.NewGuid());
        Assert.NotNull(product.SupplierId);
        

        product.RemoveSupplier();
        

        Assert.Null(product.SupplierId);
    }

    [Fact]
    public void ApplyDiscount_Should_Calculate_Correct_Final_Price()
    {

        var product = new Product();
        product.UpdatePrice(200);
        
        //Act - 25% de réduction sur 200
        product.ApplyDiscount(25);
        
        //Assert - 200 - (200 * 0.25) = 150
        Assert.Equal(150, product.Price);
    }

    [Fact]
    public void DefaultConstructor_Should_Create_Product_With_Default_Values()
    {

        var product = new Product();
        

        Assert.NotEqual(Guid.Empty, product.Id);
        Assert.Equal(string.Empty, product.Name);
        Assert.Equal(string.Empty, product.Description);
        Assert.Equal(0, product.Price);
        Assert.Equal(0, product.VATRate);
        Assert.True(product.IsActive);
        Assert.Null(product.SupplierId);
    }
}

