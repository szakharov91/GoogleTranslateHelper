namespace GoogleTranslateHelper.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GtCore.Get().Translate("Hello, World!", "ru"));

            Console.WriteLine(GtCore.Get().SetContent("Hello, World!").To(Languages.German));

            Console.WriteLine(GtCore.Get().SetContent("Hello, World!").To("tr"));

            Console.WriteLine(GtCore.Get().SetContent("Hello, World!").To("hr"));

            Console.WriteLine(GtCore.Get().Translate("На краю дороги стоял дуб. " +
                "Вероятно, в десять раз старше берёз, составлявших лес, " +
                "он был в десять раз толще и в два раза выше каждой берёзы.", "fr"));

            Console.ReadLine();
        }
    }
}
