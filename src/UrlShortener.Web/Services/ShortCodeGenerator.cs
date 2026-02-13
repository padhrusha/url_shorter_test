using System.Security.Cryptography;

namespace UrlShortener.Web.Services;

public static class ShortCodeGenerator
{
    // для большей защиты можно добавить знаки пунктуации и др.
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private const int CodeLength = 7;

    public static string Generate()
    {
        return RandomNumberGenerator.GetString(Alphabet, CodeLength);
    }
}
