using AdvancedDevSample.Domain.Exceptions;

namespace AdvancedDevSample.Domain.Entities
{
    /// <summary>
    /// Entité Fournisseur.
    /// Règles : nom et email obligatoires, validation basique de l'email.
    /// </summary>
    public class Supplier
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Address { get; private set; }
        public bool IsActive { get; private set; }

        public Supplier()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            Address = string.Empty;
            IsActive = true;
        }

        /// <summary>
        /// Crée un fournisseur en validant les règles métier.
        /// </summary>
        public Supplier(Guid id, string name, string email, string phoneNumber, string address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Le nom du fournisseur est obligatoire.");

            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("L'email du fournisseur est obligatoire.");

            if (!email.Contains("@"))
                throw new DomainException("L'email du fournisseur n'est pas valide.");

            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber ?? string.Empty;
            Address = address ?? string.Empty;
            IsActive = true;
        }

        public void UpdateInfo(string name, string email, string phoneNumber, string address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Le nom du fournisseur est obligatoire.");

            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("L'email du fournisseur est obligatoire.");

            if (!email.Contains("@"))
                throw new DomainException("L'email du fournisseur n'est pas valide.");

            Name = name;
            Email = email;
            PhoneNumber = phoneNumber ?? string.Empty;
            Address = address ?? string.Empty;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
