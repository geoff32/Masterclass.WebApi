using Microsoft.AspNetCore.Mvc;

namespace MasterClass.WebApi.Controllers
{
    [ApiController]
    [Route("api/_system")]
    public class DiagnosticController : ControllerBase
    {
        [HttpGet, HttpHead, Route("healthcheck")]
        public IActionResult HealthCheck() => Ok("system_ok");
    }
}