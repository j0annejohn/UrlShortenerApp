using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortenerApp.Data;
using UrlShortenerApp.Models;

namespace UrlShortenerApp.Controllers
{
    public class ShortenerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShortenerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. THE MAIN PAGE: Shows the list of all links
        public async Task<IActionResult> Index()
        {
            var links = await _context.ShortenedLinks.OrderByDescending(l => l.CreatedAt).ToListAsync();
            return View(links);
        }

        // 2. THE LOGIC: This creates the short link
        [HttpPost]
        public async Task<IActionResult> Create(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl)) return RedirectToAction("Index");

            // Generate a random 6-character code
            string code = Guid.NewGuid().ToString().Substring(0, 6);

            var newLink = new ShortenedLink
            {
                OriginalUrl = originalUrl,
                ShortCode = code,
                CreatedAt = DateTime.Now
            };

            _context.ShortenedLinks.Add(newLink);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // 3. THE REDIRECT: This is what happens when someone clicks the short link
        [Route("s/{code}")]
        public async Task<IActionResult> RedirectTo(string code)
        {
            var link = await _context.ShortenedLinks.FirstOrDefaultAsync(l => l.ShortCode == code);

            if (link == null) return NotFound();

            // Increment the click counter (for your Analytics requirement!)
            link.ClickCount++;
            await _context.SaveChangesAsync();

            return Redirect(link.OriginalUrl);
        }
    }
}