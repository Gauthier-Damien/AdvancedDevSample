using AdvancedDevSample.Application.DTOs.Auth;
using AdvancedDevSample.Application.Exceptions;
using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Interfaces.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AdvancedDevSample.Application.Services
{
    /// <summary>
    /// Service d'authentification JWT.
    /// Gère la génération de tokens, la validation et le rafraîchissement.
    /// </summary>
    public class AuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// Authentifie un utilisateur et génère un token JWT
        /// </summary>
        public LoginResponse Login(LoginRequest request)
        {
            // Récupérer l'utilisateur
            var user = _authRepository.GetUserByUsername(request.Username);
            if (user == null)
                throw new ApplicationServiceException("Nom d'utilisateur ou mot de passe invalide", HttpStatusCode.Unauthorized);

            // Vérifier le mot de passe
            if (user.PasswordHash == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new ApplicationServiceException("Nom d'utilisateur ou mot de passe invalide", HttpStatusCode.Unauthorized);

            // Vérifier que l'utilisateur est actif
            if (!user.IsActive)
                throw new ApplicationServiceException("Ce compte est désactivé", HttpStatusCode.Forbidden);

            // Générer les tokens
            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            // Sauvegarder le refresh token
            var refreshTokenEntity = new RefreshToken(
                user.Id,
                refreshToken,
                int.Parse(_configuration["JwtSettings:RefreshTokenExpirationDays"] ?? "7")
            );
            _authRepository.SaveRefreshToken(refreshTokenEntity);

            // Retourner la réponse
            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "60")),
                Username = user.Username,
                Role = user.Role
            };
        }

        /// <summary>
        /// Rafraîchit un token d'accès expiré
        /// </summary>
        public LoginResponse RefreshToken(RefreshTokenRequest request)
        {
            // Récupérer le refresh token
            var refreshToken = _authRepository.GetRefreshToken(request.RefreshToken);
            if (refreshToken == null)
                throw new ApplicationServiceException("Refresh token invalide", HttpStatusCode.Unauthorized);

            // Vérifier la validité
            if (!refreshToken.IsValid())
                throw new ApplicationServiceException("Refresh token expiré ou révoqué", HttpStatusCode.Unauthorized);

            // Récupérer l'utilisateur
            var user = _authRepository.GetUserByUsername(refreshToken.UserId.ToString());
            if (user == null || !user.IsActive)
                throw new ApplicationServiceException("Utilisateur introuvable ou inactif", HttpStatusCode.Unauthorized);

            // Révoquer l'ancien refresh token
            refreshToken.Revoke("Utilisé pour rafraîchissement");
            _authRepository.SaveRefreshToken(refreshToken);

            // Générer de nouveaux tokens
            var newAccessToken = GenerateAccessToken(user);
            var newRefreshToken = GenerateRefreshToken();

            // Sauvegarder le nouveau refresh token
            var newRefreshTokenEntity = new RefreshToken(
                user.Id,
                newRefreshToken,
                int.Parse(_configuration["JwtSettings:RefreshTokenExpirationDays"] ?? "7")
            );
            _authRepository.SaveRefreshToken(newRefreshTokenEntity);

            return new LoginResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "60")),
                Username = user.Username,
                Role = user.Role
            };
        }

        /// <summary>
        /// Génère un token d'accès JWT
        /// </summary>
        private string GenerateAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["JwtSettings:Secret"] ?? throw new InvalidOperationException("JWT Secret non configuré")
            ));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("FullName", user.FullName)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "60")),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Génère un refresh token aléatoire sécurisé
        /// </summary>
        private static string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}
