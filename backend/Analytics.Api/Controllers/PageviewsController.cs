using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Analytics.Api.Data;
using Analytics.Api.DTOs;

namespace Analytics.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PageviewsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PageviewsController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/pageviews?from=...&to=...&articleId=...
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PageviewDto>>> GetPageviews([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int? articleId)
        {
            var query = _context.Pageviews.Include(p => p.Article).AsQueryable();

            if (from.HasValue) query = query.Where(p => p.ViewedAt >= from.Value);
            if (to.HasValue) query = query.Where(p => p.ViewedAt <= to.Value);
            if (articleId.HasValue) query = query.Where(p => p.ArticleId == articleId.Value);

            var result = await query.OrderByDescending(p => p.ViewedAt).Take(100)
                .Select(p => new PageviewDto
                {
                    Id = p.Id,
                    ArticleId = p.ArticleId,
                    ArticleTitle = p.Article!.Title,
                    ViewedAt = p.ViewedAt,
                    DurationSeconds = p.DurationSeconds,
                    IsBounce = p.IsBounce
                }).ToListAsync();

            return Ok(result);
        }

        // POST /api/pageviews
        [HttpPost]
        public async Task<ActionResult<PageviewDto>> AddPageview(PageviewDto dto)
        {
            var pageview = new Models.Pageview
            {
                ArticleId = dto.ArticleId,
                ViewedAt = dto.ViewedAt,
                DurationSeconds = dto.DurationSeconds,
                IsBounce = dto.IsBounce
            };

            _context.Pageviews.Add(pageview);
            await _context.SaveChangesAsync();

            dto.Id = pageview.Id;
            return CreatedAtAction(nameof(GetPageviews), new { id = pageview.Id }, dto);
        }

        // DELETE /api/pageviews/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePageview(int id)
        {
            var pv = await _context.Pageviews.FindAsync(id);
            if (pv == null) return NotFound();

            _context.Pageviews.Remove(pv);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
