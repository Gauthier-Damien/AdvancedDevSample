using AdvancedDevSample.Application.DTOs.Users;
using AdvancedDevSample.Application.Exceptions;
using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Interfaces.Users;

namespace AdvancedDevSample.Application.Services
{
    /// <summary>
    /// Gestion des utilisateurs (CRUD).
    /// </summary>
    public class UserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<UserResponse> GetAllUsers()
        {
            var users = _repo.GetAll();
            return users.Where(u => u.IsActive).Select(MapToResponse);
        }

        public UserResponse GetUserById(Guid id)
        {
            var user = GetUser(id);
            return MapToResponse(user);
        }

        public UserResponse CreateUser(CreateUserRequest request)
        {
            var user = new User(Guid.NewGuid(), request.Username, request.Email, request.FirstName, request.LastName, request.Role);
            _repo.Save(user);
            return MapToResponse(user);
        }

        public UserResponse UpdateUser(Guid id, UpdateUserRequest request)
        {
            var user = GetUser(id);
            user.UpdateInfo(request.Username, request.Email, request.FirstName, request.LastName);
            _repo.Save(user);
            return MapToResponse(user);
        }

        /// <summary>
        /// Soft delete : désactive l'utilisateur.
        /// </summary>
        public void DeleteUser(Guid id)
        {
            var user = GetUser(id);
            user.SetActive(false);
            _repo.Save(user);
        }

        private User GetUser(Guid id)
        {
            return _repo.GetByID(id) ?? throw new ApplicationServiceException("Utilisateur non trouvé", System.Net.HttpStatusCode.NotFound);
        }

        private UserResponse MapToResponse(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName,
                Role = user.Role,
                IsActive = user.IsActive
            };
        }
    }
}