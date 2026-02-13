using UrlShortener.Web.Models;

namespace UrlShortener.Web.Services;

public interface IUrlShortenerService
{
    Task<IReadOnlyList<ShortenedUrl>> GetAllAsync();
    Task<ShortenedUrl?> GetByIdAsync(int id);
    Task<ShortenedUrl?> GetByShortCodeAsync(string shortCode);
    Task<ShortenedUrl> CreateAsync(string originalUrl);
    Task<ShortenedUrl?> UpdateAsync(int id, string newOriginalUrl);
    Task<bool> DeleteAsync(int id);
    Task IncrementClickCountAsync(string shortCode);
}
