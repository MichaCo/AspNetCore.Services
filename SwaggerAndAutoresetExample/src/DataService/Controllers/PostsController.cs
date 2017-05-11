using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

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

        /// <summary>
        /// Gets all blog posts.
        /// </summary>
        /// <returns>A list of blog posts.</returns>
        [HttpGet("")]
        [Produces("application/json", Type = typeof(BlogPostModel[]))]
        [SwaggerOperation(operationId: "getBlogPosts")]
        [ProducesResponseType(typeof(BlogPostModel[]), (int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
            return Json(_cache.Value);
        }

        /// <summary>
        /// Gets a blog post by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The blog post.</returns>
        /// <remarks>
        /// As <c>404</c> is a valid response code, clients should return <c>null</c> for those responses instead of throwing an exception.
        /// </remarks>
        /// <response code="200">The blog post for the <paramref name="id"/>.</response>
        /// <response code="404">No post found for the <paramref name="id"/>.</response>
        [HttpGet("{id}")]
        [Produces("application/json", Type = typeof(BlogPostModel))]
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

        /// <summary>
        /// Gets all blog posts filtered by tag.
        /// </summary>
        /// <param name="tag">The tag to filter.</param>
        /// <returns>A list of blog posts if any posts found for <paramref name="tag"/> or an empty list.</returns>
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

    /// <summary>
    /// The model representing a blog post's meta data.
    /// </summary>
    public class BlogPostModel
    {
        private static Random _rnd = new Random();

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Required]
        public string Title { get; set; } = "Blog Title Lorem ipsum dolor sit amet";

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        [Required]
        public string Content { get; set; } = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public string Author { get; set; } = "Michael Conrad";

        /// <summary>
        /// Gets or sets the published.
        /// </summary>
        /// <value>
        /// The published.
        /// </value>
        [Required]
        public DateTime Published { get; set; }

        /// <summary>
        /// Gets or sets the modified.
        /// </summary>
        /// <value>
        /// The modified.
        /// </value>
        public DateTime? Modified { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public string[] Tags { get; set; }

        internal static BlogPostModel Random()
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