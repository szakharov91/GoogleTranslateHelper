namespace GoogleTranslateHelper.Utils.HttpClient
{
    public static class HttpClientExtensions
    {
        /// <summary> Добавляет в заголовок случайный user-agent из списка </summary>
        /// <param name="httpClient"></param>
        public static void AddUserAgentToHeader(this System.Net.Http.HttpClient httpClient)
        {
            if (httpClient.DefaultRequestHeaders.Contains("User-Agent"))
                httpClient.DefaultRequestHeaders.Remove("User-Agent");

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", UserAgentCarousel.GetUserAgent());
        }
    }
}
