using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Interfaces.Auth;
using System.Collections.Concurrent;

namespace AdvancedDevSample.Infrastructure.Repositories
{
    /// <summary>
    /// Repository d'authentification (InMemory pour démo).
    /// En production, utiliser Entity Framework avec vraie base de données.
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        private static readonly ConcurrentDictionary<Guid, User> _users = new();
        private static readonly ConcurrentDictionary<string, RefreshToken> _refreshTokens = new();

        public User? GetUserByUsername(string username)
        {
            return _users.Values.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public void SaveRefreshToken(RefreshToken refreshToken)
        {
            if (refreshToken == null)
                throw new ArgumentNullException(nameof(refreshToken));

            _refreshTokens[refreshToken.Token] = refreshToken;
        }

        public RefreshToken? GetRefreshToken(string token)
        {
            _refreshTokens.TryGetValue(token, out var refreshToken);
            return refreshToken;
        }

        public void RevokeAllUserTokens(Guid userId, string reason)
        {
            var userTokens = _refreshTokens.Values.Where(t => t.UserId == userId && !t.IsRevoked);
            foreach (var token in userTokens)
            {
                token.Revoke(reason);
                _refreshTokens[token.Token] = token;
            }
        }

        /// <summary>
        /// Crée un utilisateur de démo avec mot de passe hashé (pour développement)
        /// </summary>
        public void SeedUser(string username, string password, string role)
        {
            // Vérifier si l'utilisateur existe déjà
            if (_users.Values.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
                return;

            var user = new User(
                Guid.NewGuid(),
                username,
                $"{username}@advanceddevsample.com",
                username == "admin" ? "Admin" : "Étudiant",
                username == "admin" ? "System" : "Demo",
                role
            );

            // Hasher le mot de passe avec BCrypt
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            user.SetPassword(passwordHash);

            _users[user.Id] = user;
        }
    }
}
