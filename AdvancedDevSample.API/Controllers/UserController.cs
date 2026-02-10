using AdvancedDevSample.Application.DTOs.Users;
using AdvancedDevSample.Application.Services;
using Microsoft.AspNetCore.Mvc;
namespace AdvancedDevSample.API.Controllers
{
    /// <summary>
    /// Contrôleur pour gérer les opérations CRUD sur les utilisateurs.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="UserController"/>.
        /// </summary>
        /// <param name="userService">Service pour gérer les utilisateurs.</param>
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Récupère tous les utilisateurs.
        /// </summary>
        /// <returns>Liste de tous les utilisateurs.</returns>
        /// <response code="200">Liste récupérée avec succès</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        /// <summary>
        /// Récupère un utilisateur spécifique par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur.</param>
        /// <returns>L'utilisateur correspondant.</returns>
        /// <response code="200">Utilisateur trouvé</response>
        /// <response code="404">Utilisateur non trouvé</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserById(Guid id)
        {
            var user = _userService.GetUserById(id);
            return Ok(user);
        }

        /// <summary>
        /// Crée un nouvel utilisateur.
        /// </summary>
        /// <param name="request">Données de l'utilisateur à créer.</param>
        /// <returns>L'utilisateur créé.</returns>
        /// <response code="201">Utilisateur créé avec succès</response>
        /// <response code="400">Données invalides</response>
        [HttpPost]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateUser([FromBody] CreateUserRequest request)
        {
            var user = _userService.CreateUser(request);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        /// <summary>
        /// Met à jour un utilisateur existant.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur à mettre à jour.</param>
        /// <param name="request">Données mises à jour de l'utilisateur.</param>
        /// <returns>L'utilisateur mis à jour.</returns>
        /// <response code="200">Utilisateur mis à jour avec succès</response>
        /// <response code="404">Utilisateur non trouvé</response>
        /// <response code="400">Données invalides</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUser(Guid id, [FromBody] UpdateUserRequest request)
        {
            var user = _userService.UpdateUser(id, request);
            return Ok(user);
        }

        /// <summary>
        /// Supprime un utilisateur par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur à supprimer.</param>
        /// <returns>Confirmation de la suppression.</returns>
        /// <response code="204">Utilisateur supprimé avec succès</response>
        /// <response code="404">Utilisateur non trouvé</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(Guid id)
        {
            _userService.DeleteUser(id);
            return NoContent();
        }
    }
}