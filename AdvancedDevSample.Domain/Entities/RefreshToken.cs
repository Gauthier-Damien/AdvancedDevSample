using AdvancedDevSample.Domain.Exceptions;

namespace AdvancedDevSample.Domain.Entities
{
    /// <summary>
    /// Entité représentant un token de rafraîchissement JWT.
    /// Permet de renouveler l'accès sans re-authentification.
    /// </summary>
    public class RefreshToken
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsRevoked { get; private set; }
        public string? RevokedReason { get; private set; }

        /// <summary>
        /// Constructeur par défaut pour EF Core
        /// </summary>
        public RefreshToken()
        {
            Id = Guid.NewGuid();
            Token = string.Empty;
            CreatedAt = DateTime.UtcNow;
            IsRevoked = false;
        }

        /// <summary>
        /// Crée un nouveau refresh token
        /// </summary>
        public RefreshToken(Guid userId, string token, int expirationDays = 7)
        {
            if (userId == Guid.Empty)
                throw new DomainException("L'identifiant utilisateur est obligatoire.");

            if (string.IsNullOrWhiteSpace(token))
                throw new DomainException("Le token est obligatoire.");

            if (expirationDays <= 0)
                throw new DomainException("La durée d'expiration doit être positive.");

            Id = Guid.NewGuid();
            UserId = userId;
            Token = token;
            CreatedAt = DateTime.UtcNow;
            ExpiresAt = DateTime.UtcNow.AddDays(expirationDays);
            IsRevoked = false;
        }

        /// <summary>
        /// Vérifie si le token est encore valide
        /// </summary>
        public bool IsValid()
        {
            return !IsRevoked && DateTime.UtcNow < ExpiresAt;
        }

        /// <summary>
        /// Révoque le token (ex: déconnexion, compromission)
        /// </summary>
        public void Revoke(string reason)
        {
            if (IsRevoked)
                throw new DomainException("Ce token est déjà révoqué.");

            IsRevoked = true;
            RevokedReason = reason;
        }
    }
}
