using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DataService.Shared
{
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
        /// Gets or sets the post's tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public string[] Tags { get; set; }

        /// <summary>
        /// Created a random instance of this model.
        /// </summary>
        /// <returns>A new instance of <see cref="BlogPostModel"/> with random values.</returns>
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