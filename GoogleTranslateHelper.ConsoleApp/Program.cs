namespace GoogleTranslateHelper.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GtCore.Get().Translate("Hello, World!", "ru"));

            Console.ReadLine();
        }
    }
}
