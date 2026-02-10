using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Interfaces.Users;
using System.Collections.Concurrent;

namespace AdvancedDevSample.Infrastructure.Repositories
{
    /// <summary>
    /// Adapter : implémente IUserRepository avec stockage InMemory.
    /// </summary>
    public class EfUserRepository : IUserRepository
    {
        private static readonly ConcurrentDictionary<Guid, User> _users = new();

        public User? GetByID(Guid id)
        {
            _users.TryGetValue(id, out var user);
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _users.Values.ToList();
        }

        public void Save(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _users[user.Id] = user;
        }
    }
}
