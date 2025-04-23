using GoogleTranslateHelper.Utils.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleTranslateHelper.Tests
{
    public class HttpClientExtensionsTests
    {
        [Fact]
        public void AddUserAgentToHeader_ShouldAddUserAgentHeader()
        {
            var client = new HttpClient();
            client.AddUserAgentToHeader();

            Assert.True(client.DefaultRequestHeaders.Contains("User-Agent"));
            var headerValues = client.DefaultRequestHeaders.GetValues("User-Agent").ToList();
            string completeUserAgent = string.Join(" ", headerValues);
            
            Assert.False(string.IsNullOrWhiteSpace(completeUserAgent));
        }

        [Fact]
        public void AddUserAgentToHeader_ShouldReplaceExistingHeader()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "OldValue");
            client.AddUserAgentToHeader();

            var headerValues = client.DefaultRequestHeaders.GetValues("User-Agent").ToList();
            string completeUserAgent = string.Join(" ", headerValues);

            Assert.NotEqual("OldValue", completeUserAgent);
            Assert.False(string.IsNullOrWhiteSpace(completeUserAgent));
        }
    }
}
