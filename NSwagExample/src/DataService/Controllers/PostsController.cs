using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace DataService.Controllers
{
    [Route("[controller]")]
    public class PostsController : Controller
    {
        private static Lazy<IList<BlogPostModel>> _cache = new Lazy<IList<BlogPostModel>>(() =>
        {
            var list = new List<BlogPostModel>();
            for (var i = 0; i < 100; i++)
            {
                list.Add(BlogPostModel.Random());
            }

            return list;
        }, true);

        [HttpGet("")]
        // NSwag seems to ignore the Type property of Prodcues - I have to define ProducesResponseType, otherwise the swagger.json output is crap
        [Produces("application/json", Type = typeof(List<BlogPostModel>))]
        [SwaggerOperation(operationId: "getBlogPosts")]
        [ProducesResponseType(typeof(List<BlogPostModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
            return Json(_cache.Value);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [SwaggerOperation(operationId: "getBlogPostById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BlogPostModel), (int)HttpStatusCode.OK)]
        public IActionResult GetPostById(string id)
        {
            var post = _cache.Value.FirstOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
            if (post == null)
            {
                return NotFound();
            }

            return Json(post);
        }

        [HttpGet("byTag/{tag}")]
        [Produces("application/json")]
        [SwaggerOperation(operationId: "getBlogPostsByTag")]
        [ProducesResponseType(typeof(BlogPostModel[]), (int)HttpStatusCode.OK)]
        public IActionResult GetPostsByTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
            {
                return Json(Enumerable.Empty<BlogPostModel>());
            }

            return Json(_cache.Value.Where(p => p.Tags.Contains(tag.ToLowerInvariant())));
        }
    }

    public class BlogPostModel
    {
        private static Random _rnd = new Random();

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; } = "Blog Title Lorem ipsum dolor sit amet";

        public string Content { get; set; } = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";

        public string Author { get; set; } = "Michael Conrad";

        public DateTime Published { get; set; }

        public DateTime Modified { get; set; }

        public string[] Tags { get; set; }

        public static BlogPostModel Random()
        {
            var published = DateTime.UtcNow.AddDays(_rnd.Next(50, 1000) * -1);
            return new BlogPostModel()
            {
                Tags = Enumerable.Repeat("tag", 5).Select(p => p + _rnd.Next(1, 100)).ToArray(),
                Published = published,
                Modified = published.AddDays(10)
            };
        }
    }
}