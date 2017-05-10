using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clients;
using Microsoft.AspNetCore.Mvc;

namespace ConsumingWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataServiceClient _service;

        public HomeController(IDataServiceClient service)
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
