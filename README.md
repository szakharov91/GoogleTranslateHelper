# GoogleTranslateHelper
Library for simple translation using google

### # Client code

````csharp
Console.WriteLine(GtCore.Get().Translate("Hello, World!", "ru"));

Console.WriteLine(GtCore.Get().SetContent("Hello, World!").To(Languages.German));

Console.WriteLine(GtCore.Get().SetContent("Hello, World!").To("tr"));

Console.WriteLine(GtCore.Get().SetContent("Hello, World!").To("hr"));
````
