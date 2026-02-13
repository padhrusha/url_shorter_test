using UrlShortener.Web.Services;

namespace UrlShortener.Tests;

public class ShortCodeGeneratorTests
{
    //just as example 
    [Fact]
    public void Generate_ReturnsCodeOfLength7()
    {
        var code = ShortCodeGenerator.Generate();
        Assert.Equal(7, code.Length);
    }
}
