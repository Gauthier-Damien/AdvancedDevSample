using AdvancedDevSample.Domain.Exceptions;

namespace AdvancedDevSample.Domain.Entities
{
    /// <summary>
    /// Entité Utilisateur du système.
    /// Règles : username, email, prénom et nom obligatoires.
    /// </summary>
    public class User
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Role { get; private set; }
        public bool IsActive { get; private set; }

        public User()
        {
            Id = Guid.NewGuid();
            Username = string.Empty;
            Email = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Role = "User";
            IsActive = true;
        }

        /// <summary>
        /// Crée un utilisateur en validant les règles métier.
        /// </summary>
        public User(Guid id, string username, string email, string firstName, string lastName, string role = "User")
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new DomainException("Le nom d'utilisateur est obligatoire.");

            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("L'email est obligatoire.");

            if (!email.Contains("@"))
                throw new DomainException("L'email n'est pas valide.");

            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("Le prénom est obligatoire.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Le nom de famille est obligatoire.");

            Id = id;
            Username = username;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Role = role ?? "User";
            IsActive = true;
        }

        public void UpdateInfo(string username, string email, string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new DomainException("Le nom d'utilisateur est obligatoire.");

            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("L'email est obligatoire.");

            if (!email.Contains("@"))
                throw new DomainException("L'email n'est pas valide.");

            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("Le prénom est obligatoire.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Le nom de famille est obligatoire.");

            Username = username;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public void ChangeRole(string newRole)
        {
            if (string.IsNullOrWhiteSpace(newRole))
                throw new DomainException("Le rôle est obligatoire.");

            Role = newRole;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }

        public string FullName => $"{FirstName} {LastName}";
    }
}
