using Analytics.Api.Models;

namespace Analytics.Api.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Articles.Any()) return;

            var rnd = new Random();

            var categories = new[] { "Tech", "Health", "Business" };
            var articles = new List<Article>();

            for (int i = 1; i <= 25; i++)
            {
                var article = new Article
                {
                    Title = $"Sample Article {i}",
                    Category = categories[rnd.Next(categories.Length)],
                    PublishedAt = DateTime.UtcNow.AddDays(-rnd.Next(1, 90)),
                    Details = new ArticleDetails
                    {
                        Summary = $"Summary for article {i}",
                        HeroImageUrl = $"https://picsum.photos/seed/{i}/600/400",
                        ReadingTimeSeconds = rnd.Next(60, 600)
                    }
                };

                // Generate pageviews
                var pageviews = new List<Pageview>();
                for (int j = 0; j < rnd.Next(800, 2500); j++)
                {
                    pageviews.Add(new Pageview
                    {
                        ViewedAt = DateTime.UtcNow.AddDays(-rnd.Next(0, 90)),
                        DurationSeconds = rnd.Next(10, 600),
                        IsBounce = rnd.NextDouble() < 0.3
                    });
                }
                article.Pageviews = pageviews;

                articles.Add(article);
            }

            context.Articles.AddRange(articles);
            context.SaveChanges();
        }
    }
}
