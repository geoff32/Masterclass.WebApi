using MasterClass.Core.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MasterClass.WebApi.Controllers
{
    [ApiController]
    [Route("api/_system")]
    public class DiagnosticController : ControllerBase
    {
        private readonly DiagnosticOptions _options;

        public DiagnosticController(IOptionsSnapshot<DiagnosticOptions> options)
        {
            _options = options.Value;
        }

        [HttpGet, HttpHead, Route("healthcheck")]
        public IActionResult HealthCheck() => Ok(_options.HealthCheckContent);
    }
}