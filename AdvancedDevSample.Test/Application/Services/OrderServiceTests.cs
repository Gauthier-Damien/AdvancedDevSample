using AdvancedDevSample.Application.DTOs.Orders;
using AdvancedDevSample.Application.Exceptions;
using AdvancedDevSample.Application.Services;
using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Interfaces.Orders;

namespace AdvancedDevSample.Test.Application.Services;

/// <summary>
/// Classe de tests unitaires pour <see cref="OrderService"/>.
/// </summary>
public class OrderServiceTests
{
    private readonly FakeOrderRepository _fakeRepository;
    private readonly OrderService _service;

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="OrderServiceTests"/>.
    /// </summary>
    public OrderServiceTests()
    {
        _fakeRepository = new FakeOrderRepository();
        _service = new OrderService(_fakeRepository);
    }[Fact]
    public void GetAllOrders_Should_Return_All_Orders()
    {

        var order1 = new Order(Guid.NewGuid(), "Address 1");
        var order2 = new Order(Guid.NewGuid(), "Address 2");

        _fakeRepository.Add(order1);
        _fakeRepository.Add(order2);


        var result = _service.GetAllOrders().ToList();


        Assert.Equal(2, result.Count);
    }[Fact]
    public void GetOrdersByCustomer_Should_Return_Only_Customer_Orders()
    {

        var customerId = Guid.NewGuid();
        var otherCustomerId = Guid.NewGuid();

        var customerOrder1 = new Order(customerId, "Address 1");
        var customerOrder2 = new Order(customerId, "Address 2");
        var otherOrder = new Order(otherCustomerId, "Address 3");

        _fakeRepository.Add(customerOrder1);
        _fakeRepository.Add(customerOrder2);
        _fakeRepository.Add(otherOrder);


        var result = _service.GetOrdersByCustomer(customerId).ToList();


        Assert.Equal(2, result.Count);
        Assert.All(result, o => Assert.Equal(customerId, o.CustomerId));
    }[Fact]
    public void GetOrderById_Should_Return_Order_When_Exists()
    {

        var order = new Order(Guid.NewGuid(), "Test Address");
        _fakeRepository.Add(order);


        var result = _service.GetOrderById(order.Id);


        Assert.NotNull(result);
        Assert.Equal(order.Id, result.Id);
        Assert.Equal("Test Address", result.DeliveryAddress);
    }[Fact]
    public void GetOrderById_Should_Throw_Exception_When_Not_Found()
    {

        var exception = Assert.Throws<ApplicationServiceException>((Action)(() =>
            _service.GetOrderById(Guid.NewGuid())));

        Assert.Equal("Commande non trouvée", exception.Message);
    }[Fact]
    public void CreateOrder_Should_Create_And_Return_Order()
    {

        var request = new CreateOrderRequest
        {
            CustomerId = Guid.NewGuid(),
            DeliveryAddress = "123 Main Street",
            Notes = "Test notes"
        };


        var result = _service.CreateOrder(request);


        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(request.CustomerId, result.CustomerId);
        Assert.Equal("123 Main Street", result.DeliveryAddress);
        Assert.Equal("Test notes", result.Notes);
        Assert.Equal(OrderStatus.Pending, result.Status);
        Assert.Equal("Pending", result.StatusLabel);
    }[Fact]
    public void UpdateOrderTotals_Should_Update_Amounts()
    {

        var order = new Order(Guid.NewGuid(), "Test Address");
        _fakeRepository.Add(order);

        var request = new UpdateOrderTotalsRequest
        {
            TotalAmountExcludingTax = 100m,
            TotalAmountIncludingTax = 120m
        };


        var result = _service.UpdateOrderTotals(order.Id, request);


        Assert.Equal(100m, result.TotalAmountExcludingTax);
        Assert.Equal(120m, result.TotalAmountIncludingTax);
    }[Fact]
    public void UpdateOrderTotals_Should_Throw_Exception_When_Not_Found()
    {

        var request = new UpdateOrderTotalsRequest
        {
            TotalAmountExcludingTax = 100m,
            TotalAmountIncludingTax = 120m
        };


        var exception = Assert.Throws<ApplicationServiceException>((Action)(() =>
            _service.UpdateOrderTotals(Guid.NewGuid(), request)));

        Assert.Equal("Commande non trouvée", exception.Message);
    }[Fact]
    public void ConfirmOrder_Should_Change_Status_To_Confirmed()
    {

        var order = new Order(Guid.NewGuid(), "Test Address");
        order.UpdateTotals(100m, 120m);
        _fakeRepository.Add(order);


        var result = _service.ConfirmOrder(order.Id);


        Assert.Equal(OrderStatus.Confirmed, result.Status);
        Assert.Equal("Confirmed", result.StatusLabel);
    }[Fact]
    public void ShipOrder_Should_Change_Status_To_Shipped()
    {

        var order = new Order(Guid.NewGuid(), "Test Address");
        order.UpdateTotals(100m, 120m);
        order.Confirm();
        _fakeRepository.Add(order);


        var result = _service.ShipOrder(order.Id);


        Assert.Equal(OrderStatus.Shipped, result.Status);
        Assert.Equal("Shipped", result.StatusLabel);
    }[Fact]
    public void DeliverOrder_Should_Change_Status_To_Delivered()
    {

        var order = new Order(Guid.NewGuid(), "Test Address");
        order.UpdateTotals(100m, 120m);
        order.Confirm();
        order.Ship();
        _fakeRepository.Add(order);


        var result = _service.DeliverOrder(order.Id);


        Assert.Equal(OrderStatus.Delivered, result.Status);
        Assert.Equal("Delivered", result.StatusLabel);
    }[Fact]
    public void CancelOrder_Should_Change_Status_To_Cancelled()
    {

        var order = new Order(Guid.NewGuid(), "Test Address");
        _fakeRepository.Add(order);


        var result = _service.CancelOrder(order.Id);


        Assert.Equal(OrderStatus.Cancelled, result.Status);
        Assert.Equal("Cancelled", result.StatusLabel);
    }[Fact]
    public void ConfirmOrder_Should_Throw_Exception_When_Not_Found()
    {

        var exception = Assert.Throws<ApplicationServiceException>((Action)(() =>
            _service.ConfirmOrder(Guid.NewGuid())));

        Assert.Equal("Commande non trouvée", exception.Message);
    }[Fact]
    public void ShipOrder_Should_Throw_Exception_When_Not_Found()
    {

        var exception = Assert.Throws<ApplicationServiceException>((Action)(() =>
            _service.ShipOrder(Guid.NewGuid())));

        Assert.Equal("Commande non trouvée", exception.Message);
    }[Fact]
    public void DeliverOrder_Should_Throw_Exception_When_Not_Found()
    {

        var exception = Assert.Throws<ApplicationServiceException>((Action)(() =>
            _service.DeliverOrder(Guid.NewGuid())));

        Assert.Equal("Commande non trouvée", exception.Message);
    }[Fact]
    public void CancelOrder_Should_Throw_Exception_When_Not_Found()
    {

        var exception = Assert.Throws<ApplicationServiceException>((Action)(() =>
            _service.CancelOrder(Guid.NewGuid())));

        Assert.Equal("Commande non trouvée", exception.Message);
    }
}

/// <summary>
/// Implémentation fictive de <see cref="IOrderRepository"/> pour les tests (InMemory).
/// </summary>
public class FakeOrderRepository : IOrderRepository
{
    private readonly Dictionary<Guid, Order> _orders = new();

    /// <summary>
    /// Récupère une commande par son identifiant.
    /// </summary>
    /// <param name="id">Identifiant de la commande.</param>
    /// <returns>La commande correspondante ou null si elle n'existe pas.</returns>
    public Order? GetByID(Guid id)
    {
        _orders.TryGetValue(id, out var order);
        return order;
    }

    /// <summary>
    /// Récupère toutes les commandes.
    /// </summary>
    /// <returns>Liste de toutes les commandes.</returns>
    public IEnumerable<Order> GetAll()
    {
        return _orders.Values.ToList();
    }

    /// <summary>
    /// Récupère les commandes d'un client spécifique.
    /// </summary>
    /// <param name="customerId">Identifiant du client.</param>
    /// <returns>Liste des commandes du client.</returns>
    public IEnumerable<Order> GetByCustomerId(Guid customerId)
    {
        return _orders.Values.Where(o => o.CustomerId == customerId).ToList();
    }

    /// <summary>
    /// Sauvegarde une commande.
    /// </summary>
    /// <param name="order">Commande à sauvegarder.</param>
    public void Save(Order order)
    {
        _orders[order.Id] = order;
    }

    /// <summary>
    /// Ajoute une nouvelle commande.
    /// </summary>
    /// <param name="order">Commande à ajouter.</param>
    public void Add(Order order)
    {
        _orders[order.Id] = order;
    }
}
