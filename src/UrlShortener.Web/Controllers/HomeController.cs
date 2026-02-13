using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Web.Models;
using UrlShortener.Web.Services;

namespace UrlShortener.Web.Controllers;

public class HomeController : Controller
{
    private readonly IUrlShortenerService _urlService;

    public HomeController(IUrlShortenerService urlService)
    {
        _urlService = urlService;
    }

    public async Task<IActionResult> Index()
    {
        var urls = await _urlService.GetAllAsync();

        var viewModel = new UrlListViewModel
        {
            Urls = urls.Select(u => new UrlItemViewModel
            {
                Id = u.Id,
                OriginalUrl = u.OriginalUrl,
                ShortCode = u.ShortCode,
                ShortUrl = $"{Request.Scheme}://{Request.Host}/{u.ShortCode}",
                CreatedAt = u.CreatedAt,
                ClickCount = u.ClickCount
            }).ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind(Prefix = "NewUrl")] CreateUrlViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var urls = await _urlService.GetAllAsync();
            var viewModel = new UrlListViewModel
            {
                NewUrl = model,
                Urls = urls.Select(u => new UrlItemViewModel
                {
                    Id = u.Id,
                    OriginalUrl = u.OriginalUrl,
                    ShortCode = u.ShortCode,
                    ShortUrl = $"{Request.Scheme}://{Request.Host}/{u.ShortCode}",
                    CreatedAt = u.CreatedAt,
                    ClickCount = u.ClickCount
                }).ToList()
            };
            return View("Index", viewModel);
        }

        await _urlService.CreateAsync(model.OriginalUrl);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var url = await _urlService.GetByIdAsync(id);
        if (url is null) return NotFound();

        return View(new EditUrlViewModel
        {
            Id = url.Id,
            OriginalUrl = url.OriginalUrl,
            ShortCode = url.ShortCode
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditUrlViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _urlService.UpdateAsync(model.Id, model.OriginalUrl);
        if (result is null) return NotFound();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _urlService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
