using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using DataService.Shared;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DataService.Controllers
{
    /// <summary>
    /// The blog post service.
    /// </summary>
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
}