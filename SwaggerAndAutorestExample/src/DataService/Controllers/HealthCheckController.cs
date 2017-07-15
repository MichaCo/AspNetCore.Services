using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DataService.Controllers
{
    /// <summary>
    /// A basic health check service
    /// </summary>
    [Route("[Controller]")]
    public class HealthCheckController : Controller
    {
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