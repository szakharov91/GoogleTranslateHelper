using GoogleTranslateHelper.Core;

namespace GoogleTranslateHelper.ConsoleApp;

internal sealed class Program
{
    static void Main(string[] args)
    {
        using var httpClient = new HttpClient();

        var googleTranslateCore = new GtCore(httpClient);

        Console.WriteLine(googleTranslateCore.Translate("Hello, World!", "ru"));

        Console.WriteLine(googleTranslateCore.SetContent("Hello, World!").To(Languages.German));

        Console.WriteLine(googleTranslateCore.SetContent("Hello, World!").To("tr"));

        Console.WriteLine(googleTranslateCore.SetContent("Hello, World!").To("hr"));

        Console.WriteLine(googleTranslateCore.Translate("На краю дороги стоял дуб. " +
            "Вероятно, в десять раз старше берёз, составлявших лес, " +
            "он был в десять раз толще и в два раза выше каждой берёзы.", "fr"));

        Console.ReadLine();
    }
}
