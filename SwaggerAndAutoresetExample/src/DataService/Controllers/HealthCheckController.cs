using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DataService.Controllers
{
    [Route("[Controller]")]
    public class HealthCheckController : Controller
    {
        public HealthCheckController()
        {
        }

        [HttpGet("")]
        [SwaggerOperation(operationId: "getHealthCheck")]
        public IActionResult Ping()
        {
            return Ok("I'm fine");
        }
    }
}