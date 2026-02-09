using System.ComponentModel.DataAnnotations;

namespace AdvancedDevSample.Application.DTOs.Users
{
    /// <summary>
    /// Requête de mise à jour d'un utilisateur.
    /// </summary>
    public class UpdateUserRequest
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;
    }
}
