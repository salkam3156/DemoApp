using DemoApp.Infrastructure.Exceptions.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DemoApp.PublicApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult CatchUnhandledError()
        {
            var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandler is null)
            {
                return NoContent();
            }

            // should be called in a string.Format style to give structured logging sinks a chance

            return exceptionHandler.Error switch
            {
                DatabaseAccessException
                    => StatusCode((int)HttpStatusCode.FailedDependency, "We're currently having issues fetching your data."),

                _ => StatusCode((int)HttpStatusCode.InternalServerError, "Sorry, we're having some issues. Please be patient while we resolve them.")
            };
        }
    }
}
