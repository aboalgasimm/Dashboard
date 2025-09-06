using System.ComponentModel.DataAnnotations;

namespace Analytics.Api.Models
{
    public class Article
    {
        public int Id { get; set; }
        [Required] public string Title { get; set; } = string.Empty;
        [Required] public string Category { get; set; } = string.Empty;
        public DateTime PublishedAt { get; set; }

        public ArticleDetails? Details { get; set; }
        public ICollection<Pageview> Pageviews { get; set; } = new List<Pageview>();
    }
}
