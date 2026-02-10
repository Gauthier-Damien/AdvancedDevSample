using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Exceptions;

namespace AdvancedDevSample.Test.Domaine.Entities;

/// <summary>
/// Tests unitaires pour l'entité Order
/// </summary>
public class OrderTests
{
    [Fact]
    public void Constructor_Should_Create_Order_With_Valid_Data()
    {

        var customerId = Guid.NewGuid();
        var deliveryAddress = "123 Main Street, Paris";


        var order = new Order(customerId, deliveryAddress, "Test notes");


        Assert.NotEqual(Guid.Empty, order.Id);
        Assert.NotNull(order.OrderNumber);
        Assert.StartsWith("ORD-", order.OrderNumber);
        Assert.Equal(customerId, order.CustomerId);
        Assert.Equal(deliveryAddress, order.DeliveryAddress);
        Assert.Equal("Test notes", order.Notes);
        Assert.Equal(OrderStatus.Pending, order.Status);
        Assert.Equal(0, order.TotalAmountExcludingTax);
        Assert.Equal(0, order.TotalAmountIncludingTax);
    }

    [Fact]
    public void Constructor_Should_Throw_Exception_When_CustomerId_Is_Empty()
    {

        var exception = Assert.Throws<DomainException>(() =>
            new Order(Guid.Empty, "123 Main Street"));

        Assert.Equal("L'identifiant du client est obligatoire.", exception.Message);
    }

    [Fact]
    public void Constructor_Should_Throw_Exception_When_DeliveryAddress_Is_Empty()
    {

        var exception = Assert.Throws<DomainException>(() =>
            new Order(Guid.NewGuid(), ""));

        Assert.Equal("L'adresse de livraison est obligatoire.", exception.Message);
    }

    [Fact]
    public void UpdateTotals_Should_Update_Amounts()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");


        order.UpdateTotals(100m, 120m);


        Assert.Equal(100m, order.TotalAmountExcludingTax);
        Assert.Equal(120m, order.TotalAmountIncludingTax);
    }

    [Fact]
    public void UpdateTotals_Should_Throw_Exception_When_AmountExcludingTax_Is_Negative()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");


        var exception = Assert.Throws<DomainException>(() =>
            order.UpdateTotals(-10m, 120m));

        Assert.Equal("Le montant HT ne peut pas être négatif.", exception.Message);
    }

    [Fact]
    public void UpdateTotals_Should_Throw_Exception_When_IncludingTax_Less_Than_ExcludingTax()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");


        var exception = Assert.Throws<DomainException>(() =>
            order.UpdateTotals(100m, 90m));

        Assert.Equal("Le montant TTC doit être supérieur ou égal au montant HT.", exception.Message);
    }

    [Fact]
    public void UpdateTotals_Should_Throw_Exception_When_Order_Is_Cancelled()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");
        order.UpdateTotals(100m, 120m);
        order.Cancel();


        var exception = Assert.Throws<DomainException>(() =>
            order.UpdateTotals(200m, 240m));

        Assert.Equal("Impossible de modifier une commande annulée.", exception.Message);
    }

    [Fact]
    public void Confirm_Should_Change_Status_To_Confirmed()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");
        order.UpdateTotals(100m, 120m);


        order.Confirm();


        Assert.Equal(OrderStatus.Confirmed, order.Status);
    }

    [Fact]
    public void Confirm_Should_Throw_Exception_When_Not_Pending()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");
        order.UpdateTotals(100m, 120m);
        order.Confirm();


        var exception = Assert.Throws<DomainException>(() =>
            order.Confirm());

        Assert.Equal("Seules les commandes en attente peuvent être confirmées.", exception.Message);
    }

    [Fact]
    public void Confirm_Should_Throw_Exception_When_Amount_Is_Zero()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");


        var exception = Assert.Throws<DomainException>(() =>
            order.Confirm());

        Assert.Equal("Impossible de confirmer une commande sans montant.", exception.Message);
    }

    [Fact]
    public void Ship_Should_Change_Status_To_Shipped()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");
        order.UpdateTotals(100m, 120m);
        order.Confirm();


        order.Ship();


        Assert.Equal(OrderStatus.Shipped, order.Status);
    }

    [Fact]
    public void Ship_Should_Throw_Exception_When_Not_Confirmed()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");


        var exception = Assert.Throws<DomainException>(() =>
            order.Ship());

        Assert.Equal("Seules les commandes confirmées peuvent être expédiées.", exception.Message);
    }

    [Fact]
    public void Deliver_Should_Change_Status_To_Delivered()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");
        order.UpdateTotals(100m, 120m);
        order.Confirm();
        order.Ship();


        order.Deliver();


        Assert.Equal(OrderStatus.Delivered, order.Status);
    }

    [Fact]
    public void Deliver_Should_Throw_Exception_When_Not_Shipped()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");
        order.UpdateTotals(100m, 120m);
        order.Confirm();


        var exception = Assert.Throws<DomainException>(() =>
            order.Deliver());

        Assert.Equal("Seules les commandes expédiées peuvent être livrées.", exception.Message);
    }

    [Fact]
    public void Cancel_Should_Change_Status_To_Cancelled_When_Pending()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");


        order.Cancel();


        Assert.Equal(OrderStatus.Cancelled, order.Status);
    }

    [Fact]
    public void Cancel_Should_Work_When_Confirmed()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");
        order.UpdateTotals(100m, 120m);
        order.Confirm();


        order.Cancel();


        Assert.Equal(OrderStatus.Cancelled, order.Status);
    }

    [Fact]
    public void Cancel_Should_Throw_Exception_When_Delivered()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");
        order.UpdateTotals(100m, 120m);
        order.Confirm();
        order.Ship();
        order.Deliver();


        var exception = Assert.Throws<DomainException>(() =>
            order.Cancel());

        Assert.Equal("Impossible d'annuler une commande déjà livrée.", exception.Message);
    }

    [Fact]
    public void Cancel_Should_Throw_Exception_When_Shipped()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");
        order.UpdateTotals(100m, 120m);
        order.Confirm();
        order.Ship();


        var exception = Assert.Throws<DomainException>(() =>
            order.Cancel());

        Assert.Equal("Impossible d'annuler une commande déjà expédiée.", exception.Message);
    }

    [Fact]
    public void UpdateDeliveryAddress_Should_Update_Address()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");


        order.UpdateDeliveryAddress("456 New Street");


        Assert.Equal("456 New Street", order.DeliveryAddress);
    }

    [Fact]
    public void UpdateDeliveryAddress_Should_Throw_Exception_When_Address_Is_Empty()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");


        var exception = Assert.Throws<DomainException>(() =>
            order.UpdateDeliveryAddress(""));

        Assert.Equal("L'adresse de livraison est obligatoire.", exception.Message);
    }

    [Fact]
    public void UpdateDeliveryAddress_Should_Throw_Exception_When_Shipped()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");
        order.UpdateTotals(100m, 120m);
        order.Confirm();
        order.Ship();


        var exception = Assert.Throws<DomainException>(() =>
            order.UpdateDeliveryAddress("456 New Street"));

        Assert.Equal("Impossible de modifier l'adresse d'une commande expédiée ou livrée.", exception.Message);
    }

    [Fact]
    public void ChangeStatus_Should_Throw_Exception_When_Reactivating_Cancelled()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");
        order.Cancel();


        var exception = Assert.Throws<DomainException>(() =>
            order.ChangeStatus(OrderStatus.Pending));

        Assert.Equal("Impossible de réactiver une commande annulée.", exception.Message);
    }

    [Fact]
    public void ChangeStatus_Should_Throw_Exception_When_Modifying_Delivered()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");
        order.UpdateTotals(100m, 120m);
        order.Confirm();
        order.Ship();
        order.Deliver();


        var exception = Assert.Throws<DomainException>(() =>
            order.ChangeStatus(OrderStatus.Pending));

        Assert.Equal("Impossible de modifier une commande déjà livrée.", exception.Message);
    }

    [Fact]
    public void DefaultConstructor_Should_Create_Order_With_Default_Values()
    {

        var order = new Order();


        Assert.NotEqual(Guid.Empty, order.Id);
        Assert.NotNull(order.OrderNumber);
        Assert.Equal(Guid.Empty, order.CustomerId);
        Assert.Equal(OrderStatus.Pending, order.Status);
        Assert.Equal(0, order.TotalAmountExcludingTax);
        Assert.Equal(0, order.TotalAmountIncludingTax);
    }

    [Fact]
    public void OrderNumber_Should_Have_Correct_Format()
    {

        var order = new Order(Guid.NewGuid(), "123 Main Street");


        Assert.Matches(@"^ORD-\d{8}-\d{4}-[a-f0-9]{6}$", order.OrderNumber);
    }

    [Fact]
    public void OrderDate_Should_Be_Set_To_UtcNow()
    {

        var before = DateTime.UtcNow;


        var order = new Order(Guid.NewGuid(), "123 Main Street");
        var after = DateTime.UtcNow;


        Assert.InRange(order.OrderDate, before, after);
    }
}
