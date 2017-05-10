using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace DataService.Controllers
{
    /*
    Don't know why, but nswag generates an empty path here and the UI blows up 
    */
    [Route("[Controller]")]
    public class HealthCheckController : Controller
    {
        public HealthCheckController()
        {
        }

        [HttpGet("")]
        [SwaggerOperation(operationId: "getHealthCheck")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Ping()
        {
            return Ok("I'm fine");
        }
    }
}