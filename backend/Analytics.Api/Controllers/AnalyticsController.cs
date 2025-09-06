using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Analytics.Api.Data;
using Analytics.Api.Models;
using Analytics.Api.DTOs;

namespace Analytics.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ArticlesController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/articles?search=...
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetArticles([FromQuery] string? search)
        {
            var query = _context.Articles.Include(a => a.Details).AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(a => a.Title.Contains(search) || a.Category.Contains(search));

            var result = await query.Select(a => new ArticleDto
            {
                Id = a.Id,
                Title = a.Title,
                Category = a.Category,
                PublishedAt = a.PublishedAt,
                Details = a.Details == null ? null : new ArticleDetailsDto
                {
                    Summary = a.Details.Summary,
                    HeroImageUrl = a.Details.HeroImageUrl,
                    ReadingTimeSeconds = a.Details.ReadingTimeSeconds
                }
            }).ToListAsync();

            return Ok(result);
        }

        // GET /api/articles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> GetArticle(int id)
        {
            var article = await _context.Articles.Include(a => a.Details)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null) return NotFound();

            return new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                Category = article.Category,
                PublishedAt = article.PublishedAt,
                Details = article.Details == null ? null : new ArticleDetailsDto
                {
                    Summary = article.Details.Summary,
                    HeroImageUrl = article.Details.HeroImageUrl,
                    ReadingTimeSeconds = article.Details.ReadingTimeSeconds
                }
            };
        }

        // POST /api/articles
        [HttpPost]
        public async Task<ActionResult<ArticleDto>> CreateArticle(ArticleDto dto)
        {
            var article = new Article
            {
                Title = dto.Title,
                Category = dto.Category,
                PublishedAt = dto.PublishedAt,
                Details = dto.Details == null ? null : new ArticleDetails
                {
                    Summary = dto.Details.Summary,
                    HeroImageUrl = dto.Details.HeroImageUrl,
                    ReadingTimeSeconds = dto.Details.ReadingTimeSeconds
                }
            };

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            dto.Id = article.Id;
            return CreatedAtAction(nameof(GetArticle), new { id = article.Id }, dto);
        }

        // PUT /api/articles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(int id, ArticleDto dto)
        {
            var article = await _context.Articles.Include(a => a.Details).FirstOrDefaultAsync(a => a.Id == id);
            if (article == null) return NotFound();

            article.Title = dto.Title;
            article.Category = dto.Category;
            article.PublishedAt = dto.PublishedAt;

            if (dto.Details != null)
            {
                if (article.Details == null)
                {
                    article.Details = new ArticleDetails
                    {
                        Summary = dto.Details.Summary,
                        HeroImageUrl = dto.Details.HeroImageUrl,
                        ReadingTimeSeconds = dto.Details.ReadingTimeSeconds
                    };
                }
                else
                {
                    article.Details.Summary = dto.Details.Summary;
                    article.Details.HeroImageUrl = dto.Details.HeroImageUrl;
                    article.Details.ReadingTimeSeconds = dto.Details.ReadingTimeSeconds;
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE /api/articles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _context.Articles.Include(a => a.Details).FirstOrDefaultAsync(a => a.Id == id);
            if (article == null) return NotFound();

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
