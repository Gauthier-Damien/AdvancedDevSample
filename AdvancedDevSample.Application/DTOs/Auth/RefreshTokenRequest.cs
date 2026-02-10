using System.ComponentModel.DataAnnotations;

namespace AdvancedDevSample.Application.DTOs.Auth
{
    /// <summary>
    /// Requête de rafraîchissement de token
    /// </summary>
    public class RefreshTokenRequest
    {
        /// <summary>
        /// Token de rafraîchissement
        /// </summary>
        [Required(ErrorMessage = "Le refresh token est obligatoire")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
