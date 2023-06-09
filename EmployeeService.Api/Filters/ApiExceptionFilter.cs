using EmployeeService.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using System.Security.Authentication;
using System.Text.Json;

namespace EmployeeService.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            HttpStatusCode statusCode;
            switch (context.Exception)
            {
                case ValidationException validationException:
                case ArgumentException argumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case InvalidCredentialException invalidCredentialException:
                    statusCode = HttpStatusCode.Forbidden;
                    break;
                case AuthenticationException authenticationException:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;

                    var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

                    if (configuration.GetValue("SendErrors", false))
                    {
  
                        //var notificationApiClient = context.HttpContext.RequestServices.GetRequiredService<INotificationApiClient>();
                        //await notificationApiClient.SendErrorAsync(message);
                    }

                    break;
            }

            context.HttpContext.Response.StatusCode = (int)statusCode;
            await context.HttpContext.Response
                .WriteAsync(JsonSerializer
                    .Serialize(new ExceptionModel
                    {
                        StatusCode = context.HttpContext.Response.StatusCode,
                        ErrorMessage = context.Exception.Message,
                    }));
            context.ExceptionHandled = true;
        }
    }
    public class ExceptionModel
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
