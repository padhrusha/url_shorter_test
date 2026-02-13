using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Web.Models;

public class ShortenedUrl
{
    public int Id { get; set; }

    [Required]
    [Url]
    [MaxLength(2048)]
    public string OriginalUrl { get; set; } = string.Empty;

    [MaxLength(10)]
    public string ShortCode { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public long ClickCount { get; set; }
}
