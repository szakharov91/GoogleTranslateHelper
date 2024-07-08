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
        #region public properties
        /// <summary> Возвращает синглтон </summary>
        public static GtCore Get => Nested.Instance;
        #endregion

        #region private methods
        /// <summary> Закрываем конструктор </summary>
        private GtCore() => _httpClient = new HttpClient(); 
        
        /// <summary> Перевести текст </summary>
        /// <param name="inputText">Исходный текст</param>
        /// <param name="LanguageCode">Код языка ISO 639-1</param>
        public string Translate(string inputText, string LanguageCode) =>
            Translate(inputText, LanguageCode, "");
        
        #endregion

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
            _httpClient.AddUserAgentToHeader();

            var fromLang = from == null ? "auto" : from.GetStringValue();

            string urlForTranslate = $"https://translate.google.com/m?" +
                                     $"sl={fromLang}&tl={to.GetStringValue()}" +
                                     $"&ie=UTF-8&prev=_m&q={inputText}";

            var response = _httpClient.GetAsync(urlForTranslate).Result;
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
            _httpClient.AddUserAgentToHeader();

            if (string.IsNullOrEmpty(languageFrom))
                languageFrom = "auto";
            
            string urlForTranslate = $"https://translate.google.com/m?" +
                                     $"sl={languageFrom}&tl={languageTo}" +
                                     $"&ie=UTF-8&prev=_m&q={inputText}";
            
            var response = _httpClient.GetAsync(urlForTranslate).Result;
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Translate fault with http status code {response.StatusCode}");

            return ParseTranslatedText(response.Content.ReadAsStringAsync().Result);
        }
        #endregion
        
        #region private fields
        /// <summary> Синглтон </summary>
        private static class Nested
        {
            internal static readonly GtCore Instance = new();
        }

        /// <summary> Клиент для работы с Google Translator </summary>
        private readonly HttpClient _httpClient; 
        #endregion
    }
}
