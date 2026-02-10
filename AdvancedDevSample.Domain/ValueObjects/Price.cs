namespace AdvancedDevSample.Domain.ValueObjects
{
    /// <summary>
    /// Value Object représentant un prix avec TVA.
    /// Immutable : toute modification retourne une nouvelle instance.
    /// Invariant : le montant HT doit être strictement positif.
    /// </summary>
    public sealed class Price : IEquatable<Price>
    {
        public decimal AmountExcludingTax { get; }
        public decimal VATRate { get; }
        public decimal VATAmount => AmountExcludingTax * (VATRate / 100);
        public decimal AmountIncludingTax => AmountExcludingTax + VATAmount;

        public Price(decimal amountExcludingTax, decimal vatRate)
        {
            if (amountExcludingTax <= 0)
                throw new ArgumentException("Le montant hors taxe doit être strictement positif.", nameof(amountExcludingTax));

            if (vatRate < 0 || vatRate > 100)
                throw new ArgumentException("Le taux de TVA doit être entre 0 et 100%.", nameof(vatRate));

            AmountExcludingTax = amountExcludingTax;
            VATRate = vatRate;
        }

        /// <summary>
        /// Retourne une nouvelle instance avec le prix réduit (immutabilité).
        /// </summary>
        public Price ApplyDiscount(decimal discountPercentage)
        {
            if (discountPercentage <= 0 || discountPercentage > 100)
                throw new ArgumentException("Le pourcentage de réduction doit être entre 0.01 et 100%.", nameof(discountPercentage));

            var newAmount = AmountExcludingTax * (1 - discountPercentage / 100);
            
            if (newAmount <= 0)
                throw new InvalidOperationException("Le prix après réduction doit rester strictement positif.");

            return new Price(newAmount, VATRate);
        }

        public Price WithVATRate(decimal newVATRate)
        {
            return new Price(AmountExcludingTax, newVATRate);
        }

        /// <summary>
        /// Égalité basée sur les valeurs (Value Object pattern).
        /// </summary>
        public bool Equals(Price? other)
        {
            if (other is null)
                return false;

            return AmountExcludingTax == other.AmountExcludingTax 
                   && VATRate == other.VATRate;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Price);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AmountExcludingTax, VATRate);
        }


        public override string ToString()
        {
            return $"{AmountExcludingTax:C} HT (TVA {VATRate}%) = {AmountIncludingTax:C} TTC";
        }
    }
}
