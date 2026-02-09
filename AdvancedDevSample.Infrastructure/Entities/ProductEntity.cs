namespace AdvancedDevSample.Infrastructure.Entities
{
    /// <summary>
    /// Modèle de persistance pour Entity Framework (non utilisé avec InMemory).
    /// Mapping : ProductEntity ↔ Product (entité du Domain).
    /// </summary>
    public class ProductEntity
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
