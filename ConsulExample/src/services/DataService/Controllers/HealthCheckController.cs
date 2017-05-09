using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace DataService.Controllers
{
    [Route("[Controller]")]
    public class HealthCheckController : Controller
    {
        public HealthCheckController()
        {
        }

        [HttpGet("")]
        [HttpHead("")]
        public IActionResult Ping()
        {
            return Ok("I'm fine");
        }
    }
}