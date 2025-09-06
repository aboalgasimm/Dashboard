namespace Analytics.Api.DTOs
{
    public class PageviewDto
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string ArticleTitle { get; set; } = string.Empty;
        public DateTime ViewedAt { get; set; }
        public int DurationSeconds { get; set; }
        public bool IsBounce { get; set; }
    }
}
