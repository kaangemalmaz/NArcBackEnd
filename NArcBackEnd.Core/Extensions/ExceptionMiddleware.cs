using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace NArcBackEnd.Core.Extensions
{
    // Exception Handler - 2
    public class ExceptionMiddleware
    {
        private RequestDelegate _next; //gelen requestti handler etmemizi sağlayacak!

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json"; //bu hata verme olasılığı olduğu içni ekleniyor her türlü json dönsün diye.
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";
            IEnumerable<ValidationFailure> errors = null;
            if (e.GetType() == typeof(ValidationException))
            {
                message = e.Message;
                errors = ((ValidationException)e).Errors;

                return httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    Errors = errors,
                    Messages = message,
                    StatusCode = 400
                }.ToString()); // tostring json dönmesi için önemli! oluşturulan details sayfalarında tostringi override edildi.
            }

            return httpContext.Response.WriteAsync(new ErrorHandlerDetails
            {
                Messages = e.Message,
                StatusCode = httpContext.Response.StatusCode
            }.ToString());


        }
    }
}
