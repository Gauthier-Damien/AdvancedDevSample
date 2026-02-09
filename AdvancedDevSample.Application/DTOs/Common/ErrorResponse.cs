namespace AdvancedDevSample.Application.DTOs.Common
{
    /// <summary>
    /// DTO pour les réponses d'erreur de l'API.
    /// </summary>
    public class ErrorResponse
    {
        public string Error { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
    }
}
