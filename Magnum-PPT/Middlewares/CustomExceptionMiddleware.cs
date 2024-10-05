using Newtonsoft.Json;
using System.Net;

namespace Magnum_PPT.Middlewares
{
    public class CustomExceptionMiddleware
    {
            private readonly RequestDelegate _next;
            private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Continuar la solicitud
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Manejo de excepción
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Registrar la excepción
            _logger.LogError(exception, "Ha ocurrido una excepción no controlada.");

            // Tipo de respuesta
            context.Response.ContentType = "application/json";

            // Código predeterminado
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Definir mensaje al cliente
            var response = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = "Se produjo un error en el servidor. Por favor, inténtalo de nuevo más tarde."
            };

            // Convertir a formato JSON
            var result = JsonConvert.SerializeObject(response);
            await context.Response.WriteAsync(result);
        }
    }

    // Clase para estructurar la respuesta de error
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
