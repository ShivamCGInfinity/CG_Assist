namespace CG_Assist_MCPServer.Middleware
{
    public class ApiKeyValidationMiddleware
    {
        private readonly RequestDelegate _next;

        private const string ValidApiKey = "LEAVE_API_123";

        public ApiKeyValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Allow ping endpoint without API key for healthcheck purposes
            if (context.Request.Path.StartsWithSegments("/ping"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.ContainsKey("X-API-KEY") ||
                context.Request.Headers["X-API-KEY"] != ValidApiKey)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsync(
                    "Unauthorized: Invalid or missing API Key.");

                return;
            }

            await _next(context);
        }
    }
}