using Microsoft.AspNetCore.Mvc;
using UrlShortener.Web.Services;

namespace UrlShortener.Web.Controllers;

//user shortend url to redirect
public class RedirectController : Controller
{
    private readonly IUrlShortenerService _urlService;

    public RedirectController(IUrlShortenerService urlService)
    {
        _urlService = urlService;
    }

    [HttpGet("/{shortCode:regex(^[[a-zA-Z0-9]]{{7}}$)}")]
    public async Task<IActionResult> Go(string shortCode)
    {
        var url = await _urlService.GetByShortCodeAsync(shortCode);
        if (url is null) return NotFound();

        await _urlService.IncrementClickCountAsync(shortCode);

        return Redirect(url.OriginalUrl);
    }
}
