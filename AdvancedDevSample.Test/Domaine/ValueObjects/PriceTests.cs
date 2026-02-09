using AdvancedDevSample.Domain.ValueObjects;

namespace AdvancedDevSample.Test.Domaine.ValueObjects;

/// <summary>
/// Tests unitaires pour le Value Object Price
/// </summary>
public class PriceTests
{
    [Fact]
    public void Constructor_Should_Create_Price_With_Valid_Values()
    {

        var price = new Price(100m, 20m);
        

        Assert.Equal(100m, price.AmountExcludingTax);
        Assert.Equal(20m, price.VATRate);
    }

    [Fact]
    public void Constructor_Should_Throw_Exception_When_Amount_Is_Zero()
    {

        var exception = Assert.Throws<ArgumentException>(() => new Price(0m, 20m));
        Assert.Equal("Le montant hors taxe doit être strictement positif. (Parameter 'amountExcludingTax')", exception.Message);
    }

    [Fact]
    public void Constructor_Should_Throw_Exception_When_Amount_Is_Negative()
    {

        var exception = Assert.Throws<ArgumentException>(() => new Price(-10m, 20m));
        Assert.Equal("Le montant hors taxe doit être strictement positif. (Parameter 'amountExcludingTax')", exception.Message);
    }

    [Fact]
    public void Constructor_Should_Throw_Exception_When_VATRate_Is_Negative()
    {

        var exception = Assert.Throws<ArgumentException>(() => new Price(100m, -5m));
        Assert.Equal("Le taux de TVA doit être entre 0 et 100%. (Parameter 'vatRate')", exception.Message);
    }

    [Fact]
    public void Constructor_Should_Throw_Exception_When_VATRate_Is_Greater_Than_100()
    {

        var exception = Assert.Throws<ArgumentException>(() => new Price(100m, 150m));
        Assert.Equal("Le taux de TVA doit être entre 0 et 100%. (Parameter 'vatRate')", exception.Message);
    }

    [Fact]
    public void VATAmount_Should_Calculate_Correctly()
    {

        var price = new Price(100m, 20m);
        

        var vatAmount = price.VATAmount;
        

        Assert.Equal(20m, vatAmount); // 100 * 20% = 20
    }

    [Fact]
    public void AmountIncludingTax_Should_Calculate_Correctly()
    {

        var price = new Price(100m, 20m);
        

        var totalAmount = price.AmountIncludingTax;
        

        Assert.Equal(120m, totalAmount); // 100 + 20 = 120
    }

    [Fact]
    public void ApplyDiscount_Should_Return_New_Price_With_Discount_Applied()
    {

        var price = new Price(100m, 20m);
        

        var discountedPrice = price.ApplyDiscount(25m); // 25% de réduction
        

        Assert.Equal(75m, discountedPrice.AmountExcludingTax); // 100 - 25% = 75
        Assert.Equal(20m, discountedPrice.VATRate); // TVA inchangée
        Assert.Equal(90m, discountedPrice.AmountIncludingTax); // 75 + 15 TVA = 90
    }

    [Fact]
    public void ApplyDiscount_Should_Throw_Exception_When_Percentage_Is_Zero()
    {

        var price = new Price(100m, 20m);
        

        var exception = Assert.Throws<ArgumentException>(() => price.ApplyDiscount(0m));
        Assert.Equal("Le pourcentage de réduction doit être entre 0.01 et 100%. (Parameter 'discountPercentage')", exception.Message);
    }

    [Fact]
    public void ApplyDiscount_Should_Throw_Exception_When_Percentage_Is_Negative()
    {

        var price = new Price(100m, 20m);
        

        var exception = Assert.Throws<ArgumentException>(() => price.ApplyDiscount(-10m));
        Assert.Equal("Le pourcentage de réduction doit être entre 0.01 et 100%. (Parameter 'discountPercentage')", exception.Message);
    }

    [Fact]
    public void ApplyDiscount_Should_Throw_Exception_When_Percentage_Is_Greater_Than_100()
    {

        var price = new Price(100m, 20m);
        

        var exception = Assert.Throws<ArgumentException>(() => price.ApplyDiscount(150m));
        Assert.Equal("Le pourcentage de réduction doit être entre 0.01 et 100%. (Parameter 'discountPercentage')", exception.Message);
    }

    [Fact]
    public void ApplyDiscount_Should_Throw_Exception_When_Result_Would_Be_Zero_Or_Negative()
    {

        var price = new Price(100m, 20m);
        

        var exception = Assert.Throws<InvalidOperationException>(() => price.ApplyDiscount(100m));
        Assert.Equal("Le prix après réduction doit rester strictement positif.", exception.Message);
    }

    [Fact]
    public void WithVATRate_Should_Return_New_Price_With_Different_VATRate()
    {

        var price = new Price(100m, 20m);
        

        var newPrice = price.WithVATRate(5.5m);
        

        Assert.Equal(100m, newPrice.AmountExcludingTax); // Montant HT inchangé
        Assert.Equal(5.5m, newPrice.VATRate); // Nouveau taux de TVA
        Assert.Equal(5.5m, newPrice.VATAmount); // 100 * 5.5% = 5.5
        Assert.Equal(105.5m, newPrice.AmountIncludingTax); // 100 + 5.5 = 105.5
    }

    [Fact]
    public void Equals_Should_Return_True_For_Same_Values()
    {

        var price1 = new Price(100m, 20m);
        var price2 = new Price(100m, 20m);
        

        Assert.True(price1.Equals(price2));
        Assert.True(price1 == price2);
    }

    [Fact]
    public void Equals_Should_Return_False_For_Different_Amounts()
    {

        var price1 = new Price(100m, 20m);
        var price2 = new Price(90m, 20m);
        

        Assert.False(price1.Equals(price2));
        Assert.True(price1 != price2);
    }

    [Fact]
    public void Equals_Should_Return_False_For_Different_VATRates()
    {

        var price1 = new Price(100m, 20m);
        var price2 = new Price(100m, 10m);
        

        Assert.False(price1.Equals(price2));
        Assert.True(price1 != price2);
    }

    [Fact]
    public void GetHashCode_Should_Be_Same_For_Equal_Prices()
    {

        var price1 = new Price(100m, 20m);
        var price2 = new Price(100m, 20m);
        

        Assert.Equal(price1.GetHashCode(), price2.GetHashCode());
    }

    [Fact]
    public void ToString_Should_Return_Formatted_String()
    {

        var price = new Price(100m, 20m);
        

        var result = price.ToString();
        

        Assert.Contains("100", result);
        Assert.Contains("20", result);
        Assert.Contains("120", result);
    }

    [Fact]
    public void Price_Should_Be_Immutable()
    {

        var originalPrice = new Price(100m, 20m);
        

        var discountedPrice = originalPrice.ApplyDiscount(10m);
        var newVATPrice = originalPrice.WithVATRate(10m);
        
        //Assert - Les instances originales ne changent pas
        Assert.Equal(100m, originalPrice.AmountExcludingTax);
        Assert.Equal(20m, originalPrice.VATRate);
        
        // Les nouvelles instances sont différentes
        Assert.NotEqual(originalPrice, discountedPrice);
        Assert.NotEqual(originalPrice, newVATPrice);
    }

    [Fact]
    public void ApplyDiscount_Multiple_Times_Should_Compound()
    {

        var price = new Price(100m, 20m);
        

        var price1 = price.ApplyDiscount(10m); // -10% = 90
        var price2 = price1.ApplyDiscount(10m); // -10% de 90 = 81
        

        Assert.Equal(81m, price2.AmountExcludingTax);
    }

    [Fact]
    public void Constructor_Should_Accept_Zero_VATRate()
    {

        var price = new Price(100m, 0m);
        

        Assert.Equal(0m, price.VATRate);
        Assert.Equal(0m, price.VATAmount);
        Assert.Equal(100m, price.AmountIncludingTax);
    }

    [Fact]
    public void Constructor_Should_Accept_VATRate_100()
    {

        var price = new Price(100m, 100m);
        

        Assert.Equal(100m, price.VATRate);
        Assert.Equal(100m, price.VATAmount);
        Assert.Equal(200m, price.AmountIncludingTax);
    }
}
