using System;
using System.Net.Http;
using GoogleTranslateHelper.Extensions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GoogleTranslateHelper.Utils.HttpClient;

namespace GoogleTranslateHelper
{
    /// <summary> Ядро библиотеки </summary>
    public class GtCore
    {
        /// <summary> Возвращает синглтон </summary>
        private static readonly Lazy<GtCore> Instance = new Lazy<GtCore>(() => new GtCore());

        /// <summary> Закрываем конструктор </summary>
        private GtCore() { }

        private string _tempContent;

        public static GtCore Get() => Instance.Value;

        /// <summary> Перевести текст </summary>
        /// <param name="inputText">Исходный текст</param>
        /// <param name="LanguageCode">Код языка ISO 639-1</param>
        public string Translate(string inputText, string LanguageCode) =>
            Translate(inputText, LanguageCode, "");

        /// <summary> Устанавливает временный контет </summary>
        /// <param name="inputText">входящий текст</param>
        public GtCore SetContent(string inputText)
        {
            _tempContent = inputText;

            return this;
        }

        /// <summary> Переводит установленный контент </summary>
        /// <param name="languages">Язык из библиотеки</param>
        /// <returns>Переведенная строка</returns>
        public string To(Languages languages)
        {
            if (string.IsNullOrEmpty(_tempContent) || string.IsNullOrWhiteSpace(_tempContent)) return string.Empty;

            return Translate(_tempContent, languages.GetStringValue());
        }

        /// <summary> Переводит установленный контент </summary>
        /// <param name="languages">Код языка ISO 3166-1 alpha-2 </param>
        /// <returns>Переведенная строка</returns>
        public string To(string language)
        {
            if (string.IsNullOrEmpty(_tempContent) || string.IsNullOrWhiteSpace(_tempContent)) return string.Empty;

            return Translate(_tempContent, language);
        }

        //---------------------------------------------------------------------------
        #region private methods
        //---------------------------------------------------------------------------
        /// <summary> Разбирает DIV с результатом перевода и возвращает текстовое значение </summary>
        /// <param name="htmlPageResult">html страница переводчика</param>
        private string ParseTranslatedText(string htmlPageResult)
        {
            var resultContainer = Regex.Matches(htmlPageResult, @"div[^""]*?""result-container"".*?>(.+?)</div>");
            return resultContainer.Count > 0 ? resultContainer[0].Groups[1].Value : null;
        }
        
        /// <summary> Перевести текст </summary>
        /// <param name="inputText">Исходный текст</param>
        /// <param name="from">С какого языка</param>
        /// <param name="to">На какой язык</param>
        private string Translate(string inputText, Languages to, Languages? from = null)
        {
            if (string.IsNullOrEmpty(inputText) || string.IsNullOrWhiteSpace(inputText)) return string.Empty;

            using var httpClient = new HttpClient();

            httpClient.AddUserAgentToHeader();

            var fromLang = from == null ? "auto" : from.GetStringValue();

            string urlForTranslate = $"https://translate.google.com/m?" +
                                     $"sl={fromLang}&tl={to.GetStringValue()}" +
                                     $"&ie=UTF-8&prev=_m&q={inputText}";

            var response = httpClient.GetAsync(urlForTranslate).Result;
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Translate fault with http status code {response.StatusCode}");

            return ParseTranslatedText(response.Content.ReadAsStringAsync().Result);
        }

        /// <summary> Перевести текст </summary>
        /// <param name="inputText">Исходных текст</param>
        /// <param name="languageTo">На какой язык</param>
        /// <param name="languageFrom">С какого языка</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private string Translate(string inputText, string languageTo, string languageFrom = "")
        {
            if (string.IsNullOrEmpty(inputText) || string.IsNullOrWhiteSpace(inputText)) return string.Empty;

            using var httpClient = new HttpClient();

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
}
