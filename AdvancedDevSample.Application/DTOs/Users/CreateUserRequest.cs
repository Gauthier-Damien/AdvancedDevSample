using System.ComponentModel.DataAnnotations;

namespace AdvancedDevSample.Application.DTOs.Users
{
    /// <summary>
    /// Requête de création d'un utilisateur.
    /// </summary>
    public class CreateUserRequest
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

        [StringLength(50)]
        public string Role { get; set; } = "User";
    }
}
