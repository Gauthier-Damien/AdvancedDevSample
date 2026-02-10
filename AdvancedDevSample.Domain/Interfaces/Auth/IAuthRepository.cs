using AdvancedDevSample.Domain.Entities;

namespace AdvancedDevSample.Domain.Interfaces.Auth
{
    /// <summary>
    /// Interface du repository d'authentification (pattern Repository).
    /// Définie dans le Domain, implémentée dans l'Infrastructure.
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// Récupère un utilisateur par son nom d'utilisateur
        /// </summary>
        User? GetUserByUsername(string username);

        /// <summary>
        /// Sauvegarde un refresh token
        /// </summary>
        void SaveRefreshToken(RefreshToken refreshToken);

        /// <summary>
        /// Récupère un refresh token par sa valeur
        /// </summary>
        RefreshToken? GetRefreshToken(string token);

        /// <summary>
        /// Révoque tous les refresh tokens d'un utilisateur
        /// </summary>
        void RevokeAllUserTokens(Guid userId, string reason);

        /// <summary>
        /// Crée un utilisateur de démo (pour développement)
        /// </summary>
        void SeedUser(string username, string password, string role);
    }
}
