using AdvancedDevSample.Application.DTOs.Auth;
using AdvancedDevSample.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedDevSample.API.Controllers
{
    /// <summary>
    /// Contrôleur d'authentification JWT.
    /// Gère le login, le rafraîchissement de token et la déconnexion.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authentifie un utilisateur et retourne un token JWT
        /// </summary>
        /// <param name="request">Credentials (username + password)</param>
        /// <returns>Token JWT + Refresh Token</returns>
        /// <response code="200">Authentification réussie</response>
        /// <response code="401">Credentials invalides</response>
        /// <response code="403">Compte désactivé</response>
        /// <remarks>
        /// Comptes de test disponibles :
        /// - Username: demo, Password: demo123 (Rôle: Student)
        /// - Username: admin, Password: admin123 (Rôle: Admin)
        /// 
        /// Le token d'accès expire après 60 minutes.
        /// Le refresh token expire après 7 jours.
        /// </remarks>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var response = _authService.Login(request);
            return Ok(response);
        }

        /// <summary>
        /// Rafraîchit un token d'accès expiré
        /// </summary>
        /// <param name="request">Refresh token</param>
        /// <returns>Nouveau token JWT + nouveau Refresh Token</returns>
        /// <response code="200">Token rafraîchi avec succès</response>
        /// <response code="401">Refresh token invalide ou expiré</response>
        /// <remarks>
        /// Utilisez le refresh token obtenu lors du login pour obtenir un nouveau token d'accès
        /// sans avoir à re-saisir les credentials.
        /// L'ancien refresh token est automatiquement révoqué.
        /// </remarks>
        [HttpPost("refresh")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Refresh([FromBody] RefreshTokenRequest request)
        {
            var response = _authService.RefreshToken(request);
            return Ok(response);
        }

        /// <summary>
        /// Endpoint de test pour vérifier l'authentification
        /// </summary>
        /// <returns>Informations sur l'utilisateur connecté</returns>
        /// <response code="200">Utilisateur authentifié</response>
        /// <response code="401">Non authentifié</response>
        [HttpGet("me")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetCurrentUser()
        {
            var username = User.Identity?.Name;
            var role = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            return Ok(new
            {
                UserId = userId,
                Username = username,
                Role = role,
                Message = "✅ Vous êtes authentifié avec succès !"
            });
        }
    }
}
