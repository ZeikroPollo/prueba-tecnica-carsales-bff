using System.Net;
using System.Text.Json;

namespace Carsales.RickAndMorty.BFF.Middleware
{
    /// <summary>
    /// Middleware global para manejo uniforme de errores.
    /// Centraliza el mapeo de excepciones a respuestas JSON.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Continúa con el siguiente middleware / endpoint
                await _next(context);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Error de validación.");
                await WriteError(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Recurso no encontrado.");
                await WriteError(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al llamar API externa.");
                await WriteError(context, HttpStatusCode.BadGateway, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado.");
                await WriteError(context, HttpStatusCode.InternalServerError, "Ha ocurrido un error inesperado.");
            }
        }

        private async Task WriteError(HttpContext context, HttpStatusCode status, string message)
        {
            context.Response.StatusCode = (int)status;
            context.Response.ContentType = "application/json";

            var response = new
            {
                status = (int)status,
                error = message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}