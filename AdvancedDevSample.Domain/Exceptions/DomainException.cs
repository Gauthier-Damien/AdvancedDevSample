namespace AdvancedDevSample.Domain.Exceptions
{
    /// <summary>
    /// Exception levée par les entités du Domain lors de violations d'invariants
    /// ou de règles métier (prix négatif, transitions d'état interdites, etc.).
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}
