using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Exceptions
{
    public class EntidadNoEncontradaException : Exception
    {
        public EntidadNoEncontradaException(string message) : base(message) { }
    }

    public class ReglaDeNegocioException : Exception
    {
        public ReglaDeNegocioException(string message) : base(message) { }
    }

    public interface IExceptionHandler
    {
        ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken);
    }

    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Excepción no manejada: {Message}", exception.Message);

            var statusCode = exception switch
            {
                EntidadNoEncontradaException => StatusCodes.Status404NotFound,
                ReglaDeNegocioException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            httpContext.Response.StatusCode = statusCode;
            /*await httpContext.Response.WriteAsJsonAsync(new
            {
                Titulo = "Error",
                Status = statusCode,
                Detalle = exception.Message
            }, cancellationToken);*/

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                Titulo = "Error",
                Status = statusCode,
                Detalle = exception.Message
            }));

            return true;
        }
    }

    public class ErrorResponse
    {
        public string Titulo { get; set; }
        public int Status { get; set; }
        public string Detalle { get; set; }
        public Dictionary<string, string[]>? Errores { get; set; }

        public ErrorResponse(string titulo, int status, string detalle)
        {
            Titulo = titulo;
            Status = status;
            Detalle = detalle;
        }
    }
}