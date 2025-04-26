using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleTranslateHelper.Tests
{
    public class GtCoreTests
    {
        [Fact]
        public void Get_ShouldReturnNotSameInstance()
        {
            var a = new GtCore(new HttpClient());
            var b = new GtCore(new HttpClient());
            Assert.NotSame(a, b);
        }

        [Theory]
        [InlineData("", "en", "")]
        [InlineData(null, "ru", "")]
        [InlineData("   ", "de", "")]
        public void Translate_EmptyOrWhitespaceInput_ReturnsEmpty(string input, string lang, string expected)
        {
            var translator = new GtCore(new HttpClient());
            var result = translator.Translate(input, lang);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Translate_HelloToGerman_ReturnsNonEmptyText()
        {
            var translator = new GtCore(new HttpClient());
            var result = translator.Translate("hello", "de");
            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        [Fact]
        public void SetContent_And_To_MethodChain_WorksCorrectly()
        {
            var translator = new GtCore(new HttpClient()).SetContent("world");
            var result = translator.To("ru");
            Assert.False(string.IsNullOrWhiteSpace(result));
        }
    }
}
