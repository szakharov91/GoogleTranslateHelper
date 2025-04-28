using GoogleTranslateHelper.Core;

namespace GoogleTranslateHelper.ConsoleApp;

internal sealed class Program
{
    public static void Main()
    {
        using var httpClient = new HttpClient();

        var googleTranslateCore = new GtCore(httpClient);

        Console.WriteLine(googleTranslateCore.Translate("Hello, World!", "ru"));

        Console.WriteLine(googleTranslateCore.SetContent("Hello, World!").To(Languages.German));

        Task.Run(async() => Console.WriteLine(await googleTranslateCore.SetContent("Hello, World!").ToAsync("tr")));

        Task.Run(async () => Console.WriteLine(await googleTranslateCore.SetContent("Hello, World!").ToAsync("hr")));

        Task.Run(async () => Console.WriteLine(await googleTranslateCore.TranslateAsync("На краю дороги стоял дуб. " +
            "Вероятно, в десять раз старше берёз, составлявших лес, " +
            "он был в десять раз толще и в два раза выше каждой берёзы.", "fr")));

        Console.ReadLine();
    }
}
