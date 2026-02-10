using AdvancedDevSample.Domain.Exceptions;

namespace AdvancedDevSample.Domain.Entities
{
    /// <summary>
    /// Entité Commande avec machine à états.
    /// Transitions autorisées : Pending → Confirmed → Shipped → Delivered
    /// Annulation possible uniquement avant expédition.
    /// </summary>
    public class Order
    {
        private static readonly Random _random = new();

        public Guid Id { get; private set; }
        public string OrderNumber { get; private set; }
        public DateTime OrderDate { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal TotalAmountExcludingTax { get; private set; }
        public decimal TotalAmountIncludingTax { get; private set; }
        public OrderStatus Status { get; private set; }
        public string DeliveryAddress { get; private set; }
        public string Notes { get; private set; }

        public Order()
        {
            Id = Guid.NewGuid();
            OrderNumber = GenerateOrderNumber();
            OrderDate = DateTime.UtcNow;
            CustomerId = Guid.Empty;
            TotalAmountExcludingTax = 0;
            TotalAmountIncludingTax = 0;
            Status = OrderStatus.Pending;
            DeliveryAddress = string.Empty;
            Notes = string.Empty;
        }

        public Order(Guid customerId, string deliveryAddress, string notes = "")
        {
            if (customerId == Guid.Empty)
                throw new DomainException("L'identifiant du client est obligatoire.");

            if (string.IsNullOrWhiteSpace(deliveryAddress))
                throw new DomainException("L'adresse de livraison est obligatoire.");

            Id = Guid.NewGuid();
            OrderNumber = GenerateOrderNumber();
            OrderDate = DateTime.UtcNow;
            CustomerId = customerId;
            TotalAmountExcludingTax = 0;
            TotalAmountIncludingTax = 0;
            Status = OrderStatus.Pending;
            DeliveryAddress = deliveryAddress;
            Notes = notes ?? string.Empty;
        }

        public void UpdateTotals(decimal amountExcludingTax, decimal amountIncludingTax)
        {
            if (amountExcludingTax < 0)
                throw new DomainException("Le montant HT ne peut pas être négatif.");

            if (amountIncludingTax < amountExcludingTax)
                throw new DomainException("Le montant TTC doit être supérieur ou égal au montant HT.");

            if (Status == OrderStatus.Cancelled)
                throw new DomainException("Impossible de modifier une commande annulée.");

            TotalAmountExcludingTax = amountExcludingTax;
            TotalAmountIncludingTax = amountIncludingTax;
        }

        /// <summary>
        /// Valide les transitions d'état autorisées.
        /// </summary>
        public void ChangeStatus(OrderStatus newStatus)
        {
            if (Status == OrderStatus.Cancelled && newStatus != OrderStatus.Cancelled)
                throw new DomainException("Impossible de réactiver une commande annulée.");

            if (Status == OrderStatus.Delivered && newStatus != OrderStatus.Delivered)
                throw new DomainException("Impossible de modifier une commande déjà livrée.");

            Status = newStatus;
        }

        /// <summary>
        /// Transition : Pending → Confirmed.
        /// </summary>
        public void Confirm()
        {
            if (Status != OrderStatus.Pending)
                throw new DomainException("Seules les commandes en attente peuvent être confirmées.");

            if (TotalAmountExcludingTax <= 0)
                throw new DomainException("Impossible de confirmer une commande sans montant.");

            ChangeStatus(OrderStatus.Confirmed);
        }

        /// <summary>
        /// Transition : Confirmed → Shipped.
        /// </summary>
        public void Ship()
        {
            if (Status != OrderStatus.Confirmed)
                throw new DomainException("Seules les commandes confirmées peuvent être expédiées.");

            ChangeStatus(OrderStatus.Shipped);
        }

        /// <summary>
        /// Transition : Shipped → Delivered.
        /// </summary>
        public void Deliver()
        {
            if (Status != OrderStatus.Shipped)
                throw new DomainException("Seules les commandes expédiées peuvent être livrées.");

            ChangeStatus(OrderStatus.Delivered);
        }

        /// <summary>
        /// Annulation possible uniquement si non expédiée.
        /// </summary>
        public void Cancel()
        {
            if (Status == OrderStatus.Delivered)
                throw new DomainException("Impossible d'annuler une commande déjà livrée.");

            if (Status == OrderStatus.Shipped)
                throw new DomainException("Impossible d'annuler une commande déjà expédiée.");

            ChangeStatus(OrderStatus.Cancelled);
        }

        public void UpdateDeliveryAddress(string newAddress)
        {
            if (string.IsNullOrWhiteSpace(newAddress))
                throw new DomainException("L'adresse de livraison est obligatoire.");

            if (Status != OrderStatus.Pending && Status != OrderStatus.Confirmed)
                throw new DomainException("Impossible de modifier l'adresse d'une commande expédiée ou livrée.");

            DeliveryAddress = newAddress;
        }

        private static string GenerateOrderNumber()
        {
            var date = DateTime.UtcNow;
            var random = _random.Next(1000, 9999);
            var guid = Guid.NewGuid().ToString("N")[..6];
            return $"ORD-{date:yyyyMMdd}-{random}-{guid}";
        }
    }

    public enum OrderStatus
    {
        Pending = 0,
        Confirmed = 1,
        Shipped = 2,
        Delivered = 3,
        Cancelled = 4
    }
}
