using MasterClass.WebApi.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasterClass.WebApi.Controllers
{
    [Route("api/alcohol"), Authorize(Policy = Policies.REQUIRED_ALCOHOL_MAJORITY)]
    public class AlcoholController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}