using System.ComponentModel.DataAnnotations;

namespace AdvancedDevSample.Application.DTOs.Suppliers
{
    /// <summary>
    /// Requête de création d'un fournisseur.
    /// </summary>
    public class CreateSupplierRequest
    {
        [Required(ErrorMessage = "Le nom du fournisseur est obligatoire")]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(500)]
        public string Address { get; set; } = string.Empty;
    }
}
