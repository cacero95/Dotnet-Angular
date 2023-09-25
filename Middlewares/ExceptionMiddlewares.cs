using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middlewares
{
    public class ExceptionMiddlewares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddlewares> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddlewares( RequestDelegate next, ILogger<ExceptionMiddlewares> logger, IHostEnvironment env )
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync( HttpContext context ) {
            try {
                await _next( context );
            } catch ( Exception err ) {
                _logger.LogInformation( "There is a error" );
                _logger.LogInformation( "---------------" );
                _logger.LogError( err, err.Message );
                _logger.LogInformation( "---------------" );
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = _env.IsDevelopment()
                    ? new ApiException( context.Response.StatusCode, err.Message, err.StackTrace?.ToString() )
                    : new ApiException( context.Response.StatusCode, err.Message, "Interval server Error" );
                var options = new JsonSerializerOptions{
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var jsonResponse = JsonSerializer.Serialize( response, options );
                await context.Response.WriteAsync( jsonResponse );
            }
        }
    }
}