using AdvancedDevSample.Application.Exceptions;
using AdvancedDevSample.Domain.Exceptions;
using System.Net;
using System.Text.Json;
using AdvancedDevSample.Infrastructure.Exceptions;

namespace AdvancedDevSample.API.Middlewares
{
    /// <summary>
    /// Middleware global de gestion centralisée des exceptions.
    /// Intercepte toutes les exceptions et les transforme en réponses HTTP appropriées.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Initialise le middleware avec le prochain délégué de la pipeline et le logger.
        /// </summary>
        /// <param name="next">Prochain middleware dans la chaîne de traitement</param>
        /// <param name="logger">Logger pour enregistrer les erreurs</param>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Traite la requête HTTP et capture les exceptions pour les transformer en réponses appropriées.
        /// </summary>
        /// <param name="context">Contexte HTTP de la requête</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException domainEx)
            {
                // Violations des règles métier (prix négatif, invariants, etc.)
                _logger.LogError(domainEx, "Erreur metier");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { error = "Erreur metier", detail = domainEx.Message });
            }
            catch (ApplicationServiceException ex)
            {
                // Erreurs applicatives (ressource introuvable, opération invalide, etc.)
                _logger.LogWarning(ex, "Erreur Applicative");

                context.Response.StatusCode = (int)ex.StatusCode;
                await context.Response.WriteAsJsonAsync(new { error = "Ressource Introuvable", detail = ex.Message });
            }
            catch (InfrastructureException ex)
            {
                // Erreurs d'infrastructure (base de données, système de fichiers, etc.)
                _logger.LogWarning(ex, "Erreur Infrastructure");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { error = "Erreur Infrastructure", detail = ex.Message });
            }
            catch (Exception ex)
            {
                // Erreurs inattendues - ne jamais exposer les détails internes en production
                _logger.LogCritical(ex, "Erreur inattendue");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = "Erreur Interne" }) );
            }
        }
    }
}
