using System.ComponentModel.DataAnnotations;

namespace AdvancedDevSample.Application.DTOs.Suppliers
{
    /// <summary>
    /// Requête de mise à jour d'un fournisseur.
    /// </summary>
    public class UpdateSupplierRequest
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(500)]
        public string Address { get; set; } = string.Empty;
    }
}
