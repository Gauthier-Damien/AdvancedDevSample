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
    product.UpdatePrice(10); //Inital value
    
    //Simulate Unactivated product
    //product.IsActive=true;
    typeof(Product).GetProperty(nameof(Product.IsActive))?.SetValue(product, false);
    
    //Act & Assert
    var exception = Assert.Throws<DomainException>(() => product.UpdatePrice(20));
    
    Assert.Equal("Impossible de modifier un produit inactif.", exception.Message);
}

[Fact]
public void ApplyDiscount_Should_Decrease_Price()
{
    Product product = new Product();
    product.UpdatePrice(100); //Init value
    
    //Act
    product.ApplyDiscount(30);
    
    //Assert
    Assert.Equal(70, product.Price);

}
}
