namespace Analytics.Api.DTOs
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime PublishedAt { get; set; }
        public ArticleDetailsDto? Details { get; set; }
    }

    public class ArticleDetailsDto
    {
        public string Summary { get; set; } = string.Empty;
        public string HeroImageUrl { get; set; } = string.Empty;
        public int ReadingTimeSeconds { get; set; }
    }
}
