using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleTranslateHelper.Core;

namespace GoogleTranslateHelper.Tests;

public class GtCoreTests
{
    [Fact]
    public void Get_ShouldReturnNotSameInstance()
    {
        using var httpClient = new HttpClient();

        var a = new GtCore(httpClient);
        var b = new GtCore(httpClient);
        Assert.NotSame(a, b);
    }

    [Theory]
    [InlineData("", "en", "")]
    [InlineData(null, "ru", "")]
    [InlineData("   ", "de", "")]
    public void Translate_EmptyOrWhitespaceInput_ReturnsEmpty(string input, string lang, string expected)
    {
        using var httpClient = new HttpClient();

        var translator = new GtCore(httpClient);
        var result = translator.Translate(input, lang);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Translate_HelloToGerman_ReturnsNonEmptyText()
    {
        using var httpClient = new HttpClient();

        var translator = new GtCore(httpClient);
        var result = translator.Translate("hello", "de");
        Assert.False(string.IsNullOrWhiteSpace(result));
    }

    [Fact]
    public void SetContent_And_To_MethodChain_WorksCorrectly()
    {
        using var httpClient = new HttpClient();

        var translator = new GtCore(httpClient).SetContent("world");
        var result = translator.To("ru");
        Assert.False(string.IsNullOrWhiteSpace(result));
    }
}
