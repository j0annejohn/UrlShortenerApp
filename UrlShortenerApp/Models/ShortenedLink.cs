using System.ComponentModel.DataAnnotations;

namespace UrlShortenerApp.Models
{
    public class ShortenedLink
    {
        public int Id { get; set; }

        [Required]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string OriginalUrl { get; set; }

        public string ShortCode { get; set; } // The 6-character random code

        public int ClickCount { get; set; } = 0; // Tracking how many people click it

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}