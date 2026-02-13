using Microsoft.EntityFrameworkCore;
using UrlShortener.Web.Data;
using UrlShortener.Web.Services;

namespace UrlShortener.Tests;

public class UrlShortenerServiceTests : IDisposable
{
    private readonly AppDbContext _db;
    private readonly UrlShortenerService _service;

    public UrlShortenerServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _db = new AppDbContext(options);
        _service = new UrlShortenerService(_db);
    }

    public void Dispose()
    {
        _db.Dispose();
    }

    [Fact]
    public async Task CreateAsync_StoresUrlAndReturnsEntity()
    {
        var result = await _service.CreateAsync("https://example.com");

        Assert.NotNull(result);
        Assert.Equal("https://example.com", result.OriginalUrl);
        Assert.Equal(7, result.ShortCode.Length);
        Assert.Equal(0, result.ClickCount);
    }
}
