using DemoApp.Infrastructure.Exceptions.Database;
using DemoApp.Infrastructure.Exceptions.FileStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoApp.PublicApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
            => _logger = logger;
        
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public IActionResult CatchUnhandledError()
        {
            var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandler is null)
            {
                return NoContent();
            }

            _logger.LogError($"{nameof(ErrorController)}: An application dependency failed to meets its demands. The operation failed. Reason: {exceptionHandler.Error}");

            return exceptionHandler.Error switch
            {
                DatabaseAccessException or FileStorageAccessException
                    => StatusCode(StatusCodes.Status424FailedDependency, "We're currently having issues fetching your data."),

                _ => StatusCode(StatusCodes.Status500InternalServerError, "Sorry, we're having some issues. Please be patient while we resolve them.")
            };
        }
    }
}
