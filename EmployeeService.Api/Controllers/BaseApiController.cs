using EmployeeService.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers
{
    [ApiController]
    [ModelValidationFilter]
    [ApiExceptionFilter]
    [Produces("application/json")]
    public class BaseApiController : ControllerBase
    {

    }
}
