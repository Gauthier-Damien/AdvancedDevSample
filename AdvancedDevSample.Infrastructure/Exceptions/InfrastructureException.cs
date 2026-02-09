namespace AdvancedDevSample.Infrastructure.Exceptions;

/// <summary>
/// Exception levée par la couche Infrastructure pour les erreurs techniques
/// (base de données, système de fichiers, services externes, etc.).
/// </summary>
public class InfrastructureException : Exception
{
    public InfrastructureException(string message) : base(message)
    {
    }

    public InfrastructureException(string message, Exception innerException) : base(message, innerException)
    {
    }
}