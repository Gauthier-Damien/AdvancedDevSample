using AdvancedDevSample.Domain.Entities;

namespace AdvancedDevSample.Application.DTOs.Orders
{
    /// <summary>
    /// Réponse contenant les informations d'une commande.
    /// </summary>
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalAmountExcludingTax { get; set; }
        public decimal TotalAmountIncludingTax { get; set; }
        public OrderStatus Status { get; set; }
        public string StatusLabel { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
