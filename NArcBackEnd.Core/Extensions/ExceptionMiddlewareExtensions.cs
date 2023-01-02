using Microsoft.AspNetCore.Builder;

namespace NArcBackEnd.Core.Extensions
{
    // Exception Handler - 3
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
