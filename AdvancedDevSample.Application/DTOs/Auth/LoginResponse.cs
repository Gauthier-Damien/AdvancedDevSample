namespace AdvancedDevSample.Application.DTOs.Auth
{
    /// <summary>
    /// Réponse de connexion contenant le token JWT et le refresh token
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Token JWT d'accès (expire après 60 minutes par défaut)
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// Token de rafraîchissement (expire après 7 jours par défaut)
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// Date d'expiration du token d'accès
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Nom d'utilisateur
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Rôle de l'utilisateur
        /// </summary>
        public string Role { get; set; } = string.Empty;
    }
}
