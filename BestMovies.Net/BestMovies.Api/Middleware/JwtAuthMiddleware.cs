using BestMovies.Api.Service;

namespace BestMovies.Api.Middleware
{
    public class JwtAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AuthenticationService authService)
        {
            if (authService.AuthenticateRequest(context) == false)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
