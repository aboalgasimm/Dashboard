using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analytics.Api.Models
{
    public class ArticleDetails
    {
        [Key, ForeignKey("Article")]
        public int ArticleId { get; set; }
        public string Summary { get; set; } = string.Empty;
        public string HeroImageUrl { get; set; } = string.Empty;
        public int ReadingTimeSeconds { get; set; }

        public Article? Article { get; set; }
    }
}
