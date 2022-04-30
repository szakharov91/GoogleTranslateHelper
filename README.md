# GoogleTranslateHelper
Library for simple translation using google

### # Client code

````csharp
var translator = GtCore.Get;

var readLine = Console.ReadLine();
var result = translator.Translate(readLine, "de");
````
