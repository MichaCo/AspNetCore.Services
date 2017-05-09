using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsumingWebsite.Clients.DataService;
using Microsoft.AspNetCore.Mvc;

namespace ConsumingWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataService _service;

        public HomeController(IDataService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _service.GetBlogPostsAsync();

            return View(posts);
        }
        
        public IActionResult Error()
        {
            return View();
        }
    }
}
