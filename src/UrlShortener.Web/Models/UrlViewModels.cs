using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Web.Models;

public class CreateUrlViewModel
{
    [Required(ErrorMessage = "URL обязателен.")]
    [Url(ErrorMessage = "Пожалуйста введите валидный URL (e.g. https://example.com).")]
    [RegularExpression(@"^https?://[^/]+\.[a-zA-Z]{2,}(/.*)?$", ErrorMessage = "URL должен быть валидным ( https://example.com).")]
    [MaxLength(2048, ErrorMessage = "URL не должен быть более 2048 символов.")]
    public string OriginalUrl { get; set; } = string.Empty;
}

public class EditUrlViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "URL обязателен.")]
    [Url(ErrorMessage = "Пожалуйста введите валидный URL (e.g. https://example.com).")]
    [RegularExpression(@"^https?://[^/]+\.[a-zA-Z]{2,}(/.*)?$", ErrorMessage = "URL должен быть валидным  https://example.com).")]
    [MaxLength(2048, ErrorMessage = "URL не должен быть более 2048 символов.")]
    public string OriginalUrl { get; set; } = string.Empty;

    public string ShortCode { get; set; } = string.Empty;
}

public class UrlListViewModel
{
    public IReadOnlyList<UrlItemViewModel> Urls { get; set; } = [];
    public CreateUrlViewModel NewUrl { get; set; } = new();
}

public class UrlItemViewModel
{
    public int Id { get; set; }
    public string OriginalUrl { get; set; } = string.Empty;
    public string ShortUrl { get; set; } = string.Empty;
    public string ShortCode { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public long ClickCount { get; set; }
}
