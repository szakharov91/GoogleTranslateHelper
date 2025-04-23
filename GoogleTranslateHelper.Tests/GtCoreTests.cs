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
        public void Get_ShouldReturnSameInstance()
        {
            var a = GtCore.Get();
            var b = GtCore.Get();
            Assert.Same(a, b);
        }

        [Theory]
        [InlineData("", "en", "")]
        [InlineData(null, "ru", "")]
        [InlineData("   ", "de", "")]
        public void Translate_EmptyOrWhitespaceInput_ReturnsEmpty(string input, string lang, string expected)
        {
            var translator = GtCore.Get();
            var result = translator.Translate(input, lang);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Translate_HelloToGerman_ReturnsNonEmptyText()
        {
            var translator = GtCore.Get();
            var result = translator.Translate("hello", "de");
            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        [Fact]
        public void SetContent_And_To_MethodChain_WorksCorrectly()
        {
            var translator = GtCore.Get().SetContent("world");
            var result = translator.To("ru");
            Assert.False(string.IsNullOrWhiteSpace(result));
        }
    }
}
