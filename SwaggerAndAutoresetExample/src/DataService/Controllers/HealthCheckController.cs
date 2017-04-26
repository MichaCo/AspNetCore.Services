using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace DataService.Controllers
{
    [Route("[Controller]")]
    public class HealthCheckController : Controller
    {
        [HttpGet("")]
        [HttpHead("")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}