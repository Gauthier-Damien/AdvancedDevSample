using System.Net;

namespace AdvancedDevSample.Application.Exceptions
{
    
public class ApplicationServiceException : Exception
{
    public HttpStatusCode StatusCode { get; }
    
    public ApplicationServiceException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
    {
        StatusCode = statusCode;
    }
}
}
