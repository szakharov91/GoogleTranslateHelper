using GoogleTranslateHelper.Core;
using GoogleTranslateHelper.Extensions;
using GoogleTranslateHelper.Utils.HttpClient;

namespace GoogleTranslateHelper.Core;

/// <summary> Ядро библиотеки </summary>
public class GtCore
{
    private readonly HttpClient httpClient;

    private string tempContent;

    /// <summary> .ctor </summary>
    /// <param name="httpClient">inject http client</param>
    public GtCore(HttpClient httpClient) => this.httpClient = httpClient;

    /// <summary> Устанавливает временный контет </summary>
    /// <param name="inputText">входящий текст</param>
    /// <returns>Текущий инстанс объекта, для fluent синтаксиса</returns>
    public GtCore SetContent(string inputText)
    {
        this.tempContent = inputText;

        return this;
    }

    /// <summary> Переводит установленный контент </summary>
    /// <param name="languages">Язык из библиотеки</param>
    /// <returns>Переведенная строка</returns>
    public string To(Languages languages)
    {
        if (string.IsNullOrEmpty(this.tempContent) || string.IsNullOrWhiteSpace(this.tempContent))
        {
            return string.Empty;
        }

        return this.Translate(this.tempContent, languages.GetStringValue());
    }

    /// <summary> Переводит установленный контент </summary>
    /// <param name="language">Код языка ISO 3166-1 alpha-2 </param>
    /// <returns>Переведенная строка</returns>
    public string To(string language)
    {
        if (string.IsNullOrEmpty(this.tempContent) || string.IsNullOrWhiteSpace(this.tempContent))
        {
            return string.Empty;
        }

        return this.Translate(this.tempContent, language);
    }

    #region private methods

    /// <summary> Разбирает DIV с результатом перевода и возвращает текстовое значение </summary>
    /// <param name="htmlPageResult">html страница переводчика</param>
    private string ParseTranslatedText(string htmlPageResult)
    {
        var resultContainer = Regex.Matches(htmlPageResult, @"div[^""]*?""result-container"".*?>(.+?)</div>");
        return resultContainer.Count > 0 ? resultContainer[0].Groups[1].Value : null;
    }

    /// <summary> Перевести текст </summary>
    /// <param name="inputText">Исходных текст</param>
    /// <param name="languageTo">На какой язык</param>
    /// <param name="languageFrom">С какого языка</param>
    /// <returns>Переведенный текст</returns>
    /// <exception cref="Exception"></exception>
    public string Translate(string inputText, string languageTo, string languageFrom = "")
    {
        if (string.IsNullOrEmpty(inputText) || string.IsNullOrWhiteSpace(inputText)) return string.Empty;

        httpClient.AddUserAgentToHeader();

        if (string.IsNullOrEmpty(languageFrom))
            languageFrom = "auto";

        string urlForTranslate = $"https://translate.google.com/m?" +
                                 $"sl={languageFrom}&tl={languageTo}" +
                                 $"&ie=UTF-8&prev=_m&q={inputText}";

        var response = httpClient.GetAsync(urlForTranslate).Result;
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            throw new Exception($"Translate fault with http status code {response.StatusCode}");

        return ParseTranslatedText(response.Content.ReadAsStringAsync().Result);
    }
    #endregion

}
