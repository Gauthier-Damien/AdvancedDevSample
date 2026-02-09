using AdvancedDevSample.Domain.Exceptions;

namespace AdvancedDevSample.Domain.Entities
{
    /// <summary>
    /// Entité du catalogue produit.
    /// Invariant : le prix doit toujours être strictement positif.
    /// </summary>
    public class Product
    {
        public Guid Id { get; private set; }
        public decimal Price  { get; private set; }
        public bool IsActive  { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal VATRate { get; private set; }
        public Guid? SupplierId { get; private set; }

        public Product()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            Description = string.Empty;
            Price = 0;
            VATRate = 0;
            IsActive = true;
            SupplierId = null;
        }

        /// <summary>
        /// Crée un produit en validant les invariants métier.
        /// </summary>
        public Product(Guid id, string name, string description, decimal price, decimal vatRate, bool isActive = true, Guid? supplierId = null)
        {
            if (price <= 0)
                throw new DomainException("Le prix doit être strictement positif.");

            if (vatRate < 0 || vatRate > 100)
                throw new DomainException("Le taux de TVA doit être entre 0 et 100%.");

            Id = id;
            Name = name ?? string.Empty;
            Description = description ?? string.Empty;
            Price = price;
            VATRate = vatRate;
            IsActive = isActive;
            SupplierId = supplierId;
        }

        public Product(Guid id, decimal price, bool isActive)
        {
            if (price <= 0)
                throw new DomainException("Le prix doit être strictement positif.");

            Id = id;
            Price = price;
            IsActive = isActive;
            Name = string.Empty;
            Description = string.Empty;
            VATRate = 0;
            SupplierId = null;
        }

        /// <summary>
        /// Modifie le prix en vérifiant l'invariant (prix > 0) et que le produit est actif.
        /// </summary>
        public void UpdatePrice(decimal newPrice)
        {
            if(newPrice <= 0)
                throw new DomainException("Le prix doit être strictement positif.");
            if(!IsActive)
                throw new DomainException("Impossible de modifier un produit inactif.");

            Price = newPrice;
        }

        /// <summary>
        /// Applique une réduction en pourcentage.
        /// Vérifie que le prix résultant reste strictement positif (invariant préservé).
        /// </summary>
        public void ApplyDiscount(decimal discountPercentage)
        {
            if(discountPercentage <= 0)
                throw new DomainException("Le pourcentage de réduction doit être strictement positif.");
            if(discountPercentage > 100)
                throw new DomainException("Le pourcentage de réduction ne peut pas dépasser 100%.");

            decimal discountAmount = Price * (discountPercentage / 100);
            decimal newPrice = Price - discountAmount;

            if (newPrice <= 0)
                throw new DomainException("Le prix après réduction doit rester strictement positif.");

            UpdatePrice(newPrice);
        }

        public void ChangeIsActive(bool newState)
        {
            IsActive = newState;
        }

        public void AssignSupplier(Guid supplierId)
        {
            SupplierId = supplierId;
        }

        public void RemoveSupplier()
        {
            SupplierId = null;
        }
    }
}
