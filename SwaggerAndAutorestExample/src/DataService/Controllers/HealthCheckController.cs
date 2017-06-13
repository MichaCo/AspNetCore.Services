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

        /// <summary>
        /// Simple health check http endpoint.
        /// </summary>
        /// <returns>A string indicating the service is available.</returns>
        [HttpGet("")]
        [SwaggerOperation(operationId: "getHealthCheck")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Ping()
        {
            return Ok("I'm fine");
        }
    }
}