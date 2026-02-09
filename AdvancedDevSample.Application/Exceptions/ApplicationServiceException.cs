using System.Net;

namespace AdvancedDevSample.Application.Exceptions
{
    /// <summary>
    /// Exception levée par la couche Application pour les erreurs applicatives
    /// (ressource introuvable, opération invalide, etc.).
    /// Permet d'associer un code HTTP au message d'erreur.
    /// </summary>
    public class ApplicationServiceException : Exception
    {
        public HttpStatusCode StatusCode { get; }
    
        public ApplicationServiceException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
