using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Analytics.Api.Data;
using Analytics.Api.DTOs;

namespace Analytics.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnalyticsController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/analytics/kpis?from=...&to=...
        [HttpGet("kpis")]
        public async Task<ActionResult<KpiDto>> GetKpis([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var query = _context.Pageviews.AsQueryable();
            if (from.HasValue) query = query.Where(p => p.ViewedAt >= from.Value);
            if (to.HasValue) query = query.Where(p => p.ViewedAt <= to.Value);

            var totalViews = await query.CountAsync();
            var avgTime = totalViews > 0 ? await query.AverageAsync(p => p.DurationSeconds) : 0;
            var bounceRate = totalViews > 0 ? 100.0 * await query.CountAsync(p => p.IsBounce) / totalViews : 0;

            return Ok(new KpiDto
            {
                TotalViews = totalViews,
                AvgTime = Math.Round(avgTime, 2),
                BounceRate = Math.Round(bounceRate, 2)
            });
        }

        // GET /api/analytics/daily-views?from=...&to=...
        [HttpGet("daily-views")]
        public async Task<ActionResult<IEnumerable<object>>> GetDailyViews([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var query = _context.Pageviews.AsQueryable();
            if (from.HasValue) query = query.Where(p => p.ViewedAt >= from.Value);
            if (to.HasValue) query = query.Where(p => p.ViewedAt <= to.Value);

            var dailyViews = await query
                .GroupBy(p => p.ViewedAt.Date)
                .Select(g => new { Date = g.Key, Views = g.Count() })
                .OrderBy(g => g.Date)
                .ToListAsync();

            return Ok(dailyViews);
        }

        // GET /api/analytics/top-articles?from=...&to=...&limit=5
        [HttpGet("top-articles")]
        public async Task<ActionResult<IEnumerable<object>>> GetTopArticles([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int limit = 5)
        {
            var query = _context.Pageviews.Include(p => p.Article).AsQueryable();
            if (from.HasValue) query = query.Where(p => p.ViewedAt >= from.Value);
            if (to.HasValue) query = query.Where(p => p.ViewedAt <= to.Value);

            var top = await query
                .GroupBy(p => new { p.ArticleId, p.Article!.Title })
                .Select(g => new { Title = g.Key.Title, Views = g.Count() })
                .OrderByDescending(g => g.Views)
                .Take(limit)
                .ToListAsync();

            return Ok(top);
        }
    }
}
