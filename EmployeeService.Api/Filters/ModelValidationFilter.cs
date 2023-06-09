using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Api.Filters
{
    public class ModelValidationFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                throw new ValidationException(string.Join(" ", context.ModelState
                    .SelectMany(keyValuePair => keyValuePair.Value.Errors)
                    .Select(modelError => modelError.ErrorMessage)
                    .ToArray()));
            }

            await next().ConfigureAwait(false);
        }
    }
}
