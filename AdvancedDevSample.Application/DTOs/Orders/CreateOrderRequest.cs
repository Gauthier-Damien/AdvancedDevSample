using System.ComponentModel.DataAnnotations;

namespace AdvancedDevSample.Application.DTOs.Orders
{
    /// <summary>
    /// Requête de création d'une commande.
    /// </summary>
    public class CreateOrderRequest
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        [StringLength(500)]
        public string DeliveryAddress { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;
    }
}
