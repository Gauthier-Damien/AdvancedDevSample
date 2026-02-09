using AdvancedDevSample.Domain.Entities;

namespace AdvancedDevSample.Domain.Interfaces.Users
{
    /// <summary>
    /// Port (Interface) : Contrat de persistance des utilisateurs
    /// </summary>
    public interface IUserRepository
    {
        User? GetByID(Guid id);
        IEnumerable<User> GetAll();
        void Save(User user);
    }
}
